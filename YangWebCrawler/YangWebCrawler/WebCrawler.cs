using System;
using System.Collections.Generic;
using System.Diagnostics;
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

				List<string> allPendingDownloadUrls = new List<string>();
				List<string> firstUrls = new List<string>();

				foreach (string url in urls)
				{
					int page = 1;
					while (true)
					{
						string fUrl = String.Format(url, page);
						if (WebHelper.Is404(fUrl))
							break;
						List<string> listUrls = WebHelper.GetListUrls(fUrl);
						
						//if(listUrls.Any())
						//	firstUrls.Add(listUrls[0]);

						allPendingDownloadUrls.AddRange(listUrls);
						page += 1;
					}
				}

				var v = allPendingDownloadUrls;

				//test
				//firstUrls.ForEach(u => Process.Start(WebHelper.PttUrlPrefix + u));


			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}




	}
}
