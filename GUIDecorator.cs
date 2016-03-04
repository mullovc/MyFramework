using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public abstract class GUIDecorator : GUIElement
    {
        protected GUIElement component;

        public GUIDecorator(GUIElement comp)
            : this(comp, comp.size)
        {
        }

        public GUIDecorator(GUIElement comp, Vector2 size)
            : base(size)
        {
            component = comp;
            component.visibilityChangedListener += componentVisibilityChanged;
            comp.changeListener += onChanged;
        }

        protected void componentVisibilityChanged(object sender, EventArgs e)
        {
            visible = component.visible;
            onVisibilityChanged(e);
        }
    }
}
