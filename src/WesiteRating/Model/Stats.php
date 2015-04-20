<?php

namespace GeekBrains\WesiteRating\Model;

class Stats extends BaseModel
{

    public function __construct($config)
    {
        parent::__construct($config);
    }

    public function getSummaryStats()
    {
        return $this->dbconnect->Select(
            "SELECT tname.name name, count(tstats.data_fact) stats  FROM " . $this->tableNames['data_cube'] . " tstats " .
            "LEFT JOIN " . $this->tableNames['name'] . " tname ON tname.id = tstats.name_id " .
            "GROUP BY tname.name"
        );
    }

}