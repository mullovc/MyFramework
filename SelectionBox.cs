using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public class SelectionBox : GUIElement
    {
        protected Vector2 imagePosition;

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


        public SelectionBox(Image content, Vector2 size, CursorAllocation position, Cursor.CursorType type)
            : base(size)
        {
            this.cursorAllocation = position;
            cursor = new Cursor(type);

            setUp();
            changeContent(content);
        }

        public SelectionBox(Vector2 size, CursorAllocation position, Cursor.CursorType type)
            : base(size)
        {
            this.cursorAllocation = position;
            cursor = new Cursor(type);

            setUp();
        }

        protected void setUp()
        {
            switch (cursorAllocation)
            {
                case CursorAllocation.LEFT:
                    imagePosition = new Vector2(cursor.getWidth(), 0);
                    cursor.position = new Vector2(0, size.y / 2 - cursor.getHeight() / 2);
                    break;
                //TODO: implement rest
            }
        }

        public virtual void changeContent(Image img)
        {
            graphic.clear();
            graphic.add(img, imagePosition);
        }

        public override Image getGraphic()
        {
            Image img = graphic.getCopy();

            if (active)
            {
                img.add(cursor.texture, cursor.position);
            }

            return img;
        }

        public Vector2 getImageSize()
        {
            return new Vector2(size.x - imagePosition.x, size.y - imagePosition.y);
        }

        public void activate()
        {
            active = true;
        }

        public void deactivate()
        {
            active = false;
        }

        public void onSelect()
        {
            if (select != null)
            {
                select(this, EventArgs.Empty);
            }
        }
    }
}
