using System;
using System.Threading;

namespace BasicCommander
{
	class Keyboard
	{
		public static void Initialize()
		{
			//Console.TreatControlCAsInput = true;

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
				case ConsoleModifiers.Control:
					switch (pressedKey.Key)
					{
						case ConsoleKey.X:
							Environment.Exit(0);
							break;
					}
					break;
				case ConsoleModifiers.Alt:
					switch (pressedKey.Key)
					{
						case ConsoleKey.F1:
							Output.ChangeBackGroundColor(ConsoleLibrary.CharAttributes.empty);
							Console.ForegroundColor = ConsoleColor.DarkGreen;
							break;
						case ConsoleKey.F2:
							Output.ChangeBackGroundColor(ConsoleLibrary.CharAttributes.background_blue);
							Console.ForegroundColor = ConsoleColor.White;
							break;
					}
					break;
				default:
					switch (pressedKey.Key)
					{
						case ConsoleKey.Tab:
							Output.MoveCursorBy(5, 0);
							break;
						case ConsoleKey.UpArrow:
							Output.MoveCursorBy(0, 1);
							break;
						case ConsoleKey.DownArrow:
							Output.MoveCursorBy(0, -1);
							break;
					}
					break;
			}
		}
	}
}