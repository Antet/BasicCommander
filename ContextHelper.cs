using System;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Program;

namespace BasicCommander
{
    static class ContextHelper
    {
		public static Context GetCurrentScreen()
		{
			Coord cursorPosition = navigation.GetCursorPosition();

			if (IsInside(firstList.GetBounds(), cursorPosition))
				return Context.First;
			else if (IsInside(secondList.GetBounds(), cursorPosition))
				return Context.Second;
			else if (IsInside(navigation.toolbarBounds, cursorPosition))
				return Context.Toolbar;

			return Context.Full;
		}

		public static bool CheckIfCurrentScreenEquals(Context screen) => GetCurrentScreen().Equals(screen);

		public static void SwitchToScreen(Context newContext)
		{
			switch (newContext)
			{
				case Context.Toolbar:
					navigation.SetCursor(new Coord(0, 0));
					break;
				case Context.First:
					navigation.SetCursor(new Coord(1, 4));
					break;
				case Context.Second:
					navigation.SetCursor(new Coord(82, 4));
					break;
				case Context.FirstSearch:
					navigation.SetCursor(new Coord(2, 2));
					break;
				default:
					break;
			}
		}

		public static void SwapScreen()
		{
			if (IsInside(firstList.GetBounds(), navigation.GetCursorPosition()))
				SwitchToScreen(Context.Second);
			else if (IsInside(secondList.GetBounds(), navigation.GetCursorPosition()))
				SwitchToScreen(Context.First);
		}

		public static bool IsInside(Rect rect, Coord position)
		{
			if (rect.Left <= position.x && rect.Right >= position.x && rect.Top <= position.y && rect.Bottom >= position.y)
				return true;

			return false;
		}
    }
}