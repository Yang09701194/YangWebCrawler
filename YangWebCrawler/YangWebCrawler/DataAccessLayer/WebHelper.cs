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


		public static void DownloadPage(string url)
		{
			try
			{
				var web = new HtmlWeb();
				HtmlDocument doc = web.Load(url);

				#region Title
				string title = doc.DocumentNode.SelectSingleNode("//title").InnerText;

				#endregion

				#region Css
				HtmlNodeCollection nodesCss = doc.DocumentNode.SelectNodes("//link[@type='text/css']");
				List<string> cssUrls = nodesCss.Select(n => n.Attributes["href"].Value).ToList();
				#endregion

				#region Js
				HtmlNodeCollection nodesJs = doc.DocumentNode.SelectNodes("//script");
				List<string> jsUrls = nodesJs.Select(n => n.Attributes["src"]?.Value).Where(s => s != null).ToList();

				HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
				HtmlNode newChild = HtmlNode.CreateNode($"<script async=\"\" src=\"./{title}_files/analytics.js\">");
				//if (firstJsNode != null)
				body.InsertBefore(
						newChild
						, nodesJs[0]);


				string gaJsUrl = @"https://www.google-analytics.com/analytics.js";
				//doc.DocumentNode.InsertAfter(,)



				#endregion

				var v = nodesCss;


			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		
		}


		public const string PttUrlPrefix = "https://www.ptt.cc";
		public const string DownloadFolder = @".\..\..\TestDownload";
	}
}
