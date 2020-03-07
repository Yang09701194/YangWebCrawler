using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using YangWebCrawler.Interface;

namespace YangWebCrawler
{
	public class WebCrawler : IWebCrawler
	{
		public void Crawl()
		{
			try
			{
				var web = new HtmlWeb();
				HtmlDocument doc = web.Load(Value.Url);
				//HtmlNode node = doc.DocumentNode.SelectSingleNode("...");
				HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='r-list-container action-bar-margin bbs-screen']/div[@class='r-ent']/div[@class='title']/a");
				List<string> urls = nodes.Select(n => n.Attributes["href"].Value).ToList();

				var v = nodes;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

	}
}
