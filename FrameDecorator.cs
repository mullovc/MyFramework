using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;
using MyFramework.GUI.GUIElements;

namespace MyFramework.GUI.GUIElements
{
    public class FrameDecorator : GUIDecorator
    {
        protected int frameWidth;
        protected int frameHeight;

        public enum FrameType
        {
            NONE,
            PLAIN
        }

        public FrameDecorator(GUIElement element)
            : this(element, FrameType.PLAIN)
        {

        }

        public FrameDecorator(GUIElement element, FrameType type)
            : base(element)
        {
            loadFrame();
            resize(new Vector2(element.getWidth() + frameWidth * 2, element.getHeight() + frameHeight * 2));

            drawFrame();
        }

        private void loadFrame()
        {
            frameHeight = 2;
            frameWidth = 2;
        }

        private void drawFrame()
        {
            for (int i = 1; i < size.x - 1; i++)
            {
                graphic.setPixel('_', new Vector2(i, 0));
                graphic.setPixel('_', new Vector2(i, size.y - 1));
            }

            for (int i = 1; i < size.y; i++)
            {
                graphic.setPixel('|', new Vector2(0, i));
                graphic.setPixel('|', new Vector2(size.x - 1, i));
            }

        }

        public override Image getGraphic()
        {
            add(component.getGraphic(), new Vector2(frameWidth, frameHeight));
            return base.getGraphic();
        }
    }

}
