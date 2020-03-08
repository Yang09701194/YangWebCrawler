using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YangWebCrawler.DataAccessLayer;

namespace YangWebCrawler
{
	public static class Test
	{

		public static void TestMethod1()
		{
			try
			{
				string[] urls = System.IO.File.ReadAllText(@".\..\UnitTest.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				WebHelper.DownloadPage(urls[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}
	}
}
