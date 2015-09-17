﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFramework.GUI;

namespace MyFramework
{
    public abstract class Scene
    {
        public Window window { get; protected set; }

        public event KeyEventHandler keyDown;

        public event KeyEventHandler keyUp;

        public event KeyEventHandler keyHold;

        public event EventHandler changed;

        public event EventHandler closeScene;

        public event SceneEventHandler addScene;

        public Scene(Window window)
        {
            this.window = window;

            window.changeListener += onChanged;
        }

        abstract protected void initialize();


        /**
         * tells listeners that the window has changed
         */
        protected void onChanged(object sender, EventArgs e)
        {
            if (changed != null)
            {
                changed(this, e);
            }
        }

        /**
         * tells listeners to detatch from this window and close it
         */
        protected void onCloseScene(EventArgs e)
        {
            if (closeScene != null)
            {
                closeScene(this, e);
            }
        }

        /**
         * tells listeners to open a new scene
         */
        protected void onAddScene(Scene scene)
        {
            if (addScene != null)
            {
                addScene(this, scene);
            }
        }

        //abstract public void inputHandler(String key);

        public void onKeyDown(String key)
        {
            if (keyDown != null)
            {
                keyDown(this, key);
            }
        }

    }
}
