<?php

$dbConfig = parse_ini_file("db_config.ini", true);

return [
    'db' => $dbConfig['DB'],
    'tables' => [
        'data_cube' => 'data_cube',
        'site' => 'site',
        'name' => 'name',
        'search_phrase' => 'search_phrase',
        'site_page' => 'site_page'
    ]
];