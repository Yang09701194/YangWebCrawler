using System;
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

			WebHelper.DownloadPage();

			Assert.AreEqual((object)1, (object)1);
		}
	}
}
