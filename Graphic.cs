﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
{
    public class Graphic : GUIElement
    {
        public Graphic() 
            : base()
        {

        }

        public Graphic(Vector2 size)
            : base(size)
        {

        }

        public Graphic(Image image)
            : base(image)
        {

        }

        public void changeGraphic(Image img)
        {
            graphic = img;
            size = graphic.size;
        }
    }
}
