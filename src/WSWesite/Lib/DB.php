<?php

namespace Geekbrains\WSWesite\Lib;

use GeekBrains\WSWesite\Exception\DBException;
use \PDO as PDO;

class DB {

    /** @var  PDO $_connection */
    protected static $_connection;
    protected $db;
    private static $instance;

    private function __construct()
    {
        if (!(self::$_connection instanceof PDO)) {
            throw new DBException("DB connection is not set.");
        }
        $this->db = self::$_connection;
    }

    private function __clone() {}

    /**
     * @return mixed
     */
    public static function getConnection()
    {
        if (self::$instance == null)
            self::$instance = new static();

        return self::$instance;
    }

    /**
     * @param mixed $connection
     */
    public static function setConnection($connection)
    {
        self::$_connection = $connection;
    }

    public function Select($query)
    {
        $result = $this->db->query($query);

        if ($result === false) {
            $error = $this->db->errorInfo();
            die("Не выполнен запрос: ".$query."<br>Error: ".$error[2]);
        }

        $n = $result->rowCount();
        $arr = [];

        for($i = 0; $i < $n; $i++)
        {
            $row = $result->fetch(PDO::FETCH_ASSOC);
            $arr[] = $row;
        }

        return $arr;
    }

    public function Insert($table, $object)
    {
        $columns = [];
        $values = [];

        foreach ($object as $key => $value)
        {
            //$key = $this->db->quote($key . '');
            $columns[] = $key;

            if ($value === null)
            {
                $values[] = 'NULL';
            }
            else {
                $value = $this->db->quote($value . '');
                $values[] = "$value";
            }
        }

        $columns_s = implode(',', $columns);
        $values_s = implode(',', $values);

        $query = "INSERT INTO $table ($columns_s) VALUES ($values_s)";
        $result = $this->db->exec($query);
        if ($result === false) {
            $error = $this->db->errorInfo();
            die("Не выполнен запрос: ".$query."<br>Error: ".$error[2]);
        }

        return $this->db->lastInsertId();
    }

    public function Update($table, $object, $where)
    {
        $sets = [];

        foreach ($object as $key => $value)
        {
            //$key = $this->db->quote($key . '');

            if ($value === null)
            {
                $sets[] = "$value=NULL";
            }
            else
            {
                $value = $this->db->quote($value . '');
                $sets[] = "$key=$value";
            }
        }

        $sets_s = implode(',', $sets);
        $query = "UPDATE $table SET $sets_s WHERE $where";
        $result = $this->db->query($query);

        if ($result === false) {
            $error = $this->db->errorInfo();
            die("Не выполнен запрос: ".$query."<br>Error: ".$error[2]);
        }

        return $result->rowCount();
    }

    public function Delete($table, $where)
    {
        $query = "DELETE FROM $table WHERE $where";
        $result = $this->db->exec($query);


        if ($result === false) {
            $error = $this->db->errorInfo();
            die("Не выполнен запрос: ".$query."<br>Error: ".$error[2]);
        }

        return $result;
    }

    public function lastInsertId()
    {
        return self::$_connection->lastInsertId();
    }
}