using System;
using System.Threading;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Navigation;
using static BasicCommander.Output;

namespace BasicCommander
{
	class Keyboard
	{
		public static void Initialize()
		{
			Console.TreatControlCAsInput = true;

			CreateKeyboardThread();
		}

		static void CreateKeyboardThread()
		{
			Thread th = new Thread(KeyboardUpdate);
			th.Name = "VideoThread";
			th.Start();
		}

		private static void KeyboardUpdate()
		{
			while (true)
			{
				KeyHandler(Console.ReadKey(true));

				Thread.Yield();
			}
		}

		private static void KeyHandler(ConsoleKeyInfo pressedKey)
		{
			switch (pressedKey.Modifiers)
			{
				// Control keys
				case ConsoleModifiers.Control:
					switch (pressedKey.Key)
					{
						case ConsoleKey.S:
							SwitchToScreen(Screen.FirstSearch);
							break;
						case ConsoleKey.X:
							Environment.Exit(0);
							break;
					}
					break;
				// Alt keys
				case ConsoleModifiers.Alt:
					switch (pressedKey.Key)
					{
						case ConsoleKey.F1:
							Output.ChangeBackgroundColor(System.Drawing.Color.Black);
							Output.ChangeTextColor(System.Drawing.Color.FromArgb(255, 192, 192, 192));
							break;
						case ConsoleKey.F2:
							Output.ChangeBackgroundColor(System.Drawing.Color.DarkBlue);
							Output.ChangeTextColor(System.Drawing.Color.Black);
							break;
					}
					break;
				// No modifiers
				default:
					switch (pressedKey.Key)
					{
						case ConsoleKey.F1:
							SwitchToScreen(Screen.First);
							break;
						case ConsoleKey.F2:
							LabelCollection.ChangeDirectory("C:\\Users\\Antonio\\");
							break;
						case ConsoleKey.F3:
							LabelCollection.ChangeDirectory("C:\\Users\\Antonio\\Desktop\\");
							break;
						case ConsoleKey.Enter:
							HighlightRect(2, 3, 75);
							break;
						case ConsoleKey.Escape:
							SwitchToScreen(Screen.Toolbar);
							break;
						case ConsoleKey.Tab:
							SwitchToScreen(Screen.Second);
							break;
						case ConsoleKey.UpArrow:
							MoveCursorBy(0, 1);
							break;
						case ConsoleKey.DownArrow:
							MoveCursorBy(0, -1);
							break;
						case ConsoleKey.RightArrow:
							ButtonCollection.Button nextButton = ButtonCollection.GetNextButton();
							SetCursor(nextButton.position.X, nextButton.position.Y);
							break;
						case ConsoleKey.LeftArrow:
							ButtonCollection.Button prevButton = ButtonCollection.GetPreviousButton();
							SetCursor(prevButton.position.X, prevButton.position.Y);
							break;
					}
					break;
			}
		}
	}
}