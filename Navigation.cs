using System;
using System.Collections.Generic;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Program;
using static BasicCommander.ContextHelper;

namespace BasicCommander
{
	class Navigation
	{
		Rect windowBounds = new Rect { };
		public Rect toolbarBounds = new Rect(0, 0, 80, 0);
		Rect firstBounds = new Rect(1, 4, 78, 55);
		Rect secondBounds = new Rect(82, 4, 159, 56);

		public event EventHandler<CursorEventArgs> cursorMoved;
		public event EventHandler<CursorEventArgs> outOfBounds;

		public Navigation()
		{
			windowBounds = output.consoleInfo.windowSize;
		}

		public void OnCursorMoved(Coord newCoord)
		{
			if (cursorMoved != null && outOfBounds != null)
			{
				// WriteCursorPosition(newCoord);
				cursorMoved(this, new CursorEventArgs(newCoord));
				outOfBounds(this, new CursorEventArgs(newCoord));
			}
		}

		public bool CheckIfCurrentScreenEquals(Context screen) => GetCurrentScreen().Equals(screen);

		public void SwapScreen()
		{
			if (IsInside(firstList.GetBounds(), GetCursorPosition()))
				SwitchToScreen(Context.Second);
			else if (IsInside(secondList.GetBounds(), GetCursorPosition()))
				SwitchToScreen(Context.First);
		}

		public Context GetCurrentScreen()
		{
			Coord cursorPosition = GetCursorPosition();

			if (IsInside(firstList.GetBounds(), cursorPosition))
				return Context.First;
			else if (IsInside(secondList.GetBounds(), cursorPosition))
				return Context.Second;
			else if (IsInside(toolbarBounds, cursorPosition))
				return Context.Toolbar;

			return Context.Full;
		}

		void WriteCursorPosition(Coord coord) => output.WriteToConsole($"{coord.x:D2}, {coord.y:D2}", new Coord(155, 59));

		public void SetCursor(Coord coord)
		{
			if (!IsInside(windowBounds, coord))
				return;
			SetConsoleCursorPosition(output.screenBuffer, coord);

			OnCursorMoved(coord);
		}

		public void MoveCursorBy(int x, int y)
		{
			Coord cursorPosition = GetCursorPosition();
			Coord newCursorPosition = new Coord(x + cursorPosition.x, y + cursorPosition.y);

			if (!IsInside(windowBounds, newCursorPosition) || !IsInside(GetCurrentList().GetBounds(), newCursorPosition))
			{
				OnCursorMoved(newCursorPosition);
				return;
			}
			SetConsoleCursorPosition(output.screenBuffer, newCursorPosition);

			OnCursorMoved(newCursorPosition);
		}

		public Coord GetCursorPosition()
		{
			ConsoleScreenBufferInfoEx consoleInfo = ConsoleScreenBufferInfoEx.Create();

			GetConsoleScreenBufferInfoEx(output.screenBuffer, ref consoleInfo);

			return consoleInfo.cursorPosition;
		}

		public ScrollableList GetCurrentList()
		{
			if (GetCurrentScreen().Equals(Context.First))
				return Program.firstList;
			else if (GetCurrentScreen().Equals(Context.Second))
				return Program.secondList;
			else
				return null;
		}

		public void SelectNextButton()
		{
			// SetCursor();
		}
	}
}