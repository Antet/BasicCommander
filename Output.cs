using System;
using System.Drawing;
using static BasicCommander.ConsoleLibrary;

namespace BasicCommander
{
    class Output
	{
		public readonly IntPtr screenBuffer;
		public readonly ConsoleScreenBufferInfoEx consoleInfo;

		int messageCounter = 0;

		public Output()
		{
			screenBuffer = CreateConsoleScreenBuffer(0xC0000000, 0x00000003, IntPtr.Zero, 1, IntPtr.Zero);
			SetConsoleScreenBufferSize(screenBuffer, new Coord(161, 60));
			SetConsoleActiveScreenBuffer(screenBuffer);

			Rect windowSize = new Rect(0, 0, 160, 59);
			SetConsoleWindowInfo(screenBuffer, true, ref windowSize);

			consoleInfo = ConsoleScreenBufferInfoEx.Create();
			GetConsoleScreenBufferInfoEx(screenBuffer, ref consoleInfo);
			SetConsoleTitle("Basic Commander");

			// Draw borders
			DrawLine(80, 0, 80, 57);
			DrawLine(0, 1, 161, 1);
			DrawLine(0, 57, 161, 57);
		}

		// Character writing
		public void Message(object message) => WriteToConsole($"Nr. {messageCounter++}: {message}", new Coord(1, 58));

		public void WriteToConsole(string _string, Coord coord, CharAttributes attributes = CharAttributes.foreground_normal)
		{
			Rect writeRect = new Rect { Left = coord.x, Right = 160, Top = coord.y, Bottom = 59 };

			WriteConsoleOutput(screenBuffer,
							   StringToCharInfo(_string, attributes),
							   new Coord((short)_string.Length, (short)1),
							   new Coord(0, 0),
							   ref writeRect
							   );
		}

		public void DrawLine(int startx, int starty, int endx, int endy)
		{
			for (int i = startx; i < endx; i++)
				SetAttributeRect(new Coord(i, starty), 1, CharAttributes.background_white);
			for (int i = starty; i < endy; i++)
				SetAttributeRect(new Coord(startx, i), 1, CharAttributes.background_white);
		}

		// Rect drawing
		public void HighlightRect(Coord coord, int length) => FillConsoleOutputAttribute(screenBuffer, (ushort)CharAttributes.background_gray, (uint)length, coord, out var z);
		public void DeHighlightRect(Coord coord, int length) => FillConsoleOutputAttribute(screenBuffer, (ushort)CharAttributes.foreground_normal, (uint)length, coord, out var z);
		public void SetAttributeRect(Coord coord, int length, CharAttributes attribute) => FillConsoleOutputAttribute(screenBuffer, (ushort)attribute, (uint)length, coord, out var z);

		public void DrawRect(int left, int top, int right, int bottom)
		{
			CharInfo[,] charactersToWrite = new CharInfo[right - left, bottom - top];
			for (int i = 0; i < (right - left) * (bottom - top); i++)
				charactersToWrite[i % (right - left), i / (right - left)] = new CharInfo('\0', CharAttributes.foreground_white);

			Rect writeRect = new Rect { Left = (short)left, Right = 160, Top = (short)top, Bottom = 59 };

			WriteConsoleOutput(screenBuffer, charactersToWrite,
							   new Coord((short)right - left, (short)bottom - top),
							   new Coord(0, 0),
							   ref writeRect
							   );
		}

		public void ChangeBackgroundColor(Color backgroundColor)
		{
			consoleInfo.ColorTable[0].SetColor(backgroundColor);

			SetConsoleScreenBufferInfoEx(screenBuffer, consoleInfo);
		}

		public void ChangeTextColor(Color textColor)
		{
			consoleInfo.ColorTable[15].SetColor(textColor);

			SetConsoleScreenBufferInfoEx(screenBuffer, consoleInfo);
		}

		public void ClearScreen(Context screenToClear)
		{
			switch (screenToClear)
			{
				case Context.First:
					DrawRect(1, 4, 78, 56);
					break;
				case Context.Second:
					DrawRect(82, 4, 159, 56);
					break;
			}
		}
	}
}
