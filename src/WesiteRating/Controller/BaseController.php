<?php

namespace GeekBrains\WesiteRating\Controller;

use GeekBrains\WesiteRating\Model\Stats;

class BaseController
{
    protected $config;
    protected $statsModel;

    public function __construct($config)
    {
        $this->config = $config;
        $this->statsModel = new Stats($config);
    }

    protected function template($fileName, $vars = array())
	{
		foreach ($vars as $k => $v)
		{
			$$k = $v;
		}

		ob_start();
		include __dir__ . "/../View/" . "$fileName";
		return ob_get_clean();
	}

    public function render($fileName, $vars = array())
    {
        $page = $this->template($fileName, $vars);
		echo $page;
    }
}
