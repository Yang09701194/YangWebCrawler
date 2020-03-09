using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangWebCrawler.DataAccessLayer
{
	public static class CommonHelper
	{
		public static string FileName(this string url)
		{ 
			return url.Substring(url.LastIndexOf("/") + 1);
		}

		public static string NameValid(this string name)
		{
			return name.Replace(":", "")
				.Replace("\\", "")
				.Replace("/", "")
				.Replace("*", "")
				.Replace("?", "")
				.Replace("\"", "")
				.Replace("<", "")
				.Replace(">", "")
				.Replace("|", "")
				;

		}




		/// <summary>
		/// 若為新的多層資料夾路徑，建立每一層資料夾
		/// </summary>
		/// <param name="folderPath"></param>
		public static void RecursiveCreateFolder(string folderPath)
		{
			if (!Directory.Exists(folderPath))
			{
				try
				{
					SimpleCreateFolder(folderPath);
				}
				catch (Exception e)
				{
					/* $Todo yang error log*/
					Debug.WriteLine(e.Message);
					RecursiveCreateFolder(folderPath);
					SimpleCreateFolder(folderPath);
				}
			}
		}


		public static void SimpleCreateFolder(string FolderPath)
		{
			try
			{
				if (!System.IO.Directory.Exists(FolderPath))
					System.IO.Directory.CreateDirectory(FolderPath);
			}
			catch (Exception e) { /* $Todo yang error log*/Debug.WriteLine(e.Message); }
		}




	}
}
