<?php

require_once "../vendor/autoload.php";
$config = require_once "../config/config.php";

use GeekBrains\WesiteRating\Controller\SiteController as Site;

define("VIEW_PATH", "../src/WesiteRating/View/");

$site = new Site($config);

$site->index();
