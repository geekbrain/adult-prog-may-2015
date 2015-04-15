<?php

namespace GeekBrains\WesiteRating\Controller;

class BaseController
{
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
