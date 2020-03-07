using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using YangWebCrawler.DataAccessLayer;
using YangWebCrawler.Interface;

namespace YangWebCrawler
{
	public class WebCrawler : IWebCrawler
	{
		public void Crawl()
		{
			try
			{

				string[] urls = System.IO.File.ReadAllText(@".\..\Value.txt").Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);


				foreach (string url in urls)
				{
					int page = 10;
					while (true)
					{
						string fUrl = String.Format(url, page);
						bool b = WebHelper.Is404(fUrl);
						//List<string> listUrls = WebHelper.GetListUrls(fUrl)
						page += 1;
					}

				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}




	}
}
