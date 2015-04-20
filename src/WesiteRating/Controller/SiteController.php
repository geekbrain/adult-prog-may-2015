<?php

namespace GeekBrains\WesiteRating\Controller;

use ReflectionClass;

class SiteController extends BaseController
{
    public function __construct($config)
    {
        parent::__construct($config);
    }

    public function index()
    {
        $action = $this->getActionName($_GET['q']);
        if (!$action) {
            $this->render("site.html.php", []);
        } else {
            $stats = $action->invoke($this->statsModel);
            $this->render("empty.html.php", [
                'response' => json_encode($stats)
            ]);
        }
    }

    public function getActionName($q)
    {
        $info = explode('/', $q);
        $params = array();

        foreach ($info as $v)
        {
            if ($v != '')
                $params[] = $v;
        }

        $ref = new ReflectionClass($this->statsModel);
        if ($ref->hasMethod($params[0])) {
            return $ref->getMethod($params[0]);
        } else {
            return false;
        }
    }

}
