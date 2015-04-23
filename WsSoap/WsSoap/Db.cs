using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WsSoap
{
    public class Db: IDisposable
    {
        private readonly MySqlConnection _connection;
        private const string SelectSiteSql =
            @"select
                    s.id, s.url
            from
                site s
            where
                not exists(select sp.site_id from site_page sp where sp.site_id = s.id)";

        private const string SelectSiteIdByUrlSql =
            @"select
                    s.id
            from
                site s
            where
                s.url = ?url";

        private const string SelectSitePageOutOfDateSql =
            @"select
                    sp.id, sp.url, sp.site_id
            from
                site_page sp
            where
                not exists(select dc.id from data_cube dc where dc.site_page_id = sp.id
                    and dc.date = ?date);";

        private const string InsertSitePageSql =
            @"insert site_page (url, site_id)
            values (?url, ?site_id);";

        private const string SelectNamesSql =
            @"select
                    n.id, n.name
            from
                names n;";

        private const string SelectSearchPhrasesSql =
            @"select
                    sph.id, sph.name
            from
                search_phrase sph
            where
                sph.name_id = ?name_id;";

        private const string SelectSitePageIdByUrlSql = 
            @"select
                    sp.id
            from
                site_page sp
            where
                sp.url = ?url";

        private const string InsertDataCubeSql =
            @"insert data_cube (date, name_id, site_page_id, data_fact)
            values (?date, ?name_id, ?site_page_id, ?data_fact)";

        private const string SelectNameIdByNameSql =
            @"select n.id
            from name n
            where n.name = ?name";

        public Db()
        {
            var connectionParams = new MySqlConnectionStringBuilder
                {
                    Server = "localhost",
                    Database = "wesite_rating",
                    UserID = "root",
                    Password = "damysql",
                    CharacterSet = "cp1251"
                };

            _connection =
                new MySqlConnection { ConnectionString = connectionParams.ConnectionString };

            _connection.Open();
        }

        private int? SelectSiteByUrl(string url)
        {
            using (var selectSiteIdByUrl = _connection.CreateCommand())
            {
                selectSiteIdByUrl.CommandText = SelectSiteIdByUrlSql;
                selectSiteIdByUrl.Parameters.AddWithValue("?url", url);

                return (int) selectSiteIdByUrl.ExecuteScalar();
            }
        }

        public string GetLink()
        {
            using (var selectSite = _connection.CreateCommand())
            {
                selectSite.CommandText = SelectSiteSql;
                using (var siteReader = selectSite.ExecuteReader())
                {
                    if (siteReader.Read())
                    {
                        var id = (int) siteReader["id"];
                        var url = siteReader["url"].ToString();

                        InsertSitePage(id, url);
                    }
                }
            }
            using (var selectSitePageOutOfDate = _connection.CreateCommand())
            {
                selectSitePageOutOfDate.CommandText = SelectSitePageOutOfDateSql;
                selectSitePageOutOfDate.Parameters.AddWithValue("?date", DateTime.Today);
                using (var sitePageOutOfDateReader = selectSitePageOutOfDate.ExecuteReader())
                {
                    if (sitePageOutOfDateReader.Read())
                    {
                        return sitePageOutOfDateReader["url"].ToString();
                    }
                }
            }
            return null;
        }

        private void InsertSitePage(int id, string url)
        {
            using (var insertSitePage = _connection.CreateCommand())
            {
                insertSitePage.CommandText = InsertSitePageSql;
                insertSitePage.Parameters.AddWithValue("?url", url);
                insertSitePage.Parameters.AddWithValue("?site_id", id);
                insertSitePage.ExecuteNonQuery();
            }
        }

        public void InsertLinks(List<string> links, string url)
        {
            var id = SelectSiteByUrl(url);
            if (id == null) throw new WsSoapException(
                "WsSoap.Db.InsertLinks exception! Can't find url in database!");
            foreach (var link in links)
            {
                InsertSitePage((int) id, url);
            }
        }

        private Dictionary<int, string> SelectNames()
        {
            var names = new Dictionary<int, string>();
            using (var selectNames = _connection.CreateCommand())
            {
                selectNames.CommandText = SelectNamesSql;
                using (var namesReader = selectNames.ExecuteReader())
                {
                    while (namesReader.Read())
                    {
                        names.Add((int) namesReader["id"], namesReader["name"].ToString());
                    }
                }
            }
            return names;
        }

        private List<string> SelectSearchPhrases(int nameId)
        {
            var searchPhrases = new List<string>();
            using (var selectSearchPhrases = _connection.CreateCommand())
            {
                selectSearchPhrases.CommandText = SelectSearchPhrasesSql;
                selectSearchPhrases.Parameters.AddWithValue("?name_id", nameId);
                using (var searchPhrasesReader = selectSearchPhrases.ExecuteReader())
                {
                    while (searchPhrasesReader.Read())
                    {
                        searchPhrases.Add(searchPhrasesReader["name"].ToString());
                    }
                }
            }

            return searchPhrases;
        }

        public Dictionary<string, List<string>> GetNames()
        {
            var names = SelectNames();
            var namesDictionary = new Dictionary<string, List<string>>();
            foreach (var name in names)
            {
                var searchPhrases = SelectSearchPhrases(name.Key);
                namesDictionary.Add(name.Value, searchPhrases);
            }
            return namesDictionary;
        }

        private int? SelectSitePageIdByUrl(string url)
        {
            using (var selectSitePageIdByUrl = _connection.CreateCommand())
            {
                selectSitePageIdByUrl.CommandText = SelectSitePageIdByUrlSql;
                selectSitePageIdByUrl.Parameters.AddWithValue("?url", url);

                return (int)selectSitePageIdByUrl.ExecuteScalar();
            }
        }

        private int? SelectNameIdByName(string name)
        {
            using (var selectNameIdByName = _connection.CreateCommand())
            {
                selectNameIdByName.CommandText = SelectNameIdByNameSql;
                selectNameIdByName.Parameters.AddWithValue("?name", name);

                return (int)selectNameIdByName.ExecuteScalar();
            }            
        }

        private void InsertDataCube(DateTime date, int nameId, int sitePageId, int dataFact)
        {
            using (var insertDataCube = _connection.CreateCommand())
            {
                insertDataCube.CommandText = InsertSitePageSql;
                insertDataCube.Parameters.AddWithValue("?date", date);
                insertDataCube.Parameters.AddWithValue("?name_id", nameId);
                insertDataCube.Parameters.AddWithValue("?site_page_id", sitePageId);
                insertDataCube.Parameters.AddWithValue("?data_fact", dataFact);
                insertDataCube.ExecuteNonQuery();
            }
        }

        public void InsertAmount(Dictionary<string, int> namesAmountDictionary, string url)
        {
            var sitePageId = SelectSitePageIdByUrl(url);
            if (sitePageId == null) throw new WsSoapException(
                "WsSoap.Db.InsertAmount exception! Can't find url in database!");
            foreach (var nameAmount in namesAmountDictionary)
            {
                var nameId = SelectNameIdByName(nameAmount.Key);
                if (nameId == null) throw new WsSoapException(
                    "WsSoap.Db.InsertAmount exception! Can't find name in database!");
                InsertDataCube(DateTime.Today, (int) nameId, (int) sitePageId,
                    nameAmount.Value);
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}