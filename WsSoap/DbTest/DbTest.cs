using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using WsSoap;

namespace DbTest
{
    [TestClass]
    public class DbTest
    {
        private readonly Db _db;

        public DbTest()
        {
            var connectionParams = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "wesite_rating",
                UserID = "root",
                Password = "damysql",
                CharacterSet = "cp1251"
            };

            _db = new Db(connectionParams.ConnectionString);
        }

        [TestMethod]
        public void TestSetSite()
        {
            const string site = "http://www.kremlin.ru/";
            var sites = _db.GetSites();
            Assert.IsTrue(sites.ContainsValue(site));
        }

        [TestMethod]
        public void TestGetSites()
        {
            _db.GetSites();
        }

        [TestMethod]
        public void TestGetSitesNotFound()
        {
            const string site = "http://www.kremlin1.ru/";
            var sites = _db.GetSites();
            Assert.IsTrue(!sites.ContainsValue(site));
        }


        [TestMethod]
        public void TestUniqueSites()
        {
            var sites = _db.GetSites();
            var sitesCountBeforeAdd = sites.Count;
            Assert.IsNotNull(sites);
            const string site = "http://www.rbc.ru";
            if (sites.ContainsValue(site))
            {
                try
                {
                    _db.SetSite(site);
                }
                catch (WsSoapException) { }
                var sitesCountAfterAdd = _db.GetSites().Count;
                Assert.IsTrue(sitesCountBeforeAdd == sitesCountAfterAdd);
            }
        }

        [TestMethod]
        public void TestGetLink()
        {
            var link = _db.GetLink();
            Assert.IsNotNull(link);
            Assert.IsTrue(link == "http://www.lenta.ru");
        }

        [TestMethod]
        public void TestGetLinkTwice()
        {
            var firstLink = _db.GetLink();
            var secondLink = _db.GetLink();
            Assert.IsNotNull(firstLink);
            Assert.IsNotNull(secondLink);
            Assert.AreNotEqual(firstLink, "");
            Assert.AreNotEqual(secondLink, "");
            Assert.AreNotEqual(firstLink, secondLink);
        }

        [TestMethod]
        public void TestEnqueue()
        {
            _db.EnqueueSitePage(2113);
        }

        [TestMethod]
        public void TestSelectOldSitePage()
        {
            int? sitePageId;
            string url;
            _db.SelectSitePageOutOfDate(out sitePageId, out url);
            Assert.IsNotNull(sitePageId);
        }

        [TestMethod]
        public void TestSelectQueueOneOldest()
        {
            int? sitePageId;
            string url;
            _db.SelectQueueOneOldest(DateTime.UtcNow, 1, out sitePageId, out url);
            Assert.IsNull(sitePageId);
        }
    }
}
