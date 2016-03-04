using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;
using MyFramework.GUI.GUIElements;

namespace MyFramework.GUI
{
    public class Window : Container
    {

        public Window(Vector2 size) 
            : base(size)
        {
            initialize();
        }

        virtual protected void initialize()
        {

        }

    }
}
