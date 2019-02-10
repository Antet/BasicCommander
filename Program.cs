using System;
using System.Collections.Generic;
using static BasicCommander.ConsoleLibrary;

namespace BasicCommander
{
	class Program
	{
		public static Output output;
		public static Navigation navigation;
		public static ScrollableList firstList;
		public static ScrollableList secondList;

		public static void Main(string[] args)
		{
			output = new Output();
			navigation = new Navigation();
			ContextHelper.SwitchToScreen(Context.First);
			Keyboard input = new Keyboard();

			Button[] buttons = new Button[] { new Button("File", new Coord(0, 0)),
											  new Button("Edit", new Coord(5, 0)),
											  new Button("Options", new Coord(10, 0)) };

			firstList = new ScrollableList(Context.First);
			secondList = new ScrollableList(Context.Second);
		}
	}
}