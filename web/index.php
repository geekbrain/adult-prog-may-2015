<?php

require_once "../vendor/autoload.php";

use GeekBrains\WesiteRating\Controller\SiteController as Site;

define(VIEW_PATH, "../src/WesiteRating/View/");

$site = new Site();

$site->index();
