using System;

namespace BasicCommander
{
	public class CursorEventArgs : EventArgs
	{
		public ConsoleLibrary.Coord newCoord { get; }

		public CursorEventArgs(ConsoleLibrary.Coord _newCoord) => newCoord = _newCoord;
	}
}