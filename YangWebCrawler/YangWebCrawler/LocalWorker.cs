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

		public static void UrlList()
		{
			string sourceDir2 = @".\..\..\..\..\..\WP\Backup\";
			string url = File.ReadAllText(sourceDir2 + "\\dUrl.txt");


			string sourceDir = @".\..\..\..\..\..\WP\Backup\DiaryNumber_Ver2";
			List<string> files = Directory.GetFiles(sourceDir).ToList();

			string result = "";
			foreach (string file in files)
			{
				string content = File.ReadAllText(file);
				int s = content.IndexOf("<title>") + 8;
				int e = content.IndexOf("</title>");
				string title = content.Substring(s,  e - s);
				string fname = Path.GetFileName(file);
				result += $"<h4><a href=\"https://www.name2name2.com/name2name2/Main/Diary/{fname}\">{title}</a></h4>";

			}




			System.Windows.Forms.Clipboard.SetText(result);
			 
			 

		}



		public static void Format1()
		{
			string sourceDir = @".\..\..\..\..\..\WP\Backup\DiaryNumber_Ver2";
			string sourceDir2 = @".\..\..\..\..\..\WP\Backup\";


			List<string> files = Directory.GetFiles(sourceDir).ToList();

			string ori = File.ReadAllText(sourceDir2 + "\\ori.txt");
			string to = File.ReadAllText(sourceDir2 + "\\to.txt");

			foreach (string file in files)
			{
				string content = File.ReadAllText(file);
				content = content.Replace(ori, to);
				File.WriteAllText(file, content);

			}



		}




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

				string filePrefix = relativeDir.Replace("_files", "");
				string file = $"{sourceDir}\\{filePrefix}.html";
				string content = File.ReadAllText(file);
				content = content.Replace($"/{filePrefix}_files/", $"/{num}_Diary/");
				File.WriteAllText(file, content);
				File.Move(file, $"{sourceDir}\\{num}_Diary.html");
			}

			#endregion



		}


	}
}
