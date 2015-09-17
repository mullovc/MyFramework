using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFramework.GUI.GUIElements;
using MyMath;

namespace MyFramework.GUI
{
    public abstract class Container : GUIElement
    {
        private bool changed;

        protected List<GUIElement> elements;

        public Container(Vector2 size) 
            : base(size)
        {
            elements = new List<GUIElement>();
        }

        virtual public void addElement(GUIElement element)
        {
            if (element.priority == 0)
            {
                elements.Add(element);
            }

            else
            {
                IEnumerator<GUIElement> en = elements.GetEnumerator();

                en.MoveNext();
                int cursor = 0;

                while (en.Current != null)
                {
                    if (element.priority < en.Current.priority)
                    {
                        break;
                    }

                    en.MoveNext();
                    cursor++;
                }

                elements.Insert(cursor, element);
            }

            element.changeListener += onChanged;
            onChanged(this, EventArgs.Empty);
        }

        public void removeElement(GUIElement element)
        {
            elements.Remove(element);
            element.changeListener -= onChanged;

            onChanged(this, EventArgs.Empty);
        }

        /**
         * tells listeners that the element has changed
         */
        protected void onChanged(object sender, EventArgs e)
        {
            changed = true;
            base.onChanged(EventArgs.Empty);
        }

        public virtual void render()
        {
            graphic.clear();

            IEnumerator<GUIElement> en = elements.GetEnumerator();

            en.MoveNext();
            while (en.Current != null)
            {
                if (en.Current.visible)
                {
                    graphic.add(en.Current.getGraphic(), en.Current.position);
                    en.MoveNext();
                }
            }

            changed = false;
        }

        override public Image getGraphic()
        {
            if (changed)
            {
                render();
            }
            return graphic;
        }

    }
}
