using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YangWebCrawler
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				WebCrawler crawler = new WebCrawler();
				crawler.Crawl();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				//throw;
			}



		}
	}
}
