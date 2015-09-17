using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public class SelectionDecorator : GUIDecorator
    {
        protected Vector2 componentPosition;

        public Cursor cursor { get; protected set; }
        public CursorAllocation cursorAllocation { get; set; }
        public bool active { get; protected set; }

        public event EventHandler select;


        public enum CursorAllocation
        {
            LEFT,
            RIGHT,
            ABOVE,
            BENEATH
        }


        public SelectionDecorator(GUIElement comp, CursorAllocation alloc, Cursor.CursorType type)
            : base(comp)
        {
            visible = true;

            cursorAllocation = alloc;
            cursor = new Cursor(type);

            setUp();
        }

        protected void setUp()
        {
            switch (cursorAllocation)
            {
                case CursorAllocation.LEFT:
                    size = new Vector2(component.getWidth() + cursor.getWidth(), component.getHeight());
                    graphic = new Image(size);
                    componentPosition = new Vector2(cursor.getWidth(), 0);
                    cursor.position = new Vector2(0, size.y / 2 - cursor.getHeight() / 2);
                    break;
                //TODO: implement rest
            }
        }

        public Vector2 getImageSize()
        {
            return component.size;
        }

        public void activate()
        {
            active = true;
            onChanged(EventArgs.Empty);
        }

        public void deactivate()
        {
            active = false;
            onChanged(EventArgs.Empty);
        }

        public void onSelect()
        {
            if (select != null)
            {
                select(this, EventArgs.Empty);
            }
        }

        public override Image getGraphic()
        {
            graphic.clear();

            if (active)
            {
                add(cursor.texture, cursor.position);
            }

            add(component.getGraphic(), componentPosition);
            return graphic;
        }
    }
}
