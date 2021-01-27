using System;

namespace BasicCommander
{
	class Commands
	{
		public static void Exit() => Environment.Exit(0);

		public static void SelectNextButton() => Program.navigation.SelectNextButton();

		public static void MoveCursorDown() => Program.navigation.MoveCursorBy(0, 1);
		public static void MoveCursorUp() => Program.navigation.MoveCursorBy(0, -1);
		public static void ScrollDown() => Program.firstList.ScrollDown();
		public static void ScrollUp() => Program.firstList.ScrollUp();

		public static void SwapScreen() => Program.navigation.SwapScreen();
		public static void ChangeDirectory() => Program.navigation.GetCurrentList().ChangeDirectory();
	}
}