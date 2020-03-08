using System;
using System.Collections.Generic;
using System.IO;
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
				HtmlNodeCollection nodesJsAll = 
					doc.DocumentNode.SelectNodes("//script");
				HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
				HtmlNode newGaChild = HtmlNode.CreateNode($"<script async=\"\" src=\"./{title}_files/analytics.js\">");
				body.InsertBefore(newGaChild, nodesJsAll[0]);

				List<HtmlNode> nodesJs =
					doc.DocumentNode.SelectNodes("//script").Where(n => n.Attributes["src"] != null).ToList();
				List<string> jsUrls = nodesJs.Select(n => n.Attributes["src"]?.Value).Where(s => s != null).ToList();
				for (int i = 0; i < nodesJs.Count; i++)
				{
					string jsUrl = nodesJs[i].Attributes["src"].Value;

					string jsFileName = jsUrl.Substring(jsUrl.LastIndexOf("/") + 1);

					nodesJs[i].Attributes["src"].Value = $"./{title}_files/{jsFileName}";
				}


				#endregion

				#region Download

				File.WriteAllText($"{DownloadFolder}{title}.html", doc.DocumentNode.InnerHtml);

				string gaJsUrl = @"https://www.google-analytics.com/analytics.js";

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
		public const string DownloadFolder = @".\..\..\TestDownload\";
	}
}
