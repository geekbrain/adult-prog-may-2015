<?php

require_once __DIR__.'/vendor/autoload.php';
require_once __DIR__ . '/app/app.php';

$apiVersion = $app["config.route"]['api_version'];
$apiPath = $app["config.route"]['path'];
$api = $app["config.route"]['api'][$apiVersion];
foreach ($api as $routeName => $route) {
    $app->$route['method'](
        $apiPath . "/" . $apiVersion . $route['url'],
        $route['action']
    );
}

$app->run();