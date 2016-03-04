using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework
{
    public class Config
    {
        public static Vector2 windowSize { get; set; }

        public static void getConfigs()
        {

            System.IO.StreamReader reader = new System.IO.StreamReader("save/Config.txt");
            string[] lines = new string[2];


            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = reader.ReadLine();
			}

            reader.Close();

            try
            {
                int x = Int32.Parse(lines[0]);
                int y = Int32.Parse(lines[1]);

                windowSize = new Vector2(x, y);
            }
            catch(FormatException e)
            {
				throw e;
                //TODO: do something
            }
        }
    }
}
