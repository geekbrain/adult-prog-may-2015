<?php

namespace Geekbrains\WSWesite\Model;

use Geekbrains\WSWesite\Lib\DB;

class StatsModel implements StatsModelInterface
{
    /** @var  DB $db */
    protected $db;

    protected $tableNames;

    public function __construct($dbConnection, $tablesNames)
    {
        $this->db = $dbConnection;
        $this->tableNames = $tablesNames;
    }

    public function getStats()
    {
        return $this->db->Select(
            "SELECT tname.name name, count(tstats.data_fact) stats  " .
            "FROM " . $this->tableNames['data_cube'] . " tstats " .
            "LEFT JOIN " . $this->tableNames['name'] . " tname ON tname.id = tstats.name_id " .
            "GROUP BY tname.name"
        );
    }

    public function getDailyStats($period)
    {
        $stats = [];
        $rawStats = $this->db->Select(
            "SELECT tstats.date, tname.name name, count(tstats.data_fact) stats  " .
            "FROM " . $this->tableNames['data_cube'] . " tstats " .
            "LEFT JOIN " . $this->tableNames['name'] . " tname ON tname.id = tstats.name_id " .
            "GROUP BY tstats.date, tname.name"
        );
        foreach ($rawStats as $rowStats) {
            $stats[$rowStats['date']][] = [
                'name' => $rowStats['name'],
                'stats' => $rowStats['stats']
            ];
        }
        return $stats;
    }

    public function getStatsByName($name)
    {
        $stats = [
            'name' => $name,
            'stats' => []
        ];
        $rawStats = $this->db->Select(
            "SELECT tstats.date, tname.name name, count(tstats.data_fact) stats  " .
            "FROM " . $this->tableNames['data_cube'] . " tstats " .
            "LEFT JOIN " . $this->tableNames['name'] . " tname ON tname.id = tstats.name_id " .
            "WHERE tname.name = '" . $name . "' " .
            "GROUP BY tstats.date"
        );
        foreach ($rawStats as $rowStats) {
            $stats['stats'][$rowStats['date']] = $rowStats['stats'];
        }
        return $stats;
    }

    public function getNames()
    {
        return $this->db->Select(
            "SELECT id, name  FROM " . $this->tableNames['name']
        );
    }

    public function getSites()
    {
        return $this->db->Select(
            "SELECT id, url  FROM " . $this->tableNames['site']
        );
    }

    public function getPages()
    {
        return $this->db->Select(
            "SELECT page.id, page.url page, site.url host " .
            "FROM " . $this->tableNames['site_page'] . " page " .
            "LEFT JOIN " . $this->tableNames['site'] . " site ON site.id = page.site_id "
        );
    }

    public function getSearchPhrases($name)
    {
        $stats = [
            'name' => $name,
            'phrases' => []
        ];
        $rawStats = $this->db->Select(
            "SELECT phrase.id, phrase.name phrase " .
            "FROM " . $this->tableNames['search_phrase'] . " phrase " .
            "LEFT JOIN " . $this->tableNames['name'] . " tname ON tname.id = phrase.name_id " .
            "WHERE tname.name = '" . $name . "' "
        );
        foreach ($rawStats as $rowStats) {
            $stats['phrases'][] = [
                'id' => $rowStats['id'],
                'phrase' => $rowStats['phrase']
            ];
        }
        return [$stats];
    }

    public function setSite($value)
    {
        $this->db->Insert(
            $this->tableNames['site'],
            [
                'url' => $value
            ]
        );
        return [
            'id' => $this->db->lastInsertId(),
            'url' => $value
        ];
    }

    public function setName($value)
    {
        $this->db->Insert(
            $this->tableNames['name'],
            [
                'name' => $value
            ]
        );
        return [
            'id' => $this->db->lastInsertId(),
            'name' => $value
        ];
    }

    public function setSearchPhrase($name, $value)
    {
        $nameRow = $this->db->Select(
            "SELECT id, name  FROM " . $this->tableNames['name'] . " WHERE name = '" . $name . "'"
        );

        $this->db->Insert(
            $this->tableNames['search_phrase'],
            [
                'name' => $value,
                'name_id' => $nameRow[0]['id']
            ]
        );
        return [
            'id' => $this->db->lastInsertId(),
            'phrase' => $value,
            'name' => $name
        ];
    }
}