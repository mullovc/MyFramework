using System;
using System.Threading;
using MyMath;

namespace MyFramework
{
	/**
	 * A console-window for Microsoft Windows. 
	 */
	public class WindowsConsoleWindow : Window
	{
        Thread inputThread;
        bool running;

		public WindowsConsoleWindow ()
		{
            running = false;
            inputThread = new Thread(waitForInput);
		}

        public override void initialize()
        {
            size = new Vector2(Console.WindowWidth - 1, Console.WindowHeight - 1);
            running = true;
            inputThread.Start();
        }

        public override void stop()
        {
            running = false;
        }

        void waitForInput()
        {
            while (running)
            {
                string input = getInput();
                onInput(input);
            }
        }


		public override void changeResolution (MyMath.Vector2 resolution)
		{
			throw new NotImplementedException ();
		}

        public override Vector2 windowSize()
        {
            Vector2 cs = new Vector2(Console.WindowWidth - 1, Console.WindowHeight - 1);
            if (cs != size)
            {
                resize(cs);
            }

            return size;
        }

		public override void resize (MyMath.Vector2 size)
		{
            this.size = size;
		}

		public override void changeColor (ConsoleColor foreGround, ConsoleColor backGround)
		{
			Console.ForegroundColor = foreGround;
			Console.BackgroundColor = backGround;
		}

		public override void setContent (Image content)
		{
			char[] output = new char[size.x * size.y + size.y];

			for (int i = 0; i < size.y; i++)
			{
				for (int j = 0; j < size.x; j++)
				{
					if (i < content.getHeight () && j < content.getWidth ()) 
					{
						output [i * size.x + j + i] = content.getPixel (new MyMath.Vector2 (j, i));
					} 
					else 
					{
						output [i * size.x + j + i] = ' ';
					}
				}
				output[(i + 1) * size.x + i] = '\n';
			}

            Console.SetCursorPosition(0, 0);
			Console.Write(output);
		}

		public override void clear ()
		{
            Console.Clear();
		}

		public override void show ()
		{
			//nothing to do
		}

		public override void close ()
		{
			throw new NotImplementedException ();
		}

		public override void cursorVisible (bool visible)
		{
			Console.CursorVisible = visible;
		}

		public override string getInput ()
		{
			ConsoleKeyInfo key = Console.ReadKey();
			string input = key.Key.ToString();

			input = getNumberInput(input);

			return input;
		}

		public override bool keyPressed ()
		{
			return Console.KeyAvailable;
		}

		public override void setTitle (string title)
		{
			Console.Title = title;
		}


		static string getNumberInput(string input)
		{
			switch (input)
			{
			case "D1":
				return "1";
			case "D2":
				return "2";
			case "D3":
				return "3";
			case "D4":
				return "4";
			case "D5":
				return "5";
			case "D6":
				return "6";
			case "D7":
				return "7";
			case "D8":
				return "8";
			case "D9":
				return "9";
			case "D0":
				return "0";
			default:
				return input;
			}
		}
    }
}

