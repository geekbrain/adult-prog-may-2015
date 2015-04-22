<?php

namespace Geekbrains\WSWesite\Controller;

use Silex\Application;
use Symfony\Component\HttpFoundation\Request;

class StatsController
{
    public function index(Request $request, Application $app)
    {
        return "Hello";
    }
}