<?php

require_once __DIR__.'/../vendor/autoload.php';
require_once __DIR__ . '/../app/app.php';

foreach ($app["config.route"] as $routeName => $route) {
    $app->$route['method'](
        $route['url'],
        $route['action']
    );
}

$app->run();