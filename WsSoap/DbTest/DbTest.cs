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
    }
}
