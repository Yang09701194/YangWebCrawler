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


		private static string testHtml = null;
		public static string TestHtml
		{
			get
			{
				if (testHtml != null)
					return testHtml;
				testHtml = System.IO.File.ReadAllText(@".\..\TestHtml.txt");
				return testHtml;
			}
		}


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
				for (int i = 0; i < nodesCss.Count; i++)
				{
					string cssUrl = nodesCss[i].Attributes["href"].Value;
					nodesCss[i].Attributes["href"].Value = $"./{title}_files/{cssUrl.FileName()}";
				}
				#endregion

				#region Doc Modify

				HtmlNode topbar = doc.DocumentNode.SelectSingleNode("//div[@id='topbar-container']");
				topbar.InnerHtml = TestHtml;

				#endregion

				#region Js
				HtmlNodeCollection nodesJsAll = 
					doc.DocumentNode.SelectNodes("//script");
				List<HtmlNode> nodesJs =
					doc.DocumentNode.SelectNodes("//script").Where(n => n.Attributes["src"] != null).ToList();

				HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
				HtmlNode newGaChild = HtmlNode.CreateNode($"<script async=\"\" src=\"./{title}_files/analytics.js\">");
				body.InsertBefore(newGaChild, nodesJsAll[0]);
				
				List<string> jsUrls = nodesJs.Select(n => n.Attributes["src"]?.Value).Where(s => s != null).ToList();
				for (int i = 0; i < nodesJs.Count; i++)
				{
					string jsUrl = nodesJs[i].Attributes["src"].Value;
					nodesJs[i].Attributes["src"].Value = $"./{title}_files/{jsUrl.FileName()}";
				}
				#endregion

				#region Download
				File.WriteAllText($"{DownloadFolder}{title}.html", doc.DocumentNode.InnerHtml);

				string gaJsUrl = @"https://www.google-analytics.com/analytics.js";
				List<string> downloadUrls = new List<string>();
				downloadUrls.AddRange(cssUrls);
				downloadUrls.AddRange(jsUrls);
				downloadUrls.Add(gaJsUrl);
				foreach (string dUrl in downloadUrls)
				{
					string downloadUrl = "";
					if (dUrl.StartsWith("//"))
					{
						downloadUrl = dUrl.Replace("//", "");
					}
					else
						downloadUrl = dUrl;

					string baseStr, route;
					if (downloadUrl.Contains("//"))
					{
						int doubleSlash = downloadUrl.IndexOf("//");
						int singleSlash = downloadUrl.IndexOf("/", doubleSlash + 2);
						baseStr = downloadUrl.Substring(0, singleSlash + 1);
						route = downloadUrl.Replace(baseStr, "");
					}
					else
					{
						baseStr = downloadUrl.Substring(0, downloadUrl.IndexOf("/") + 1);
						route = downloadUrl.Replace(baseStr, "");
						baseStr = "https://" + baseStr;
					}

					string content = NetHelper.Get(baseStr, route);
					string filePath = $"{DownloadFolder}{title}_files\\{downloadUrl.FileName()}";
					CommonHelper.RecursiveCreateFolder(Path.GetDirectoryName(filePath));
					File.WriteAllText(filePath, content);
				}
				
				#endregion

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
