using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Navigation;

namespace BasicCommander
{
	class Output
	{
		public static IntPtr screenBuffer;
		static ConsoleScreenBufferInfoEx consoleInfo;

		static int messageCounter = 0;

		public static void Initialize()
		{
			screenBuffer = CreateConsoleScreenBuffer(0xC0000000, 0x00000003, IntPtr.Zero, 1, IntPtr.Zero);
			SetConsoleScreenBufferSize(screenBuffer, new Coord(161, 60));
			SetConsoleActiveScreenBuffer(screenBuffer);

			Rect windowSize = new Rect(0, 0, 160, 59);
			SetConsoleWindowInfo(screenBuffer, true, ref windowSize);

			GetConsoleScreenBufferInfoEx(screenBuffer, ref consoleInfo);
			SetConsoleTitle("Basic Commander");

			// Draw borders
			DrawLine(Screen.First, 80, 0, 80, 57);
			DrawLine(Screen.First, 0, 1, 161, 1);
			DrawLine(Screen.First, 0, 57, 161, 57);
			CreateVideoThread();
		}

		static void CreateVideoThread()
		{
			Thread th = new Thread(VideoUpdate);
			th.Name = "VideoThread";
			//th.Start();
		}

		static void VideoUpdate()
		{
			while (true)
			{
			}
		}

		public static void Message(string message)
		{
			WriteToConsole("Nr. " + messageCounter + ": " + message, 120, 58);
		}

		public static void ChangeBackgroundColor(System.Drawing.Color backgroundColor)
		{
			consoleInfo.ColorTable[0].SetColor(backgroundColor);

			SetConsoleScreenBufferInfoEx(screenBuffer, consoleInfo);
		}

		public static void ChangeTextColor(System.Drawing.Color textColor)
		{
			consoleInfo.ColorTable[15].SetColor(textColor);

			SetConsoleScreenBufferInfoEx(screenBuffer, consoleInfo);
		}

		/*public static void WriteToConsole(Screen screen, string _string, int x = 0, int y = 0, bool highlight = false)
		{
			if (!highlight)
			{
				foreach (char _char in _string.ToCharArray())
					WriteConsoleOutputCharacter(screenBuffer, _char.ToString(), 1, new Coord(x++, y), out var nrCharsWritten);
			}
			else
			{
				foreach (char _char in _string.ToCharArray())
					WriteConsoleOutputCharacter(screenBuffer, _char.ToString(), 1, new Coord(x++, y), out var nrCharsWritten);
			}
		}*/

		static void DrawLine(Screen screen, int startx, int starty, int endx, int endy)
		{
			for (int i = startx; i < endx; i++)
				WriteConsoleOutputCharacter(screenBuffer, ('X').ToString(), 1, new Coord(i, starty), out var empty0);
			for (int i = starty; i < endy; i++)
				WriteConsoleOutputCharacter(screenBuffer, ('X').ToString(), 1, new Coord(startx, i), out var empty0);
		}

		public static void HighlightRect(int x, int y, int length) => FillConsoleOutputAttribute(screenBuffer, (ushort)CharAttributes.background_gray, 75, new Coord(x, y), out var z);
		public static void DeHighlightRect(int x, int y, int length) => FillConsoleOutputAttribute(screenBuffer, (ushort)CharAttributes.foreground_white, 75, new Coord(x, y), out var z);

		public static void WriteToConsole(string _string, int x, int y, CharAttributes attributes = CharAttributes.foreground_white)
		{
			CharInfo[,] charactersToWrite = new CharInfo[_string.Length, 1];

			charactersToWrite = StringToCharInfo(_string, attributes);

			Rect writeRect = new Rect { Left = (short)x, Right = 160, Top = (short)y, Bottom = 59 };
			WriteConsoleOutput(screenBuffer, charactersToWrite,
			new Coord((short)_string.Length, (short)1),
			new Coord(0, 0),
			ref writeRect
			);
		}

		public static void DrawRect(int left, int top, int right, int bottom)
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

		public static void ClearScreen(Screen screenToClear)
		{
			switch (screenToClear)
			{
				case Screen.First:
					DrawRect(0, 2, 80, 55);
					break;
			}
		}
	}
}
