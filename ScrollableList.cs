using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using static BasicCommander.Program;
using static BasicCommander.ConsoleLibrary;

namespace BasicCommander
{
	class ScrollableList
	{
		List<Label> listOfLabels;
		int currentSkipAmount = 0;
		int xPosition;
		Context screen;
		Rect listBounds;
		Coord lastPosition;

		public ScrollableList(Context _screen)
		{
			listOfLabels = new List<Label>();

			SetScreen(_screen);

			PopulateLabelList("C:\\Windows");
			DrawLabels();

			navigation.outOfBounds += CheckForScroll;
			navigation.cursorMoved += SetLastPosition;
		}

		public bool IsValidDirectory(string path)
		{
			try
			{
				Directory.GetDirectories(path, "", SearchOption.TopDirectoryOnly);
			}
			catch (Exception e)
			{
				if (e.HResult == -2147024891)
					output.Message("Access denied.");
				else if (e.HResult == -2147024809)
					output.Message("Not a directory.");
				return false;
			}

			return true;
		}

		public void ChangeDirectory()
		{
			Label currentLabel = GetCurrentLabel();

			if (IsValidDirectory(currentLabel.dirPath))
			{
				PopulateLabelList(currentLabel.dirPath);
				DrawLabels();
			}

			ResetCursor();
		}

		public void PopulateLabelList(string path)
		{
			listOfLabels.Clear();

			output.WriteToConsole(path, new Coord(xPosition, 2));

			if (Directory.GetParent(path) != null)
				listOfLabels.Add(new Label("..", Directory.GetParent(path).ToString(), new Coord(xPosition, listOfLabels.Count + 4)));

			foreach (string dir in Directory.GetDirectories(path, "", SearchOption.TopDirectoryOnly))
				listOfLabels.Add(new Label(dir.Replace(path, string.Empty), dir, new Coord(xPosition, listOfLabels.Count + 4)));

			foreach (string file in Directory.GetFiles(path, "", SearchOption.TopDirectoryOnly))
			{
				FileInfo fileInfo = new FileInfo(file);

				listOfLabels.Add(new Label(fileInfo.Name, fileInfo.FullName, new Coord(xPosition, listOfLabels.Count + 4), fileInfo.Length));
			}
		}

		public void DrawLabels(int skipAmount = 0)
		{
			List<Label> skippedList = listOfLabels.Skip(skipAmount).ToList();
			output.ClearScreen(screen);

			foreach (Label label in skippedList)
			{
				if (skippedList.IndexOf(label) < 52)
				{
					string formattedFileSize = LabelUtility.GetFileSize(label);
					string formattedFileName = LabelUtility.ShortenLabelName(label);

					string formattedLabel = $"{formattedFileName,-34}{formattedFileSize}";

					label.ModifyPosition(skippedList.IndexOf(label) + 4);
					output.WriteToConsole(formattedLabel, new Coord(xPosition, label.position.y));
				}
				else
					label.ModifyPosition(-1);
			}

			SetBounds(new Rect(xPosition - 1, 4, xPosition + 75, skippedList.LastOrDefault(lbl => lbl.position.y == 55)?.position.y ?? skippedList.Last().position.y));
		}

		void SetLastPosition(object sender, CursorEventArgs eventArgs) => lastPosition = eventArgs.newCoord;

		void ResetCursor()
		{
			navigation.SetCursor(new Coord(xPosition - 1, 4));

			listOfLabels[0].HighlightCheck(this, new CursorEventArgs(new Coord(xPosition, 4)));

			currentSkipAmount = 0;
		}

		public Rect GetBounds() => listBounds;
		void SetBounds(Rect newBounds) => listBounds = newBounds;

		void CheckForScroll(object sender, CursorEventArgs eventArgs)
		{
			if (eventArgs.newCoord.y >= 56 && eventArgs.newCoord.x == xPosition - 1)
				ScrollDown();
			else if (eventArgs.newCoord.y <= 3 && eventArgs.newCoord.x == xPosition - 1)
				ScrollUp();
		}

		public void ScrollDown()
		{
			if (currentSkipAmount + 1 >= listOfLabels.Count - 51)
				return;
			currentSkipAmount++;

			DrawLabels(currentSkipAmount);

			output.HighlightRect(navigation.GetCursorPosition(), 77);
		}

		public void ScrollUp()
		{
			if (currentSkipAmount - 1 < 0)
				return;
			currentSkipAmount--;

			DrawLabels(currentSkipAmount);

			output.HighlightRect(navigation.GetCursorPosition(), 77);
		}

		Label GetCurrentLabel()
		{
			foreach (Label label in listOfLabels)
			{
				if (navigation.GetCursorPosition() == new Coord(xPosition - 1, label.position.y - currentSkipAmount))
					return label;
			}

			return null;
		}

		void SetScreen(Context _screen)
		{
			switch (_screen)
			{
				case Context.First:
					lastPosition = new Coord(1, 4);
					xPosition = 2;
					screen = _screen;
					break;
				case Context.Second:
					lastPosition = new Coord(82, 4);
					xPosition = 83;
					screen = _screen;
					break;
				default:
					throw new Exception("Invalid screen.");
			}
		}
	}
}