using System;
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
        private Window currentWindow;

        private Input input;

        private bool running;
        private bool changed;

        private Thread updateThread;

        public Program()
        {
            configure();

            input = new Input();
            Output.display = new Display(Config.windowSize);
            scenes = new Stack<Scene>();
        }

        public void configure()
        {
            //Config.windowSize = new Vector2(150, 60);
            Config.getConfigs();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (Config.windowSize.x <= 150 && Config.windowSize.y <= 60)
            {
                Console.SetWindowSize(Config.windowSize.x + 1, Config.windowSize.y + 1);
            }
            Console.SetBufferSize(Config.windowSize.x + 1, Config.windowSize.y + 1);
            Console.Title = "Game";
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void run(Scene startingScene)
        {
            input.inputListener += keyPressedListener;
            input.start();

            addScene(this, startingScene);

            running = true;

            while (running)
            {
                update();
                Thread.Sleep(60);
            }
        }

        //maybe add an update method to each GUIElement
        void update()
        {
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
                Output.addGraphic(currentWindow, new Vector2(0, 0));
                Output.print();
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
                    currentWindow = en.Current.window;
                }
                else
                {
                    running = false;
                    input.stop();
                }

                drawFrame();
            }
        }

        private void addScene(object sender, Scene s)
        {
            scenes.Push(s);
            currentScene = s;
            currentWindow = s.window;

            s.changed    += new EventHandler(windowChanged);
            s.closeScene += new EventHandler(closeScene);
            s.addScene   += new SceneEventHandler(addScene);

            changed = true;
        }
        /*
        static void Main()
        {
            
            Program p = new Program();
            p.run(new TestScene());
        }*/
    }
}
