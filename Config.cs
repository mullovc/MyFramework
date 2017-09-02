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
		static Config singleton;


        public Vector2 windowSize { get; set; }

		public Window systemWindow { get; private set; }

		public string title { get; private set; }

		public ConsoleColor foreGroundColor { get; set; }

		public ConsoleColor backGroundColor { get; set; }

		Config()
		{
			initializeFromFile ();
		}

		public static Config getConfigs()
        {
			if (singleton == null) 
			{
				singleton = new Config ();
			}

			return singleton;
        }

		void initializeFromFile()
		{
			System.IO.StreamReader reader = new System.IO.StreamReader("save/Config.txt");
			string[] lines = new string[4];


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

			//provisorical
			foreGroundColor = ConsoleColor.Black;
			//provisorical
			backGroundColor = ConsoleColor.White;
			title = lines [3];
			initializeSystem (lines[2]);
		}

		void initializeSystem(string system)
		{
			switch(system)
			{
                case "windows":
                    systemWindow = new WindowsConsoleWindow ();
                    break;
                //maybe load during runtime
                case "ubuntu":
                    systemWindow = new GnomeTerminalWindow ();
                    break;
                default:
                    throw new System.IO.IOException("Config file does not contain system");
			}
		}
    }
}
