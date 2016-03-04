using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    /**
     * TODO: make a grid (kind of a two dimensional arraylist), where all the boxes are stored
     * and add an add()-method, so that the user can add as many boxes as he pleases.
     * The class then determines itself which position the newly added box has in relation to the other boxes
     * 
     * TODO: add change listeners to each GUIElement, so that it will be notices if one of them changes
     */
    public class SelectionBoxField : GUIElement
    {
        //determines whether the cursor jumps back to beginning, if the end has been reached
        public bool overflowingCursor { get; set; }
        public Vector2 cursorPosition { get; set; }
        public Cursor.CursorType cursorType { get; set; }
        public Vector2 dimensions { get; set; }
        public SelectionDecorator.CursorAllocation cursorAllocation { get; set; }

        protected SelectionDecorator[,] boxes;

        /**
         * Constructs a new rectangular field of selectable GUIElements. The GUIElements are
         * passed as the parameter b by the caller.
         */
        public SelectionBoxField(GUIElement[,] b, Vector2 size, 
            SelectionDecorator.CursorAllocation alloc, Cursor.CursorType type, bool overflowing = false)
            : base(size)
        {
            overflowingCursor = overflowing;
            cursorAllocation = alloc;
            cursorType = type;
            
            setUp(b);

            cursorPosition = new Vector2(0, 0);
            boxes[cursorPosition.x, cursorPosition.y].activate();
        }

        protected void setUp(GUIElement[,] b)
        {
            dimensions = new Vector2(b.GetLength(0), b.GetLength(1));
            boxes = new SelectionDecorator[dimensions.x, dimensions.y];

            int width = size.x / dimensions.x;
            int height = size.y / dimensions.y;

            for(int i = 0; i < dimensions.y; i++)
            {
                for (int j = 0; j < dimensions.x; j++)
                {
                    boxes[j, i] = new SelectionDecorator(b[j, i], cursorAllocation, cursorType);
                    boxes[j, i].position = new Vector2(j * width, i * height);
                }
            }
        }

        public void up()
        {
            if (cursorPosition.y > 0 || overflowingCursor)
            {
                getCurrent().deactivate();
                cursorPosition.y--;

                if (cursorPosition.y < 0)
                {
                    cursorPosition.y += dimensions.y;
                }

                getCurrent().activate();
                onChanged(this, EventArgs.Empty);
            }
        }

        public void down()
        {
            if (cursorPosition.y < dimensions.y - 1 || overflowingCursor)
            {
                getCurrent().deactivate();
                cursorPosition.y++;

                if (cursorPosition.y >= dimensions.y)
                {
                    cursorPosition.y -= dimensions.y;
                }

                getCurrent().activate();
                onChanged(this, EventArgs.Empty);
            }
        }

        public void left()
        {
            if (cursorPosition.x > 0 || overflowingCursor)
            {
                getCurrent().deactivate();
                cursorPosition.x--;

                if (cursorPosition.x < 0)
                {
                    cursorPosition.x += dimensions.x;
                }

                getCurrent().activate();
                onChanged(this, EventArgs.Empty);
            }
        }

        public void right()
        {
            if (cursorPosition.x < dimensions.x - 1 || overflowingCursor)
            {
                getCurrent().deactivate();
                cursorPosition.x++;

                if (cursorPosition.x >= dimensions.x)
                {
                    cursorPosition.x -= dimensions.x;
                }

                getCurrent().activate();
                onChanged(this, EventArgs.Empty);
            }
        }

        public void trigger()
        {
            getCurrent().onSelect();
        }

        public SelectionDecorator getCurrent()
        {
            return boxes[cursorPosition.x, cursorPosition.y];
        }

        public SelectionDecorator getElement(Vector2 pos)
        {
            return boxes[pos.x, pos.y];
        }

        public override Image getGraphic()
        {
            graphic.clear();
            for (int i = 0; i < dimensions.y; i++)
            {
                for (int j = 0; j < dimensions.x; j++)
                {
                    add(boxes[j, i].getGraphic(), boxes[j, i].position);
                }
            }

            return graphic;
        }
    }
}
