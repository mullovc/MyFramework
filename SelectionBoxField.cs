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
     */
    class SelectionBoxField : GUIElement
    {
        //determines whether the cursor jumps back to beginning, if the end has been reached
        public bool overflowingCursor { get; set; }
        public Vector2 cursorPosition { get; set; }
        public Cursor.CursorType cursorType { get; set; }
        public Vector2 dimensions { get; set; }
        public SelectionBox.CursorAllocation cursorAllocation { get; set; }

        SelectionBox[,] boxes;

        public SelectionBoxField(Vector2 size, Vector2 dimensions, Vector2 boxSize, 
            SelectionBox.CursorAllocation alloc, Cursor.CursorType type, bool overflowing = false)
            : base(size)
        {
            boxes = new SelectionBox[dimensions.x, dimensions.y];
            overflowingCursor = overflowing;
            this.dimensions = dimensions;
            cursorAllocation = alloc;
            cursorType = type;
            
            setUp();
        }

        protected void setUp()
        {
            for(int i = 0; i < dimensions.x; i++)
            {
                for (int j = 0; j < dimensions.y; j++)
                {
                    boxes[j, i] = new SelectionBox(new Vector2(size.x / dimensions.x, size.y / dimensions.y), cursorAllocation, cursorType);
                }
            }
        }

        public override Image getGraphic()
        {
            //TODO: implement
            return base.getGraphic();
        }
    }
}
