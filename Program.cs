﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyMath;
using MyFramework.GUI;

namespace MyFramework
{
    public class Program
    {
        private Stack<Scene> scenes;
        private Scene currentScene;
        private Window window;

        private bool running;
        private bool changed;

        private int frameDuration;
		//what is this?
        private Thread updateThread;

        public Program()
        {
            configure();

            scenes = new Stack<Scene>();
        }

        public void configure()
        {
			Config config = Config.getConfigs();

            frameDuration = 1000 / config.frameRate;

            System.Drawing.Font f = new System.Drawing.Font(new System.Drawing.FontFamily("Consolas"), 10);
            ASCII.GenerateASCIICharSet(f);
            System.Drawing.Font tf = new System.Drawing.Font(new System.Drawing.FontFamily("Consolas"), 16*f.Size);
            ASCII.SetTextFont(tf);

			window = config.systemWindow;
			window.setTitle (config.title);
			window.changeColor (config.foreGroundColor, config.backGroundColor);
			window.resize (config.windowSize);
			//provisorical
			window.cursorVisible (false);
			window.clear ();
        }

        public void run(Scene startingScene)
        {
            window.inputListener += keyPressedListener;
            window.initialize();

            addScene(this, startingScene);

            running = true;

            int lastFrameTime = DateTime.Now.Millisecond;
            while (running)
            {
                update();

                int sleepRemaining = frameDuration - (DateTime.Now.Millisecond - lastFrameTime);
                if (sleepRemaining > 0)
                {
                    Thread.Sleep(sleepRemaining);
                }
                lastFrameTime = DateTime.Now.Millisecond;
            }
        }

        //maybe add an update method to each GUIElement
        void update()
        {
            currentScene.onNextFrame(this, EventArgs.Empty);

            if (changed)
            {
                drawFrame();
                changed = false;
            }
        }

        void drawFrame()
        {
            lock (this)
            {
				//subject to change
				window.setContent(currentScene.getGraphic());
            }
        }

        void keyPressedListener(object sender, string key)
        {
            currentScene.onKeyDown(key);
        }

        private void windowChanged(object sender, EventArgs e)
        {
            changed = true;
        }

        private void closeScene(object sender, EventArgs e)
        {
            if(sender == scenes.Peek())
            {
                Scene s = scenes.Pop();

                s.changed -= new MyFramework.EventHandler(windowChanged);
                s.closeScene -= new MyFramework.EventHandler(closeScene);

                Stack<Scene>.Enumerator en = scenes.GetEnumerator();

                if(en.MoveNext())
                {
                    currentScene = en.Current;
                }
                else
                {
                    halt();
                }

                drawFrame();
            }
			else
			{
				//what to do?
			}
        }

        private void addScene(object sender, Scene s)
        {
            scenes.Push(s);
            currentScene = s;

            s.changed    += new EventHandler(windowChanged);
            s.closeScene += new EventHandler(closeScene);
            s.addScene   += new SceneEventHandler(addScene);

            changed = true;
        }

        private void halt()
        {
            running = false;
            window.stop();
        }
        /*
        static void Main()
        {
            
            Program p = new Program();
            p.run(new TestScene());
        }*/
    }
}
