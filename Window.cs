using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;
using MyFramework.GUI.GUIElements;

namespace MyFramework
{
    public abstract class Window
	{
		public event KeyEventHandler inputListener;
		public Image content { get; protected set; }

		protected Vector2 size;

        protected Window()
        {

        }

        public abstract void initialize();

        public abstract void stop();

		public abstract void setTitle (string title);

		public abstract void cursorVisible (bool visible);

		public abstract void resize (Vector2 size);

		public abstract void changeResolution (Vector2 resolution);

		public abstract void changeColor (ConsoleColor foreGround, ConsoleColor backGround);

		public abstract void show ();

		public abstract void close ();

		//output-methods
		public abstract void setContent (Image content);

		public abstract void clear ();

		//input-methods
		public abstract string getInput ();

		public abstract bool keyPressed ();

        protected void onInput(string input)
        {
            if (inputListener != null)
            {
                inputListener(this, input);
            }
        }
    }
}
