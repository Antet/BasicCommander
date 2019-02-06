using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using static BasicCommander.ConsoleLibrary;
using static BasicCommander.Navigation;
using static BasicCommander.Output;

namespace BasicCommander
{
	static class LabelCollection
	{
		public struct Label
		{
			public string name;
			public string fileSize;
			public int position;

			public Label(string _name, int _position, string _fileSize = "0")
			{
				name = _name;
				fileSize = _fileSize;
				position = _position;
			}
		}

		public static List<Label> labels = new List<Label>();

		public static void Initialize()
		{
			ChangeDirectory("C:\\");
		}

		public static void ChangeDirectory(string path)
		{
			labels.Clear();
			ClearScreen(Screen.First);

			WriteToConsole(path, 2, 2);

			if (Directory.GetParent(path) != null)
			{
				labels.Add(new Label(Directory.GetParent(path).Name, labels.Count + 4));
				WriteToConsole("..", 2, labels.Last().position);
			}

			foreach (string file in Directory.GetDirectories(path, "", SearchOption.TopDirectoryOnly))
			{
				labels.Add(new Label(file, labels.Count + 4));

				string dirName = file.Replace(path, string.Empty);
				string formattedLabel = String.Format("{0,-34}{1}", dirName.Length > 30 ? dirName.Substring(0, 27) + "..." : dirName, "<DIR>");

				WriteToConsole(formattedLabel, 2, labels.Last().position);
			}

			foreach (string file in Directory.GetFiles(path, "", SearchOption.TopDirectoryOnly))
			{
				FileInfo fileInfo = new FileInfo(file);

				long fileSize = fileInfo.Length;

				labels.Add(new Label(fileInfo.Name, labels.Count + 4, GetFileSizeString(fileInfo.Length)));

				string formattedLabel = String.Format("{0,-34}{1}", fileInfo.Name.Length > 30 ? fileInfo.Name.Substring(0, 27) + "..." : fileInfo.Name, labels.Last().fileSize);

				WriteToConsole(formattedLabel, 2, labels.Last().position);
			}
		}

		public static void LabelHighlightCheck()
		{
			if (GetCurrentScreen().Equals(Screen.First))
			{
				HighlightRect(2, GetCurrentLabel().position, 74);
				DeHighlightRect(2, GetCurrentLabel().position - 1, 74);
				DeHighlightRect(2, GetCurrentLabel().position + 1, 74);
			}
		}

		static void AddLabel(string labelName)
		{
			labels.Add(new Label(labelName, labels.Count));
			WriteToConsole("..", 2, (labels.Count - 1));
		}

		static Label GetCurrentLabel()
		{
			if (GetCurrentScreen().Equals(Screen.First) && IsOnLabel())
				return labels.Find(pos => pos.position == GetCursorPosition().Y);
			else
				return default(Label);
		}

		static bool IsOnLabel()
		{
			foreach (Label label in labels)
			{
				if (GetCursorPosition().Y == label.position)
					return true;
			}

			return false;
		}

		static string GetFileSizeString(long fileSize)
		{
			string[] fileSizeLabels = { "B", "KB", "MB", "GB", "TB" };

			int order = 0;
			while (fileSize >= 1024 && order < fileSizeLabels.Length - 1)
			{
				order++;
				fileSize = fileSize / 1024;
			}

			return String.Format("{0:0.##} {1}", fileSize, fileSizeLabels[order]);
		}


	}
}