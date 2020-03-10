using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangWebCrawler
{
	public static class LocalWorker
	{


		public static void ModifyFileNameToNumber()
		{
			string sourceDir = @".\..\..\Test\0DiaryOrigin";
			string targetDir = @".\..\..\Test\0DiaryOrigin";

			#region Dir

			List<string> dirs = Directory.GetDirectories(sourceDir).ToList();

			var relativeDirs = dirs.Select(r => r.Substring(r.IndexOf("Origin") + 7)).ToList();

			foreach (string relativeDir in relativeDirs)
			{
				string num = relativeDir.Substring(0, relativeDir.IndexOf("_") );
				Directory.Move($"{sourceDir}\\{relativeDir}", $"{sourceDir}\\{num}_Diary");

				string file = $"{sourceDir}\\{relativeDir}.html";
				string content = File.ReadAllText(file);
				content = content.Replace($"/{relativeDir}/", $"/{num}_Diary");
				File.WriteAllText(file, content);
				File.Move(file, $"{sourceDir}\\{num}_Diary.html");
			}

			#endregion



		}


	}
}
