using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace YangWebCrawler.DataAccessLayer
{
	public static class WebHelper
	{


		public static List<string> GetListUrls(string pageUrl)
		{
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(pageUrl);
			//HtmlNode node = doc.DocumentNode.SelectSingleNode("...");
			HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='r-list-container action-bar-margin bbs-screen']/div[@class='r-ent']/div[@class='title']/a");
			List<string> urls = nodes.Select(n => n.Attributes["href"].Value).ToList();

			return urls;
		}
		
		public static bool Is404(string pageUrl)
		{
			var web = new HtmlWeb();
			HtmlDocument doc = web.Load(pageUrl);
			//HtmlNode node = doc.DocumentNode.SelectSingleNode("...");

			bool is404 = doc.DocumentNode.InnerHtml.Contains("404 - Not Found.");

			return is404;
		}


		public static void DownloadPage()
		{

		}


		public const string PttUrlPrefix = "https://www.ptt.cc";
	}
}
