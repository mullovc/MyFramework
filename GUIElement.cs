using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public abstract class GUIElement
    {
        protected Image graphic;

        public Vector2 size { get; protected set; }
        public  Vector2 position { get; set; }
        public int priority { get; set; }
        public bool visible { get; protected set; }

        public event EventHandler changed;

        public event EventHandler visibilityChangedListener;

        public event EventHandler resizeListener;

        public event EventHandler nextFrame;

        public event KeyEventHandler keyDown;

        public event KeyEventHandler keyUp;

        public event KeyEventHandler keyHold;

        public GUIElement()
            : this(new Vector2(0, 0), new Vector2(0, 0))
        {
        }

        public GUIElement(Vector2 size)
            : this(size, new Vector2(0, 0))
        {
            //TODO are following 3 lines duplicates? (already set in constructor call 'this(...)')
            this.size = size;
            graphic = new Image(size);
            visible = true;
        }

        public GUIElement(Vector2 size, Vector2 pos)
        {
            this.size = size;
            graphic = new Image(size);
            position = pos;
            visible = true;
        }

        /*
         * forgot to set the appropriate size?
         */
        public GUIElement(Image image)
        {
            graphic = image;
            this.size = image.size;
            position = new Vector2(0, 0);
            visible = true;
        }

        public void add(Image content, Vector2 position)
        {
            graphic.add(content, position);
        }

        public void show()
        {
            visible = true;
            onVisibilityChanged(EventArgs.Empty);
        }

        public void hide()
        {
            visible = false;
            onVisibilityChanged(EventArgs.Empty);
        }

        public void resize(Vector2 s)
        {
            size = s;
            Image oldGraphic = graphic;
            graphic = new Image(size);
            add(oldGraphic, new Vector2(0, 0));

            onResize(EventArgs.Empty);
        }

        public int getWidth()
        {
            return size.x;
        }
        public int getHeight()
        {
            return size.y;
        }

        public virtual Image getGraphic()
        {
            return graphic;
        }

        /**
         * tells listeners that the element's visibility has changed
         */
        virtual protected void onVisibilityChanged(EventArgs e)
        {
            if (visibilityChangedListener != null)
            {
                visibilityChangedListener(this, e);
            }
        }

        /**
         * tells listeners that the element's size has changed
         */
        virtual protected void onResize(EventArgs e)
        {
            if (resizeListener != null)
            {
                resizeListener(this, e);
            }
        }

        /**
         * tells listeners that the element has changed
         */
        virtual protected void onChanged(object sender, EventArgs e)
        {
            if (changed != null)
            {
				changed(this, e);
            }
        }

        public void onNextFrame(object sender, EventArgs e)
        {
            if (nextFrame != null)
            {
                nextFrame(this, e);
            }
        }

        public void onKeyDown(String key)
        {
            if (keyDown != null)
            {
                keyDown(this, key);
            }
        }

		public void onKeyUp(String key)
		{
			if (keyUp != null)
			{
				keyUp(this, key);
			}
		}

		public void onKeyHold(String key)
		{
			if (keyHold != null)
			{
				keyHold(this, key);
			}
		}
    }
}
