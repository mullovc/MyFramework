using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    abstract class GUIDecorator : GUIElement
    {
        protected GUIElement component;

        public GUIDecorator(GUIElement comp)
        {
            component = comp;
        }

        public GUIDecorator(GUIElement comp, Vector2 size)
            : base(size)
        {
            component = comp;
        }
    }
}
