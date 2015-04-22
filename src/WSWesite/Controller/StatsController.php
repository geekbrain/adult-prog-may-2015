<?php

namespace Geekbrains\WSWesite\Controller;

use Silex\Application;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

class StatsController
{
    public function index(Request $request, Application $app)
    {
        return "Test";
    }

    public function getStats(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getStats();
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getDailyStats(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getDailyStats(['from' => '21-04-2015', 'to' => '22-04-2015']);
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getStatsByName(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getStatsByName("Навальный");
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getNames(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getNames();
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getSites(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getSites();
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getPages(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getPages();
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function getSearchPhrases(Request $request, Application $app)
    {
        $stats = $app['stats.model']->getSearchPhrases('Путин');
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function setName(Request $request, Application $app)
    {
        $stats = $app['stats.model']->setName('Иванов');
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function setSite(Request $request, Application $app)
    {
        $stats = $app['stats.model']->setSite('http://aif.ru');
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }

    public function setSearchPhrase(Request $request, Application $app)
    {
        $stats = $app['stats.model']->setSearchPhrase('Путин', 'Володька');
        $response = [
            'status' => [
                'code'    => Response::HTTP_OK,
                'message' => 'OK'
            ],
            'result' => $stats
        ];
        return new JsonResponse($response);
    }
}