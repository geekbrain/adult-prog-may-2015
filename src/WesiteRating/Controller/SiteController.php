<?php

namespace GeekBrains\WesiteRating\Controller;

class SiteController extends BaseController
{
    public function index()
    {
        $name = "Maxim";
        $this->render("site.html.php", [
            "name" => $name
        ]);
    }

}
