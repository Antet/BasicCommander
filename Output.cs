using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using static BasicCommander.ConsoleLibrary;

namespace BasicCommander
{
	class Output
	{
		static IntPtr screenBuffer;
		static Rect toDo_FindOutWhatThisIs;
		static COORD cursorPosition = new COORD(0, 0);

		public static void Initialize()
		{
			CreateVideoThread();

			Console.Clear();
			screenBuffer = CreateConsoleScreenBuffer(0xC0000000, 0x00000003, IntPtr.Zero, 1, IntPtr.Zero);
			SetConsoleScreenBufferSize(screenBuffer, new COORD(160, 60));

			Rect screen = new Rect(0, 0, 159, 59);

			SetConsoleWindowInfo(screenBuffer, true, ref screen);
			SetConsoleActiveScreenBuffer(screenBuffer);

			GenerateMainScreen();
			//WriteToConsole("This is a message", 50, 50);
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
				Console.Clear();

				Thread.Yield();
			}
		}

		static void GenerateMainScreen()
		{
			//column, row
			CHAR_INFO[,] charactersToWrite = new CHAR_INFO[,] { {new CHAR_INFO('F', CharAttributes.background_gray), new CHAR_INFO('i', CharAttributes.background_gray),
																 new CHAR_INFO('l', CharAttributes.background_gray), new CHAR_INFO('e', CharAttributes.background_gray),
																 CHAR_INFO.Empty,
																 new CHAR_INFO('E', CharAttributes.background_gray), new CHAR_INFO('d', CharAttributes.background_gray),
																 new CHAR_INFO('i', CharAttributes.background_gray), new CHAR_INFO('t', CharAttributes.background_gray)} };

			toDo_FindOutWhatThisIs = new Rect { Left = 0, Right = (short)(charactersToWrite.GetLength(1) - 1), Top = 0, Bottom = (short)(charactersToWrite.GetLength(0) - 1) };
			WriteConsoleOutput(screenBuffer,
			charactersToWrite,
			new COORD((short)charactersToWrite.GetLength(1), (short)charactersToWrite.GetLength(0)),
			new COORD(0, 0),
			ref toDo_FindOutWhatThisIs
			);
		}

		public static void ChangeBackGroundColor(CharAttributes backgroundColor)
		{
			CHAR_INFO[,] charactersToWrite = new CHAR_INFO[60, 160];
			for (int i = 0; i < 60 * 160; i++) charactersToWrite[i % 60, i / 60] = new CHAR_INFO('\0', backgroundColor);
			//Enumerable.Repeat(new CHAR_INFO('x', backgroundColor), 13).ToArray();

			toDo_FindOutWhatThisIs = new Rect { Left = 0, Right = (short)(charactersToWrite.GetLength(1) - 1), Top = 0, Bottom = (short)(charactersToWrite.GetLength(0) - 1) };
			WriteConsoleOutput(screenBuffer,
			charactersToWrite,
			new COORD((short)charactersToWrite.GetLength(1), (short)charactersToWrite.GetLength(0)),
			new COORD(0, 0),
			ref toDo_FindOutWhatThisIs
			);

			GenerateMainScreen();
		}

		static void WriteToConsole(string _string, short x = 0, short y = 0)
		{
			foreach (char _char in _string.ToCharArray())
				WriteConsoleOutputCharacter(screenBuffer, _char.ToString(), 1, new COORD(x++, y), out var empty1);
		}

		public static void MoveCursor(int x, int y)
		{
			SetConsoleCursorPosition(screenBuffer, new COORD(x, y));
			cursorPosition.X += (short)x;
			cursorPosition.Y += (short)y;
		}

		public static void MoveCursorBy(int x, int y)
		{
			SetConsoleCursorPosition(screenBuffer, new COORD(x + cursorPosition.X, -y + cursorPosition.Y));
			cursorPosition.X += (short)x;
			cursorPosition.Y += (short)-y;
		}
	}
}
