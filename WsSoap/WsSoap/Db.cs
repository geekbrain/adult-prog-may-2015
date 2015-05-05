using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using NLog;

namespace WsSoap
{
    public class SitePage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int SiteId { get; set; }
        public int Retries { get; set; }
    }

    public class Db: IDisposable
    {
        private readonly MySqlConnection _connection;
        private static readonly Logger _logger = LogManager.GetLogger("DbLogger");

        const int DefaultMaxQueuedTime = 1;

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

        private const string SelectSitePageByIdSql =
            @"select sp.url
            from site_page sp
            where sp.id = ?site_page_id";

        private const string SelectSitePagesBySiteIdSql =
            @"select sp.id, sp.url
            from site_page sp
            where sp.site_id = ?site_id";

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

        private const string SelectQueueSql =
            @"select q.id, q.site_page_id from site_page_queue q where q.id = ?id";

        private const string DeleteQueueSql =
            @"delete from site_page_queue where site_page_id = ?id";
        
        public Db(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }
        
        private int? SelectSiteIdByUrl(string url)
        {
            CheckConnectionState();
            using (var selectSiteIdByUrl = _connection.CreateCommand())
            {
                const string selectSiteIdByUrlSql =
                    @"select
                            s.id
                    from
                        site s
                    where
                        s.url = ?url";

                selectSiteIdByUrl.CommandText = selectSiteIdByUrlSql;
                selectSiteIdByUrl.Parameters.AddWithValue("?url", url);
                return (int?)selectSiteIdByUrl.ExecuteScalar();
            }
        }

        public SitePage SelectSitePageOutOfDate()
        {
            var sitePage = new SitePage();
            CheckConnectionState();
            using (var selectSitePageOutOfDate = _connection.CreateCommand())
            {
                const string selectSitePageOutOfDateSql =
                    @"select
                            sp.id, sp.url, sp.site_id
                    from
                        site_page sp
                    where
                        not exists (select dc.id from data_cube dc where dc.site_page_id = sp.id
                            and dc.date = ?date)
                        and not exists (select q.id from site_page_queue q where q.site_page_id = sp.id)
      			        and not exists (select bp.id from broken_page bp where bp.site_page_id = sp.id)
                    limit 1;";

                selectSitePageOutOfDate.CommandText = selectSitePageOutOfDateSql;
                selectSitePageOutOfDate.Parameters.AddWithValue("?date", DateTime.Today);
                using (var sitePageOutOfDateReader = selectSitePageOutOfDate.ExecuteReader())
                {
                    if (!sitePageOutOfDateReader.Read()) return null;
                    sitePage.Id = (int) sitePageOutOfDateReader["id"];
                    sitePage.Url = sitePageOutOfDateReader["url"].ToString();
                    sitePage.SiteId = (int) sitePageOutOfDateReader["site_id"];
                }
            }
            return sitePage;
        }

        public SitePage SelectQueueOneOldest(int intervalInHours)
        {
            var sitePage = new SitePage();
            CheckConnectionState();
            using (var selectQueueOneOldest = _connection.CreateCommand())
            {
                const string selectQueueOneOldestSql =
                    @"select q.id, q.site_page_id, sp.url, q.retries
                    from site_page_queue q
                    join site_page sp on sp.id = q.site_page_id
                    where q.datetime_income in (select min(q2.datetime_income) from site_page_queue q2)
                        and DATE_ADD(q.datetime_income, INTERVAL ?interval HOUR) < now()
                    limit 1";

                selectQueueOneOldest.CommandText = selectQueueOneOldestSql;
                selectQueueOneOldest.Parameters.AddWithValue("?interval", intervalInHours);

                using (var queueOneOldestReader = selectQueueOneOldest.ExecuteReader())
                {
                    if (!queueOneOldestReader.Read()) return null;
                    sitePage.Id = (int) queueOneOldestReader["site_page_id"];
                    sitePage.Url = queueOneOldestReader["url"].ToString();
                    sitePage.Retries = (int) queueOneOldestReader["retries"];
                }
            }
            return sitePage;
        }


        private void InsertToBrokenPages(SitePage param)
        {
            const string insertSql =
                @"insert into broken_page (site_page_id, datetime_income) values (?param, now());";

            CheckConnectionState();
            using (var insert = _connection.CreateCommand())
            {
                insert.CommandText = insertSql;
                insert.Parameters.AddWithValue("?param", param.Id);
                insert.ExecuteNonQuery();
            }
        }


        public string GetLink()
        {
            TransferSitesToSitePages();

            var sitePage = SelectQueueOneOldest(DefaultMaxQueuedTime);
            while (sitePage != null)
            {
                if (sitePage.Retries == 1)
                {
                    DequeueSitePage(sitePage.Id);
                    InsertToBrokenPages(sitePage);
                }
                else
                {
                    _logger.Info("Возвращаем url из очереди");
                    _logger.Info("URL: " + sitePage.Url);

                    return sitePage.Url;                    
                }
                sitePage = SelectQueueOneOldest(DefaultMaxQueuedTime);
            }

            sitePage = SelectSitePageOutOfDate();
            if (sitePage != null)
            {
                _logger.Info("Добавляем в очередь url по запросу из базы");
                _logger.Info("URL: " + sitePage.Url);
                EnqueueSitePage(sitePage);

                return sitePage.Url;
            }

            _logger.Info("Нет данных для обработки");

            return null;
        }

        private void TransferSitesToSitePages()
        {
            ReopenConnection();
            using (var selectSite = _connection.CreateCommand())
            {
                var sitesDictionary = new Dictionary<int, string>();
                selectSite.CommandText = SelectSiteSql;
                using (var siteReader = selectSite.ExecuteReader())
                {
                    while (siteReader.Read())
                    {
                        int? id = (int) siteReader["id"];
                        var url = siteReader["url"].ToString();
                        if (id != null)
                        {
                            sitesDictionary.Add((int) id, url);
                        }
                    }
                }
                foreach (var site in sitesDictionary)
                {
                    InsertSitePage(site.Key, site.Value);
                }
            }
        }

        private void InsertSitePage(int id, string url)
        {
            ReopenConnection();
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
            if (sitePageId == null)
            {
                _logger.Error("InsertLinks: не найден site_page_id по url");
                throw new WsSoapException(
                    "WsSoap.Db.InsertLinks exception! Can't find url in database!");
            }

            var siteId = SelectSiteIdByUrl(url);
            int? id = 0;
            if ((siteId != null) && (siteId == sitePageId))
            {
                id = (int) siteId;
            }
            else
            {
                id = SelectSiteIdBySitePageId((int) sitePageId);
                if (id == null)
                {
                    _logger.Error("InsertLinks: не найден site_id по site_page_id");
                    throw new WsSoapException(
                        "WsSoap.Db.InsertLinks exception! Can't find url in database!");
                }
            }
            var sitePages = SelectSitePagesBySiteId((int) id);

            if (links != null)
            {
                foreach (var sitePage in sitePages.Where(links.Contains))
                {
                    links.Remove(sitePage);
                }
                foreach (var link in links)
                {
                    InsertSitePage((int) id, link);
                }
            }
        }


        private IEnumerable<string> SelectSitePagesBySiteId(int siteId)
        {
            var sitePages = new List<string>();
            CheckConnectionState();

            using (var selectSitePages = _connection.CreateCommand())
            {
                selectSitePages.CommandText = SelectSitePagesBySiteIdSql;
                selectSitePages.Parameters.AddWithValue("?site_id", siteId);
                using (var selectSitePagesReader = selectSitePages.ExecuteReader())
                {
                    while (selectSitePagesReader.Read())
                    {
                        sitePages.Add(selectSitePagesReader["url"].ToString());
                    }
                }
            }

            return sitePages;
        }

        private void CheckConnectionState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private void ReopenConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Open();
        }

        private Dictionary<int, string> SelectNames()
        {
            CheckConnectionState();
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

        private List<string> SelectSearchPhrasesByNameId(int nameId)
        {
            var searchPhrases = new List<string>();
            CheckConnectionState();
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
                var searchPhrases = SelectSearchPhrasesByNameId(name.Key);
                namesDictionary.Add(name.Value, searchPhrases);
            }
            return namesDictionary;
        }

        private int? SelectSitePageIdByUrl(string url)
        {
            CheckConnectionState();
            using (var selectSitePageIdByUrl = _connection.CreateCommand())
            {
                selectSitePageIdByUrl.CommandText = SelectSitePageIdByUrlSql;
                selectSitePageIdByUrl.Parameters.AddWithValue("?url", url);

                return (int) selectSitePageIdByUrl.ExecuteScalar();
            }
        }

        private int? SelectNameIdByName(string name)
        {
            CheckConnectionState();
            using (var selectNameIdByName = _connection.CreateCommand())
            {
                selectNameIdByName.CommandText = SelectNameIdByNameSql;
                selectNameIdByName.Parameters.AddWithValue("?name", name);

                return (int) selectNameIdByName.ExecuteScalar();
            }
        }

        private void InsertDataCube(DateTime date, int nameId, int sitePageId, int dataFact)
        {
            CheckConnectionState();
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

        private int SelectRetriesFromQueueBySitePageId(int id)
        {
            CheckConnectionState();
            using (var selectFromQueueById = _connection.CreateCommand())
            {
                const string selectSiteIdByUrlSql =
                    @"select
                        retries
                    from
                        site_page_queue
                    where
                        site_page_id = ?id";

                selectFromQueueById.CommandText = selectSiteIdByUrlSql;
                selectFromQueueById.Parameters.AddWithValue("?id", id);
                return (int) selectFromQueueById.ExecuteScalar();
            }
        }

        private void UpdateSitePageQueueRetriesBySitePageId(int id, int retries)
        {
            CheckConnectionState();
            using (var update = _connection.CreateCommand())
            {
                const string updateSql =
                    @"update site_page_queue
                    set
                        datetime_income = now()
                        ,retries = ?retries
                    where
                        site_page_id = ?id";

                update.CommandText = updateSql;
                update.Parameters.AddWithValue("?id", id);
                update.Parameters.AddWithValue("?retries", retries);
                update.ExecuteScalar();
            }
        }


        public void InsertAmount(Dictionary<string, int> namesAmountDictionary, string url)
        {
            var sitePageId = SelectSitePageIdByUrl(url);
            if (sitePageId == null) throw new WsSoapException(
                "WsSoap.Db.InsertAmount exception! Can't find url in database!");
            var sitePage = new SitePage
            {
                Id = (int) sitePageId
                ,Retries = SelectRetriesFromQueueBySitePageId((int) sitePageId)
            };

            if (namesAmountDictionary != null)
            {
                foreach (var nameAmount in namesAmountDictionary)
                {
                    var nameId = SelectNameIdByName(nameAmount.Key);
                    if (nameId == null) throw new WsSoapException(
                        "WsSoap.Db.InsertAmount exception! Can't find name in database!");
                    InsertDataCube(DateTime.Today, (int) nameId, (int) sitePageId,
                        nameAmount.Value);
                }
                DequeueSitePage((int) sitePageId);
            }
            else if (sitePage.Retries < 1)
            {
                sitePage.Retries++;
                UpdateSitePageQueueRetriesBySitePageId(sitePage.Id, sitePage.Retries);
            }
            else
            {
                InsertToBrokenPages(sitePage);
                DequeueSitePage(sitePage.Id);
            }
        }

        private int? SelectSiteIdBySitePageId(int sitePageId)
        {
            CheckConnectionState();
            using (var selectSiteIdBySitePageId = _connection.CreateCommand())
            {
                selectSiteIdBySitePageId.CommandText = SelectSiteIdBySitePageIdSql;
                selectSiteIdBySitePageId.Parameters.AddWithValue("?id", sitePageId);

                return (int) selectSiteIdBySitePageId.ExecuteScalar();
            }
        }

        public Dictionary<string, int> GetStats()
        {
            var statsDictionary = new Dictionary<string, int>();

            CheckConnectionState();
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

            CheckConnectionState();
            using (var selectDailyStats = _connection.CreateCommand())
            {
                selectDailyStats.CommandText = SelectDailyStatsSql;
                using (var dailyStatsReader = selectDailyStats.ExecuteReader())
                {
                    while (dailyStatsReader.Read())
                    {
                        var statsDictionary = new Dictionary<string, int>
                        {
                            {
                                dailyStatsReader["name"].ToString(),
                                (int) Math.Floor((double) dailyStatsReader["cnt"])
                            }
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
            CheckConnectionState();
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

            CheckConnectionState();
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

            CheckConnectionState();
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

        public string SelectSitePageById(int sitePageId)
        {
            CheckConnectionState();
            using (var selectSitePageById = _connection.CreateCommand())
            {
                selectSitePageById.CommandText = SelectSitePageByIdSql;
                selectSitePageById.Parameters.AddWithValue("?site_page_id", sitePageId);

                return (string) selectSitePageById.ExecuteScalar();
            }
        }

        public Dictionary<string, Dictionary<int, string>> GetSearchPhrases()
        {
            var namesSearchPhrasesDictionary = new Dictionary<string, Dictionary<int, string>>();

            CheckConnectionState();
            using (var selectSearchPhrases = _connection.CreateCommand())
            {
                selectSearchPhrases.CommandText = SelectSearchPhrasesSql;
                using (var searchPhrasesReader = selectSearchPhrases.ExecuteReader())
                {
                    while (searchPhrasesReader.Read())
                    {
                        var searchPhrasesDictionary = new Dictionary<int, string>
                        {
                            {
                                (int) searchPhrasesReader["id"],
                                searchPhrasesReader["search_phrase"].ToString()
                            }
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
            var siteId = SelectSiteIdByUrl(url);
            if (siteId != null)
            {
                throw new WsSoapException(
                    "WsSoap.Db.SetName exception! There is that site in DB already!");
            }

            CheckConnectionState();
            using (var insertSite = _connection.CreateCommand())
            {
                insertSite.CommandText = InsertSiteSql;
                insertSite.Parameters.AddWithValue("?url", url);
                insertSite.ExecuteNonQuery();
            }
        }

        public void SetName(string name)
        {
            var nameId = SelectNameIdByName(name);
            if (nameId != null)
            {
                throw new WsSoapException(
                    "WsSoap.Db.SetName exception! There is that name in DB already!");
            }

            CheckConnectionState();
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
            if (nameId == null)
            {
                throw new WsSoapException(
                    "WsSoap.Db.SetSearchPhrase exception! Can't find url in database!");
            }
            var searchPhrases = SelectSearchPhrasesByNameId((int) nameId);
            if (searchPhrases.Contains(searchPhrase))
            {
                throw new WsSoapException(
                    "WsSoap.Db.SetSearchPhrase exception! There is that search phrase in DB already!");
            }

            CheckConnectionState();
            using (var insertSearchPhrase = _connection.CreateCommand())
            {
                insertSearchPhrase.CommandText = InsertSearchPhraseSql;
                insertSearchPhrase.Parameters.AddWithValue("?name_id", nameId);
                insertSearchPhrase.Parameters.AddWithValue("?name", searchPhrase);
                insertSearchPhrase.ExecuteNonQuery();
            }
        }

        private void InsertQueue(SitePage sitePage)
        {
            CheckConnectionState();
            using (var insertQueue = _connection.CreateCommand())
            {
                const string queueInsertSql =
                @"insert into site_page_queue (site_page_id, datetime_income) values (?site_page_id, ?datetime)";

                insertQueue.CommandText = queueInsertSql;
                insertQueue.Parameters.AddWithValue("?site_page_id", sitePage.Id);
                insertQueue.Parameters.AddWithValue("?datetime", DateTime.UtcNow);
                insertQueue.ExecuteNonQuery();
            }
        }

        private int SelectOneQueue()
        {
            CheckConnectionState();
            using (var selectQueueOne = _connection.CreateCommand())
            {
                selectQueueOne.CommandText = SelectQueueSql;

                return (int)selectQueueOne.ExecuteScalar();
            }
        }

        private void DeleteQueue(int id)
        {
            ReopenConnection();
            using (var deleteQueue = _connection.CreateCommand())
            {
                deleteQueue.CommandText = DeleteQueueSql;
                deleteQueue.Parameters.AddWithValue("?id", id);
                deleteQueue.ExecuteNonQuery();
            }
        }

        

        public void EnqueueSitePage(SitePage sitePage, int queueMaxTime = DefaultMaxQueuedTime)
        {
            InsertQueue(sitePage);
        }

        public string DequeueSitePage(int sitePageId)
        {
            _logger.Info("Удаляем из очереди site_page_id = " + sitePageId);
            DeleteQueue(sitePageId);
            return SelectSitePageById(sitePageId);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}