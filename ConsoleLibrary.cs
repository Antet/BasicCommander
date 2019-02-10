using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace BasicCommander
{
	/// <summary>
	/// 
	/// http://msdn.microsoft.com/en-us/library/ms682073(VS.85).aspx
	/// The following functions are used to access a console.
	/// 
	/// </summary>
	public class ConsoleLibrary
	{
		// http://pinvoke.net/default.aspx/kernel32/AddConsoleAlias.html
		[DllImport("kernel32", SetLastError = true)]
		public static extern bool AddConsoleAlias(
			string Source,
			string Target,
			string ExeName
			);

		// http://pinvoke.net/default.aspx/kernel32/AllocConsole.html
		[DllImport("kernel32", SetLastError = true)]
		public static extern bool AllocConsole();

		// http://pinvoke.net/default.aspx/kernel32/AttachConsole.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool AttachConsole(
			uint dwProcessId
			);

		// http://pinvoke.net/default.aspx/kernel32/CreateConsoleScreenBuffer.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateConsoleScreenBuffer(
			uint dwDesiredAccess,
			uint dwShareMode,
			IntPtr lpSecurityAttributes,
			uint dwFlags,
			IntPtr lpScreenBufferData
			);

		// http://pinvoke.net/default.aspx/kernel32/FillConsoleOutputAttribute.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FillConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort wAttribute,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfAttrsWritten
			);

		// http://pinvoke.net/default.aspx/kernel32/FillConsoleOutputCharacter.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FillConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			char cCharacter,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten
			);

		// http://pinvoke.net/default.aspx/kernel32/FlushConsoleInputBuffer.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool FlushConsoleInputBuffer(
			IntPtr hConsoleInput
			);

		// http://pinvoke.net/default.aspx/kernel32/FreeConsole.html
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		public static extern bool FreeConsole();

		// http://pinvoke.net/default.aspx/kernel32/GenerateConsoleCtrlEvent.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GenerateConsoleCtrlEvent(
			uint dwCtrlEvent,
			uint dwProcessGroupId
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleAlias.html
		[DllImport("kernel32", SetLastError = true)]
		public static extern bool GetConsoleAlias(
			string Source,
			out StringBuilder TargetBuffer,
			uint TargetBufferLength,
			string ExeName
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleAliases.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleAliases(
			StringBuilder[] lpTargetBuffer,
			uint targetBufferLength,
			string lpExeName
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleAliasesLength.html
		[DllImport("kernel32", SetLastError = true)]
		public static extern uint GetConsoleAliasesLength(
			string ExeName
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleAliasExes.html
		[DllImport("kernel32", SetLastError = true)]
		public static extern uint GetConsoleAliasExes(
			out StringBuilder ExeNameBuffer,
			uint ExeNameBufferLength
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleAliasExesLength.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleAliasExesLength();

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleCP.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleCP();

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleCursorInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			out CONSOLE_CURSOR_INFO lpConsoleCursorInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleDisplayMode.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleDisplayMode(
			out uint ModeFlags
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleFontSize.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern Coord GetConsoleFontSize(
			IntPtr hConsoleOutput,
			Int32 nFont
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleHistoryInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleHistoryInfo(
			out CONSOLE_HISTORY_INFO ConsoleHistoryInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleMode.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleMode(
			IntPtr hConsoleHandle,
			out uint lpMode
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleOriginalTitle.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleOriginalTitle(
			out StringBuilder ConsoleTitle,
			uint Size
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleOutputCP.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleOutputCP();

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleProcessList.html
		// TODO: Test - what's an out uint[] during interop? This probably isn't quite right, but provides a starting point:
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleProcessList(
			out uint[] ProcessList,
			uint ProcessCount
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleScreenBufferInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleScreenBufferInfo(
			IntPtr hConsoleOutput,
			out ConsoleScreenBufferInfo lpConsoleScreenBufferInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleScreenBufferInfoEx.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleScreenBufferInfoEx(
			IntPtr hConsoleOutput,
			ref ConsoleScreenBufferInfoEx ConsoleScreenBufferInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleSelectionInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetConsoleSelectionInfo(
			CONSOLE_SELECTION_INFO ConsoleSelectionInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleTitle.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetConsoleTitle(
			[Out] StringBuilder lpConsoleTitle,
			uint nSize
			);

		// http://pinvoke.net/default.aspx/kernel32/GetConsoleWindow.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetConsoleWindow();

		// http://pinvoke.net/default.aspx/kernel32/GetCurrentConsoleFont.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetCurrentConsoleFont(
			IntPtr hConsoleOutput,
			bool bMaximumWindow,
			out CONSOLE_FONT_INFO lpConsoleCurrentFont
			);

		// http://pinvoke.net/default.aspx/kernel32/GetLargestConsoleWindowSize.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern Coord GetLargestConsoleWindowSize(
			IntPtr hConsoleOutput
			);

		// http://pinvoke.net/default.aspx/kernel32/GetNumberOfConsoleInputEvents.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNumberOfConsoleInputEvents(
			IntPtr hConsoleInput,
			out uint lpcNumberOfEvents
			);

		// http://pinvoke.net/default.aspx/kernel32/GetNumberOfConsoleMouseButtons.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNumberOfConsoleMouseButtons(
			ref uint lpNumberOfMouseButtons
			);

		// http://pinvoke.net/default.aspx/kernel32/GetStdHandle.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetStdHandle(
			int nStdHandle
			);

		// http://pinvoke.net/default.aspx/kernel32/HandlerRoutine.html
		// Delegate type to be used as the Handler Routine for SCCH
		public delegate bool ConsoleCtrlDelegate(CtrlTypes CtrlType);

		// http://pinvoke.net/default.aspx/kernel32/PeekConsoleInput.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool PeekConsoleInput(
			IntPtr hConsoleInput,
			[Out] INPUT_RECORD[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead
			);

		// http://pinvoke.net/default.aspx/kernel32/ReadConsole.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsole(
			IntPtr hConsoleInput,
			[Out] StringBuilder lpBuffer,
			uint nNumberOfCharsToRead,
			out uint lpNumberOfCharsRead,
			IntPtr lpReserved
			);

		// http://pinvoke.net/default.aspx/kernel32/ReadConsoleInput.html
		[DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
		public static extern bool ReadConsoleInput(
			IntPtr hConsoleInput,
			[Out] INPUT_RECORD[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsRead
			);

		// http://pinvoke.net/default.aspx/kernel32/ReadConsoleOutput.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutput(
			IntPtr hConsoleOutput,
			[Out] CharInfo[] lpBuffer,
			Coord bufferSize,
			Coord bufferCoord,
			ref Rect lpReadRegion
			);

		// http://pinvoke.net/default.aspx/kernel32/ReadConsoleOutputAttribute.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			[Out] ushort[] lpAttribute,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfAttrsRead
			);

		// http://pinvoke.net/default.aspx/kernel32/ReadConsoleOutputCharacter.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			[Out] StringBuilder lpCharacter,
			uint nLength,
			Coord dwReadCoord,
			out uint lpNumberOfCharsRead
			);

		// http://pinvoke.net/default.aspx/kernel32/ScrollConsoleScreenBuffer.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ScrollConsoleScreenBuffer(
			IntPtr hConsoleOutput,
		   [In] ref Rect lpScrollRectangle,
			IntPtr lpClipRectangle,
		   Coord dwDestinationOrigin,
			[In] ref CharInfo lpFill
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleActiveScreenBuffer.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleActiveScreenBuffer(
			IntPtr hConsoleOutput
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleCP.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCP(
			uint wCodePageID
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleCtrlHandler.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCtrlHandler(
			ConsoleCtrlDelegate HandlerRoutine,
			bool Add
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleCursorInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCursorInfo(
			IntPtr hConsoleOutput,
			[In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleCursorPosition.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleCursorPosition(
			IntPtr hConsoleOutput,
		   Coord dwCursorPosition
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleDisplayMode.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleDisplayMode(
			IntPtr ConsoleOutput,
			uint Flags,
			out Coord NewScreenBufferDimensions
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleHistoryInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleHistoryInfo(
			CONSOLE_HISTORY_INFO ConsoleHistoryInfo
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleMode.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleMode(
			IntPtr hConsoleHandle,
			uint dwMode
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleOutputCP.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleOutputCP(
			uint wCodePageID
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleScreenBufferInfoEx.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleScreenBufferInfoEx(
			IntPtr ConsoleOutput,
			ConsoleScreenBufferInfoEx ConsoleScreenBufferInfoEx
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleScreenBufferSize.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleScreenBufferSize(
			IntPtr hConsoleOutput,
			Coord dwSize
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleTextAttribute.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleTextAttribute(
			IntPtr hConsoleOutput,
		   ushort wAttributes
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleTitle.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleTitle(
			string lpConsoleTitle
			);

		// http://pinvoke.net/default.aspx/kernel32/SetConsoleWindowInfo.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetConsoleWindowInfo(
			IntPtr hConsoleOutput,
			bool bAbsolute,
			[In] ref Rect lpConsoleWindow
			);

		// http://pinvoke.net/default.aspx/kernel32/SetStdHandle.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetStdHandle(
			uint nStdHandle,
			IntPtr hHandle
			);

		// http://pinvoke.net/default.aspx/kernel32/WriteConsole.html
		/// <summary>
		/// 
		/// Write a string to console
		/// 
		/// </summary>
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsole(
			IntPtr hConsoleOutput,
			string lpBuffer,
			uint nNumberOfCharsToWrite,
			out uint lpNumberOfCharsWritten,
			IntPtr lpReserved
			);

		// http://pinvoke.net/default.aspx/kernel32/WriteConsoleInput.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleInput(
			IntPtr hConsoleInput,
			INPUT_RECORD[] lpBuffer,
			uint nLength,
			out uint lpNumberOfEventsWritten
			);

		// http://pinvoke.net/default.aspx/kernel32/WriteConsoleOutput.html
		/// <summary>
		/// Writes a block of characters to a specified rectangular block. The data 
		/// to be written is taken from a correspondingly sized rectangular block at 
		/// a specified location in the source buffer
		/// </summary>
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutput(
			IntPtr hConsoleOutput,
			CharInfo[,] lpBuffer,
			Coord bufferSize,
			Coord bufferCoord,
			ref Rect lpWriteRegion
			);

		// http://pinvoke.net/default.aspx/kernel32/WriteConsoleOutputAttribute.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutputAttribute(
			IntPtr hConsoleOutput,
			ushort[] lpAttribute,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfAttrsWritten
			);

		// http://pinvoke.net/default.aspx/kernel32/WriteConsoleOutputCharacter.html
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteConsoleOutputCharacter(
			IntPtr hConsoleOutput,
			string lpCharacter,
			uint nLength,
			Coord dwWriteCoord,
			out uint lpNumberOfCharsWritten
			);

		[StructLayout(LayoutKind.Sequential)]
		public struct Coord
		{
			public short x;
			public short y;

			public Coord(int X, int Y)
			{
				x = (short)X;
				y = (short)Y;
			}

			public static bool operator ==(Coord coord1, Coord coord2) => coord1.x == coord2.x && coord1.y == coord2.y;
			public static bool operator !=(Coord coord1, Coord coord2) => !(coord1 == coord2);
		}


		public struct Rect
		{
			public short Left;
			public short Top;
			public short Right;
			public short Bottom;

			public Rect(int left, int top, int right, int bottom)
			{
				Left = (short)left;
				Top = (short)top;
				Right = (short)right;
				Bottom = (short)bottom;
			}
		}

		public struct ConsoleScreenBufferInfo
		{

			public Coord screenBufferSize;
			public Coord cursorPosition;
			public short attributes;
			public Rect windowSize;
			public Coord maximumWindowSize;

		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ConsoleScreenBufferInfoEx
		{
			public uint cbSize;
			public Coord screenBufferSize;
			public Coord cursorPosition;
			public short attributes;
			public Rect windowSize;
			public Coord maximumWindowSize;

			public ushort wPopupAttributes;
			public bool bFullscreenSupported;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public COLORREF[] ColorTable;

			public static ConsoleScreenBufferInfoEx Create()
			{
				return new ConsoleScreenBufferInfoEx { cbSize = 96 };
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct COLORREF
		{
			public uint ColorDWORD;

			public COLORREF(Color color)
			{
				ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
			}

			public Color GetColor()
			{
				return Color.FromArgb((int)(0x000000FFU & ColorDWORD),
				   (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
			}

			public void SetColor(Color color)
			{
				ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
			}
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct CONSOLE_FONT_INFO
		{
			public int nFont;
			public Coord dwFontSize;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct INPUT_RECORD
		{
			[FieldOffset(0)]
			public ushort EventType;
			[FieldOffset(4)]
			public KEY_EVENT_RECORD KeyEvent;
			[FieldOffset(4)]
			public MOUSE_EVENT_RECORD MouseEvent;
			[FieldOffset(4)]
			public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
			[FieldOffset(4)]
			public MENU_EVENT_RECORD MenuEvent;
			[FieldOffset(4)]
			public FOCUS_EVENT_RECORD FocusEvent;
		};

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct KEY_EVENT_RECORD
		{
			[FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
			public bool bKeyDown;
			[FieldOffset(4), MarshalAs(UnmanagedType.U2)]
			public ushort wRepeatCount;
			[FieldOffset(6), MarshalAs(UnmanagedType.U2)]
			//public VirtualKeys wVirtualKeyCode;
			public ushort wVirtualKeyCode;
			[FieldOffset(8), MarshalAs(UnmanagedType.U2)]
			public ushort wVirtualScanCode;
			[FieldOffset(10)]
			public char UnicodeChar;
			[FieldOffset(12), MarshalAs(UnmanagedType.U4)]
			//public ControlKeyState dwControlKeyState;
			public uint dwControlKeyState;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MOUSE_EVENT_RECORD
		{
			public Coord dwMousePosition;
			public uint dwButtonState;
			public uint dwControlKeyState;
			public uint dwEventFlags;
		}

		public struct WINDOW_BUFFER_SIZE_RECORD
		{
			public Coord dwSize;

			public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
			{
				dwSize = new Coord();
				dwSize.x = x;
				dwSize.y = y;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MENU_EVENT_RECORD
		{
			public uint dwCommandId;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FOCUS_EVENT_RECORD
		{
			public uint bSetFocus;
		}

		//CHAR_INFO struct, which was a union in the old days
		// so we want to use LayoutKind.Explicit to mimic it as closely
		// as we can
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct CharInfo
		{
			[FieldOffset(0)]
			char UnicodeChar;
			[FieldOffset(0)]
			char AsciiChar;
			[FieldOffset(2)] //2 bytes seems to work properly
			UInt16 Attributes;

			public CharInfo(char _char, CharAttributes attributes = CharAttributes.empty)
			{
				UnicodeChar = _char;
				AsciiChar = _char;
				Attributes = (ushort)attributes;
			}
			public CharInfo(char _char, ushort color)
			{
				UnicodeChar = _char;
				AsciiChar = _char;
				Attributes = color;
			}

			public static readonly CharInfo Empty = new CharInfo('\0', CharAttributes.empty);
		}

		public static CharInfo[,] StringToCharInfo(string _string, CharAttributes attributes = CharAttributes.empty)
		{
			int x = 0, y = 0;
			// CHANGE IF YOU WANT TO HAVE EXTRA LINES
			CharInfo[,] charReturn = new CharInfo[_string.Length, 1];

			foreach (char _char in _string.ToCharArray())
			{
				if (_char != '\n')
					charReturn[x++, y] = new CharInfo(_char, attributes);
				else
				{
					y++;
					continue;
				}

			}

			return charReturn;
		}

		public enum CharAttributes
		{
			empty = 0x0000,

			foreground_darkblue = 0x0001,
			foreground_darkgreen = 0x0002,
			foreground_darkcyan = 0x0002,
			foreground_darkred = 0x0004,
			foreground_darkpink = 0x0005,
			foreground_darkyellow = 0x0006,
			foreground_normal = 0x0007,
			foreground_gray = 0x0008,
			foreground_blue = 0x0009,
			foreground_green = 0x000A,
			foreground_cyan = 0x000B,
			foreground_red = 0x000C,
			foreground_pink = 0x000D,
			foreground_yellow = 0x000E,
			foreground_white = 0x000F,

			background_darkblue = 0x0010,
			background_darkgreen = 0x0020,
			background_darkcyan = 0x0020,
			background_darkred = 0x0040,
			background_darkpink = 0x0050,
			background_darkyellow = 0x0060,
			background_normal = 0x0070,
			background_gray = 0x0080,
			background_blue = 0x0090,
			background_green = 0x00A0,
			background_cyan = 0x00B0,
			background_red = 0x00C0,
			background_pink = 0x00D0,
			background_yellow = 0x00E0,
			background_white = 0x00F0,

			common_lvb_leading_byte = 0x0100,
			common_lvb_trailing_byte = 0x0200,
			common_lvb_grid_horizontal = 0x0400,
			common_lvb_grid_lvertical = 0x0800,
			common_lvb_grid_rvertical = 0x1000,
			common_lvb_reverse_video = 0x4000,
			common_lvb_underscore = 0x8000
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CONSOLE_CURSOR_INFO
		{
			public uint Size;
			public bool Visible;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CONSOLE_HISTORY_INFO
		{
			ushort cbSize;
			ushort HistoryBufferSize;
			ushort NumberOfHistoryBuffers;
			uint dwFlags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CONSOLE_SELECTION_INFO
		{
			uint Flags;
			Coord SelectionAnchor;
			Rect Selection;

			// Flags values:
			const uint CONSOLE_MOUSE_DOWN = 0x0008; // Mouse is down
			const uint CONSOLE_MOUSE_SELECTION = 0x0004; //Selecting with the mouse
			const uint CONSOLE_NO_SELECTION = 0x0000; //No selection
			const uint CONSOLE_SELECTION_IN_PROGRESS = 0x0001; //Selection has begun
			const uint CONSOLE_SELECTION_NOT_EMPTY = 0x0002; //Selection rectangle is not empty
		}

		// Enumerated type for the control messages sent to the handler routine
		public enum CtrlTypes : uint
		{
			CTRL_C_EVENT = 0,
			CTRL_BREAK_EVENT,
			CTRL_CLOSE_EVENT,
			CTRL_LOGOFF_EVENT = 5,
			CTRL_SHUTDOWN_EVENT
		}
	}
}