﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFramework
{
    namespace GUI
    {
        public class ASCII
        {
            static Image[] ascii = new Image[128];

            public static Image charToImage(char c)
            {
                int i = (int)c;

                if (ascii[i] == null)
                {
                    ascii[i] = Image.fileToImage("GUI\\ASCII\\" + i + ".txt");
                }

                return ascii[i];
            }

        }
    }
}
