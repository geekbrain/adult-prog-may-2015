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

        private const string SelectSitesSql =
            @"select
                    s.id, s.url
            from
                site s";

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
                name n;";

        private const string SelectSearchPhrasesByNameIdSql =
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

        private const string SelectSiteIdBySitePageIdSql =
            @"select
                    sp.site_id
            from
                site_page sp
            where
                sp.id = ?id";

        private const string InsertDataCubeSql =
            @"insert data_cube (date, name_id, site_page_id, data_fact)
            values (?date, ?name_id, ?site_page_id, ?data_fact)";

        private const string SelectNameIdByNameSql =
            @"select n.id
            from name n
            where n.name = ?name";

        private const string SelectStatsSql =
            @"select n.name, sum(dc.data_fact) as cnt
            from name n
            join data_cube dc on dc.name_id = n.id
            group by n.name";

        private const string SelectDailyStatsSql =
            @"select dc.date, n.name, sum(dc.data_fact) as cnt
            from name n
            join data_cube dc on dc.name_id = n.id
            group by dc.date, n.name";

        private const string SelectStatsByNameSql =
            @"select dc.date, sum(dc.data_fact) as cnt
            from data_cube dc
                where dc.name_id = ?id
            group by dc.date";

        private const string SelectSitePagesSql =
            @"select sp.id, sp.url, s.url as site_url
            from site_page sp
            join site s on s.id = sp.site_id";

        private const string SelectSearchPhrasesSql =
            @"select n.name, sph.id, sph.name as search_phrase
            from search_phrase sph
            join name n on n.id = sph.name_id";

        private const string InsertSiteSql =
            @"insert into site (url) values (?url)";

        private const string InsertNameSql =
            @"insert into name (name) values (?name)";

        private const string InsertSearchPhraseSql =
            @"insert into search_phrase (name, name_id) values (?name, ?name_id)";

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

        private int? SelectSiteIdByUrl(string url)
        {
            using (var selectSiteIdByUrl = _connection.CreateCommand())
            {
                selectSiteIdByUrl.CommandText = SelectSiteIdByUrlSql;
                selectSiteIdByUrl.Parameters.AddWithValue("?url", url);

                return (int?) selectSiteIdByUrl.ExecuteScalar();
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

                        siteReader.Close();
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
            var sitePageId = SelectSitePageIdByUrl(url);
            if (sitePageId == null) throw new WsSoapException(
                "WsSoap.Db.InsertLinks exception! Can't find url in database!");
            var siteId = SelectSiteIdByUrl(url);
            int? id = 0;
            if ((siteId != null) && (siteId == sitePageId))
            {
                id = (int) siteId;
            }
            else
            {
                id = SelectSiteIdBySitePageId(sitePageId ?? default(int));
                if (id == null)
                {
                    throw new WsSoapException(
                        "WsSoap.Db.InsertLinks exception! Can't find url in database!");
                }
            }
            foreach (var link in links)
            {
                InsertSitePage((int) id, link);
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
                selectSearchPhrases.CommandText = SelectSearchPhrasesByNameIdSql;
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
                insertDataCube.CommandText = InsertDataCubeSql;
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

        private int? SelectSiteIdBySitePageId(int sitePageId)
        {
            using (var selectSiteIdBySitePageId = _connection.CreateCommand())
            {
                selectSiteIdBySitePageId.CommandText = SelectSiteIdBySitePageIdSql;
                selectSiteIdBySitePageId.Parameters.AddWithValue("?id", sitePageId);

                return (int)selectSiteIdBySitePageId.ExecuteScalar();
            }
        }

        public Dictionary<string, int> GetStats()
        {
            var statsDictionary = new Dictionary<string, int>();

            using (var selectStats = _connection.CreateCommand())
            {
                selectStats.CommandText = SelectStatsSql;
                using (var statsReader = selectStats.ExecuteReader())
                {
                    while (statsReader.Read())
                    {
                        statsDictionary.Add(statsReader["name"].ToString(),
                            (int) Math.Floor((double) statsReader["cnt"]));
                    }
                }
            }

            return statsDictionary;
        }

        public Dictionary<DateTime, Dictionary<string, int>> GetDailyStats()
        {
            var dailyStatsDictionary = new Dictionary<DateTime, Dictionary<string, int>>();

            using (var selectDailyStats = _connection.CreateCommand())
            {
                selectDailyStats.CommandText = SelectDailyStatsSql;
                using (var dailyStatsReader = selectDailyStats.ExecuteReader())
                {
                    while (dailyStatsReader.Read())
                    {
                        var statsDictionary = new Dictionary<string, int>
                        {
                            {dailyStatsReader["name"].ToString(),
                                (int) Math.Floor((double) dailyStatsReader["cnt"])}
                        };
                        dailyStatsDictionary[(DateTime) dailyStatsReader["date"]] =
                            statsDictionary;
                    }
                }
            }

            return dailyStatsDictionary;
        }

        public Dictionary<DateTime, int> GetStatsByName(string name)
        {
            var nameId = SelectNameIdByName(name);
            if (nameId == null) throw new WsSoapException(
                "WsSoap.Db.GetStatsByName exception! Can't find name in database!");

            var statsDictionary = new Dictionary<DateTime, int>();
            using (var selectStats = _connection.CreateCommand())
            {
                selectStats.CommandText = SelectStatsByNameSql;
                selectStats.Parameters.AddWithValue("?id", (int) nameId);
                using (var statsReader = selectStats.ExecuteReader())
                {
                    while (statsReader.Read())
                    {
                        statsDictionary.Add((DateTime) statsReader["date"],
                            (int) Math.Floor((double) statsReader["cnt"]));
                    }
                }
            }

            return statsDictionary;
        }

        public Dictionary<int, string> GetNamesWithId()
        {
            return SelectNames();
        }

        public Dictionary<int, string> GetSites()
        {
            var sitesDictionary = new Dictionary<int, string>();

            using (var selectSite = _connection.CreateCommand())
            {
                selectSite.CommandText = SelectSitesSql;
                using (var siteReader = selectSite.ExecuteReader())
                {
                    while (siteReader.Read())
                    {
                        sitesDictionary.Add((int) siteReader["id"], siteReader["url"].ToString());
                    }
                }
            }

            return sitesDictionary;
        }

        public List<Page> GetPages()
        {
            var sitePages = new List<Page>();

            using (var selectSitePages = _connection.CreateCommand())
            {
                selectSitePages.CommandText = SelectSitePagesSql;
                using (var sitePagesReader = selectSitePages.ExecuteReader())
                {
                    while (sitePagesReader.Read())
                    {
                        sitePages.Add(new Page((int) sitePagesReader["id"],
                            sitePagesReader["url"].ToString(),
                            sitePagesReader["site_url"].ToString()));
                    }
                }
            }

            return sitePages;
        }

        public Dictionary<string, Dictionary<int, string>> GetSearchPhrases()
        {
            var namesSearchPhrasesDictionary = new Dictionary<string, Dictionary<int, string>>();

            using (var selectSearchPhrases = _connection.CreateCommand())
            {
                selectSearchPhrases.CommandText = SelectSearchPhrasesSql;
                using (var searchPhrasesReader = selectSearchPhrases.ExecuteReader())
                {
                    while (searchPhrasesReader.Read())
                    {
                        var searchPhrasesDictionary = new Dictionary<int, string>
                        {
                            {(int) searchPhrasesReader["id"],
                                searchPhrasesReader["search_phrase"].ToString()}
                        };
                        namesSearchPhrasesDictionary[searchPhrasesReader["name"].ToString()] =
                            searchPhrasesDictionary;
                    }
                }
            }

            return namesSearchPhrasesDictionary;
        }

        public void SetSite(string url)
        {
            using (var insertSite = _connection.CreateCommand())
            {
                insertSite.CommandText = InsertSiteSql;
                insertSite.Parameters.AddWithValue("?url", url);
                insertSite.ExecuteNonQuery();
            }
        }

        public void SetName(string name)
        {
            using (var insertName = _connection.CreateCommand())
            {
                insertName.CommandText = InsertNameSql;
                insertName.Parameters.AddWithValue("?name", name);
                insertName.ExecuteNonQuery();
            }
        }

        public void SetSearchPhrase(string name, string searchPhrase)
        {
            var nameId = SelectNameIdByName(name);
            if (nameId == null) throw new WsSoapException(
                "WsSoap.Db.SetSearchPhrase exception! Can't find url in database!");

            using (var insertSearchPhrase = _connection.CreateCommand())
            {
                insertSearchPhrase.CommandText = InsertSearchPhraseSql;
                insertSearchPhrase.Parameters.AddWithValue("?name_id", nameId);
                insertSearchPhrase.Parameters.AddWithValue("?name", searchPhrase);
                insertSearchPhrase.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}