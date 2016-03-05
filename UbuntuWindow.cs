using System;

namespace MyFramework
{
	/**
	 * A gnome-terminal window.
	 * 
	 */
	public class GnomeTerminalWindow : Window
	{
		public GnomeTerminalWindow ()
		{

		}

		public override void changeResolution (MyMath.Vector2 resolution)
		{
			throw new NotImplementedException ();
		}

		public override void resize (MyMath.Vector2 size)
		{
			throw new NotImplementedException ();
		}

		public override void changeColor (ConsoleColor foreGround, ConsoleColor backGround)
		{
			throw new NotImplementedException ();
		}

		public override void setContent (Image content)
		{
			throw new NotImplementedException ();
		}

		public override void show ()
		{
			throw new NotImplementedException ();
		}

		public override void clear ()
		{
			throw new NotImplementedException ();
		}

		public override void close ()
		{
			throw new NotImplementedException ();
		}

		public override void cursorVisible (bool visible)
		{
			throw new NotImplementedException ();
		}

		public override string getInput ()
		{
			throw new NotImplementedException ();
		}

		public override bool keyPressed ()
		{
			throw new NotImplementedException ();
		}

		public override void setTitle (string title)
		{
			throw new NotImplementedException ();
		}
	}
}

