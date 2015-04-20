<?php

namespace GeekBrains\WesiteRating\Model;

use PDO;

class BaseModel
{

    /** @var DB $dbconnect */
    protected $dbconnect;

    protected $tableNames;

    public function __construct($config)
    {
        $pdo = new PDO(
            sprintf(
                "mysql:host=%s;dbname=%s",
                $config['db']['host'],
                $config['db']['db_name']
            ),
            $config['db']['db_user'],
            $config['db']['db_pass']
        );
        DB::setConnection($pdo);
        $this->dbconnect = DB::getConnection();
        $this->tableNames = $config['tables'];
    }

}