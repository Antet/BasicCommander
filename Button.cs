using System;
using System.Linq;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Program;

namespace BasicCommander
{
	class Button
	{
		private string name;
		private Coord position;

		public Button(string _name, Coord _position)
		{
			name = _name;
			position = _position;

			DrawButton();

			navigation.cursorMoved += CheckCursorOnButton;
		}

		void DrawButton() => output.WriteToConsole(name, position);

		void CheckCursorOnButton(object sender, CursorEventArgs eventArgs)
		{
			if (navigation.CheckIfCurrentScreenEquals(Context.Toolbar) && eventArgs.newCoord == position)
				output.HighlightRect(position, name.Length);
			else
				output.DeHighlightRect(position, name.Length);
		}
	}
}