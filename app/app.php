<?php

use Monolog\Handler\StreamHandler;
use Monolog\Logger;
use Silex\Application;
use Symfony\Component\Debug\ErrorHandler;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Yaml\Parser;

ini_set('display_errors', 0);
date_default_timezone_set('Europe/Moscow');

$app = new Application();

if ($_SERVER['REMOTE_ADDR'] === '127.0.0.1' && substr($_SERVER['HTTP_HOST'], -4) === '.dev') {
    $app['debug'] = true;
}

$app['logger'] = $app->share(function() {
    return new Logger(
        'service',
        array(
            new StreamHandler(__DIR__ . '/../log/default.log'),
        )
    );
});

// Converting Errors to Exceptions
ErrorHandler::register();

$app->error(function (Exception $e, $code = 500) use ($app) {

    /** @var Logger $logger */
    $logger = $app['logger'];

    $logger->addError(
        $e->getMessage(),
        array(
            'ErrorCode' => $code,
            'ExceptionCode' => $e->getCode(),
            'ExceptionTrace' => $e->getTrace(),
        )
    );

    if ($code == 500) {
        $code = $e->getCode();
    }

    /*$apiResponse = new ApiResponse(array(), $code);

    if ($app['debug']) {
        return $apiResponse
            ->setError($e->getMessage(), $e->getTrace())
            ->getJsonResponse();
    }

    if ($e instanceof ServiceException) {
        return $apiResponse
            ->setError($e->getMessage())
            ->getJsonResponse();
    }

    return $apiResponse->setError('Error')
        ->getJsonResponse();*/

});

$yaml = new Parser();

$app['config'] = $yaml->parse(file_get_contents(__DIR__ . '/../app/config/app.yml'));
$app['config.route'] = $yaml->parse(file_get_contents(__DIR__ . '/../app/config/route.yml'));

/*$app['event.dispatcher'] = $app->share(function() use ($app) {
    $dispatcher = new Dispatcher(
        $app['config.configuration']['config.services']['dispatcher']['v1']['url'] .
        $app['config.configuration']['api']['dispatcher']['addEvent'],
        $app['config.configuration']['config.system-token']
    );
    return $dispatcher;

});*/

return $app;