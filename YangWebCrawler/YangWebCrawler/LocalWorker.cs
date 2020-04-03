using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YangWebCrawler.DataAccessLayer;

namespace YangWebCrawler
{
	public static class LocalWorker
	{

		public static void GetCwbImgs()
		{
			string urlFormat = @"/V7/symbol/weather/gif/day/{0}.gif";


			for (int i = 1; i <= 65; i++)
			{
				try
				{
					Console.WriteLine(i);
					string urlPostfix = String.Format(urlFormat, i.ToString("00"));
					FileStream fs =
						File.Create(
							$@"........\Images\{i.ToString("00")}.gif");

					Stream content = (Stream) NetHelper.Get("https://www.cwb.gov.tw/", urlPostfix, ContentType.Stream);
					content.Seek(0, SeekOrigin.Begin);
					content.CopyTo(fs);
					fs.Close();//也可以用using 不加Close()

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					//throw;
				}


			}

		}


		public static void UrlList()
		{
			string sourceDir2 = @".\..\..\..\..\..\WP\Backup\";
			string url = File.ReadAllText(sourceDir2 + "\\dUrl.txt");


			string sourceDir = @".\..\..\..\..\..\WP\Backup\DiaryNumber_Ver2";
			List<string> files = Directory.GetFiles(sourceDir).ToList();
			files = files.OrderByDescending(f =>
				Convert.ToInt32(
					Path.GetFileName(f).Split('_')[0]
				)
			).ToList();

			List<string> result = new List<string>();
			foreach (string file in files)
			{
				string content = File.ReadAllText(file);
				int s = content.IndexOf("<title>") + 7;
				int e = content.IndexOf("</title>");
				string title = content.Substring(s,  e - s);
				string fname = Path.GetFileName(file);
				result.Add($"<h4><a href=\"https://...../Diary/{fname}\">{title}</a></h4>\r\n");
			}
			string r = String.Join("", result);


			//System.Windows.Forms.Clipboard.SetText(result);

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
