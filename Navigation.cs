using System;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Output;

namespace BasicCommander
{
	static class Navigation
	{
		static Coord cursorPosition = new Coord(0, 0);
		static Screen currentScreen = Screen.First;

		static Rect windowBounds = new Rect { };
		static Rect toolbarBounds = new Rect(0, 0, 80, 0);
		static Rect firstBounds = new Rect(1, 4, 78, 56);

		public static void Initialize()
		{
			ConsoleScreenBufferInfoEx consoleInfo = ConsoleScreenBufferInfoEx.Create();
			GetConsoleScreenBufferInfoEx(screenBuffer, ref consoleInfo);
			windowBounds = consoleInfo.srWindow;

			SwitchToScreen(Screen.First);
		}

		public static void SetCursor(int x, int y)
		{
			// if (!CheckIfCanMove(x, y))
			// 	return;

			SetConsoleCursorPosition(screenBuffer, new Coord(x, y));
			cursorPosition.X = (short)x;
			cursorPosition.Y = (short)y;
			SetCurrentScreen(x, y);
			LabelCollection.LabelHighlightCheck();
		}

		public static void MoveCursorBy(int x, int y)
		{
			if (!CheckIfCanMove(x + cursorPosition.X, -y + cursorPosition.Y))
				return;

			SetConsoleCursorPosition(screenBuffer, new Coord(x + cursorPosition.X, -y + cursorPosition.Y));
			cursorPosition.X += (short)x;
			cursorPosition.Y += (short)-y;
			SetCurrentScreen(cursorPosition.X, cursorPosition.Y);
			LabelCollection.LabelHighlightCheck();
		}

		static bool CheckIfCanMove(int x, int y)
		{
			if (x < windowBounds.Left || x > windowBounds.Right || y < windowBounds.Top || y > windowBounds.Bottom)
				return false;
			switch (GetCurrentScreen())
			{
				case Screen.Toolbar:
					if (x < toolbarBounds.Left || x > toolbarBounds.Right || y < toolbarBounds.Top || y > toolbarBounds.Bottom)
						return false;
					break;
				case Screen.First:
					if (x < firstBounds.Left || x > firstBounds.Right || y < firstBounds.Top || y > firstBounds.Bottom)
						return false;
					break;
				default:
					break;
			}
			return true;
		}

		public static Coord GetCursorPosition()
		{
			return cursorPosition;
		}

		public static Screen GetCurrentScreen() => currentScreen;

		static void SetCurrentScreen(int x, int y)
		{
			WriteToConsole(string.Format("{0:D2}, {1:D2}", cursorPosition.X, cursorPosition.Y), 155, 59);

			if (x < 80 && y == 0)
				currentScreen = Screen.Toolbar;
			else if (x < 80 && y == 3)
				currentScreen = Screen.FirstSearch;
			else if (x < 80 && y > 3 && y < 57)
				currentScreen = Screen.First;
		}

		public static void SwitchToScreen(Screen switchScreen)
		{
			switch (switchScreen)
			{
				case Screen.Toolbar:
					SetCursor(0, 0);
					break;
				case Screen.First:
					SetCursor(1, 4);
					break;
				case Screen.FirstSearch:
					SetCursor(2, 2);
					break;
				default:
					break;
			}
		}

		public enum Screen
		{
			First,
			FirstSearch,
			Second,
			SecondSearch,
			Toolbar,
			Shortcuts
		}
	}
}