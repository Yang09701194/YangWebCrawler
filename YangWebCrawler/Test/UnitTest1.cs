﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YangWebCrawler.DataAccessLayer;

namespace Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
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
			
			Assert.AreEqual((object)1, (object)1);
		}
	}
}
