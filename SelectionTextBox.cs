using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public class SelectionTextBox : SelectionBox
    {
        protected TextBox textBox;

        public SelectionTextBox(String content, Vector2 size, CursorAllocation alloc, Cursor.CursorType type)
            : base(size, alloc, type)
        {
            textBox = new TextBox(content, getImageSize());
        }

        public SelectionTextBox(Vector2 size, CursorAllocation alloc, Cursor.CursorType type)
            : base(size, alloc, type)
        {
            textBox = new TextBox("", getImageSize());
        }


        public void changeContent(string content)
        {
            textBox.changeText(content);
            base.changeContent(textBox.getGraphic());
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
    }
}
