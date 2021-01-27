using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Navigation;
using static BasicCommander.Output;

namespace BasicCommander
{
	class Keyboard
	{
		private Command[] commands = new Command[] { new Command(Key(ConsoleKey.X, ConsoleModifiers.Control, '\u0018'), Commands.Exit, Context.Full),
													 new Command(Key(ConsoleKey.Tab, _char: '\t'), Commands.SwapScreen, Context.Full),

													 new Command(Key(ConsoleKey.DownArrow), Commands.MoveCursorDown, Context.Full),
													 new Command(Key(ConsoleKey.UpArrow), Commands.MoveCursorUp, Context.Full),
													 new Command(Key(ConsoleKey.PageDown), Commands.ScrollDown, Context.First),
													 new Command(Key(ConsoleKey.PageUp), Commands.ScrollUp, Context.First),

													 new Command(Key(ConsoleKey.Enter, _char: '\r'), Commands.ChangeDirectory, Context.Full),
												   };

		public static event EventHandler keyPress;

		private void OnKeyPress()
		{
			if (keyPress != null)
				keyPress(this, EventArgs.Empty);
		}

		public Keyboard()
		{
			//Console.TreatControlCAsInput = true;

			Thread thread = new Thread(KeyboardUpdate);
			thread.Start();
		}

		private void KeyboardUpdate()
		{
			while (true)
				HandleKey(Console.ReadKey(true));
		}

		private void HandleKey(ConsoleKeyInfo pressedKey)
		{
			OnKeyPress();
			foreach (Command currentCommand in commands)
			{
				if (pressedKey.Equals(currentCommand.key) && (Program.navigation.GetCurrentScreen() == currentCommand.screen || currentCommand.screen.Equals(Context.Full)))
					currentCommand.command.Invoke();
			}

			// switch (GetCurrentScreen())
			// {
			// 	// Toolbar
			// 	case (Context.Toolbar):
			// 		if (pressedKey.Key == ConsoleKey.Escape)
			// 			SwitchToScreen(Context.First);
			// 		if (pressedKey.Key == ConsoleKey.RightArrow)
			// 		{
			// 			ButtonCollection.Button nextButton = ButtonCollection.GetNextButton();
			// 			SetCursor(nextButton.position.X, nextButton.position.Y);
			// 		}
			// 		if (pressedKey.Key == ConsoleKey.LeftArrow)
			// 		{
			// 			ButtonCollection.Button prevButton = ButtonCollection.GetPreviousButton();
			// 			SetCursor(prevButton.position.X, prevButton.position.Y);
			// 		}
			// 		break;
			// 	// First screen
			// 	case (Context.First):
			// 		if (pressedKey.Key == ConsoleKey.Escape)
			// 			SwitchToScreen(Context.Toolbar);
			// 		if (pressedKey.Key == ConsoleKey.Enter)
			// 			LabelCollection.ChangeDirectory();
			// 		if (pressedKey.Key == ConsoleKey.UpArrow)
			// 			MoveCursorBy(0, 1);
			// 		if (pressedKey.Key == ConsoleKey.DownArrow)
			// 			MoveCursorBy(0, -1);
			// 		if (pressedKey.Key == ConsoleKey.RightArrow)
			// 			SetCursor(1, 53);
			// 		break;
			// 	// First search
			// 	case (Context.FirstSearch):
			// 		break;
			// 	default:
			// 		break;
			// }
		}

		static ConsoleKeyInfo Key(ConsoleKey key, ConsoleModifiers? mods = null, char _char = '\0')
		{
			if (_char == '\0' && key.ToString().Length == 1)
				_char = key.ToString()[0];

			switch (mods)
			{
				case ConsoleModifiers.Shift:
					return new ConsoleKeyInfo(_char, key, true, false, false);
				case ConsoleModifiers.Alt:
					return new ConsoleKeyInfo(_char, key, false, true, false);
				case ConsoleModifiers.Control:
					return new ConsoleKeyInfo(_char, key, false, false, true);
				default:
					return new ConsoleKeyInfo(_char, key, false, false, false);
			}
		}
	}
}