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
        protected Vector2 size;
        protected Image graphic;
        public  Vector2 position { get; set; }
        public bool visible { get; set; }
        public int priority { get; set; }

        public event EventHandler changeListener;

        public GUIElement() 
            : this(new Vector2(0, 0))
        {
        }

        public GUIElement(Vector2 size)
        {
            this.size = size;
            graphic = new Image(size);
            visible = true;
        }

        /*
         * forgot to set the appropriate size?
         */
        public GUIElement(Image image)
        {
            graphic = image;
        }

        public void add(Image content, Vector2 position)
        {
            graphic.add(content, position);
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
         * tells listeners that the element has changed
         */
        virtual protected void onChanged(EventArgs e)
        {
            if (changeListener != null)
            {
                changeListener(this, EventArgs.Empty);
            }
        }
    }
}
