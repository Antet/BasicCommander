using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static BasicCommander.Program;
using static BasicCommander.ConsoleLibrary;

namespace BasicCommander
{
	static class LabelUtility
	{
		public static string GetFileSize(Label label)
		{
			float fileSize = label.fileSize;

			bool isDirectory = File.GetAttributes(label.dirPath).HasFlag(FileAttributes.Directory);

			if (isDirectory)
				return "<DIR>";

			string[] fileSizeLabels = { "B", "kB", "MB", "GB", "TB" };

			int order = 0;
			while (fileSize >= 1024 && order < fileSizeLabels.Length - 1)
			{
				order++;
				fileSize /= 1024f;
			}

			return $"{fileSize:0.#} {fileSizeLabels[order]}";
		}

		public static string ShortenLabelName(Label label) => label.name.Length > 30 ? label.name.Substring(0, 27) + "..." : label.name;
	}
}