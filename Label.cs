using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Program;

namespace BasicCommander
{
	class Label
	{
		public string name { get; private set; }
		public string dirPath { get; private set; }
		public long fileSize { get; private set; }
		public Coord position { get; private set; }

		public Label(string _name, string _dirPath, Coord _position, long _fileSize = 0)
		{
			name = _name;
			dirPath = _dirPath;
			fileSize = _fileSize;
			position = _position;

			navigation.cursorMoved += HighlightCheck;
		}

		private bool IsCursorOnThisLabel()
		{
			if (navigation.GetCursorPosition() == new Coord(position.x - 1, position.y))
				return true;
			return false;
		}

		public void HighlightCheck(object sender, CursorEventArgs eventArgs)
		{
			if (IsCursorOnThisLabel())
				output.HighlightRect(new Coord(position.x - 1, position.y), 77);
			else
				output.DeHighlightRect(new Coord(position.x - 1, position.y), 77);
		}

		public void ModifyPosition(int _position)
		{
			position = new Coord(position.x, _position);

			if (position.y == -1)
				navigation.cursorMoved -= HighlightCheck;
			else
			{
				navigation.cursorMoved -= HighlightCheck;
				navigation.cursorMoved += HighlightCheck;
			}
		}
	}
}