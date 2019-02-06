using System;
using System.Linq;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Navigation;
using static BasicCommander.Output;

namespace BasicCommander
{
	static class ButtonCollection
	{
		public struct Button
		{
			public string label;
			public Coord position;

			public Button(string _label, int _x, int _y)
			{
				label = _label;
				position = new Coord(_x, _y);
			}
		}

		public static Button[] buttons = new Button[] { new Button("File", 0, 0),
												 		new Button("Edit", 5, 0),
												 		new Button("Options", 10, 0) };

		public static void Initialize()
		{
			DrawAllButtons();
		}

		static void DrawAllButtons()
		{
			foreach (Button button in buttons)
				WriteToConsole(button.label, button.position.X, button.position.Y);
		}

		static void DrawButton(Button drawButton)
		{
		}

		static Button GetCurrentButton()
		{
			Coord currentCursorPosition = GetCursorPosition();

			foreach (Button button in buttons)
			{
				if (currentCursorPosition.X >= button.position.X && currentCursorPosition.X <= button.label.Length + button.position.X && currentCursorPosition.Y == button.position.Y)
					return button;
			}

			return default(Button);
		}

		static Button GetCurrentButton(Coord position)
		{
			foreach (Button button in buttons)
			{
				if (position.X >= button.position.X && position.X <= button.label.Length + button.position.X && position.Y == button.position.Y)
				{
					return button;
				}
			}

			return default(Button);
		}

		public static Button GetNextButton()
		{
			for (int i = GetCurrentButton().position.X + GetCurrentButton().label.Length + 1; ; i++)
			{
				if (IsOnButton(new Coord(i, 0)))
					return GetCurrentButton(new Coord(i, 0));
				i = -1;
			}
		}

		public static Button GetPreviousButton()
		{
			for (int i = GetCurrentButton().position.X - 1; ; i--)
			{
				if (IsOnButton(new Coord(i, 0)))
					return GetCurrentButton(new Coord(i, 0));
				i = buttons.Last().position.X + 1;
			}
		}

		static bool IsOnButton()
		{
			Coord currentCursorPosition = GetCursorPosition();

			foreach (Button button in buttons)
			{
				if (currentCursorPosition.X >= button.position.X && currentCursorPosition.X <= button.label.Length + button.position.X && currentCursorPosition.Y == button.position.Y)
					return true;
			}

			return false;
		}

		static bool IsOnButton(Coord position)
		{
			foreach (Button button in buttons)
			{
				if (position.X >= button.position.X && position.X <= button.label.Length + button.position.X && position.Y == button.position.Y)
					return true;
			}

			return false;
		}
	}
}