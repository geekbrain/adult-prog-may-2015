<?php

namespace Geekbrains\WSWesite\Model;

class FakeStatsModel implements StatsModelInterface
{

    public function getStats()
    {
        return [
            [
                'name'  => 'Навальный',
                'stats' => '89'
            ],
            [
                'name'  => 'Путин',
                'stats' => '25'
            ],
            [
                'name'  => 'Медведев',
                'stats' => '98'
            ]
        ];
    }

    public function getDailyStats($period)
    {
        return [
            '21-04-2015' => [
                [
                    'name'  => 'Навальный',
                    'stats' => '89'
                ],
                [
                    'name'  => 'Путин',
                    'stats' => '25'
                ],
                [
                    'name'  => 'Медведев',
                    'stats' => '98'
                ]
            ],
            '22-04-2015' => [
                [
                    'name'  => 'Навальный',
                    'stats' => '59'
                ],
                [
                    'name'  => 'Путин',
                    'stats' => '45'
                ],
                [
                    'name'  => 'Медведев',
                    'stats' => '18'
                ]
            ],
        ];
    }

    public function getStatsByName($name)
    {
        return [
            'name'  => 'Навальный',
            'stats' => [
                '21-04-2015' => 54,
                '22-04-2015' => 77
            ]
        ];
    }

    public function getNames()
    {
        return [
            [
                'id'   => 2,
                'name' => 'Навальный'
            ],
            [
                'id'   => 4,
                'name' => 'Путин'
            ],
            [
                'id'   => 5,
                'name' => 'Медведев'
            ]
        ];
    }

    public function getSites()
    {
        return [
            [
                'id'  => 2,
                'url' => 'http://lenta.ru'
            ],
            [
                'id'  => 3,
                'url' => 'http://rbk.ru'
            ]
        ];
    }

    public function getPages()
    {
        return [
            [
                'id'   => 12,
                'host' => 'http://lenta.ru',
                'page' => '/atricle/22'
            ],
            [
                'id'   => 14,
                'host' => 'http://rbk.ru',
                'page' => '/news/12'
            ]
        ];
    }

    public function getSearchPhrases($name)
    {
        return [
            [
                'name'    => 'Путин',
                'phrases' => [
                    ['id' => 12, 'phrase' => 'президент РФ'],
                    ['id' => 13, 'phrase' => 'Владимир Владимирович'],
                    ['id' => 15, 'phrase' => 'КрымНаш']
                ]
            ]
        ];
    }

    public function setSite($value)
    {
        $fakeId = rand(10, 100);
        return [
            'id' => $fakeId,
            'url' => $value
        ];
    }

    public function setName($value)
    {
        $fakeId = rand(10, 100);
        return [
            'id' => $fakeId,
            'name' => $value
        ];
    }

    public function setSearchPhrase($name, $value)
    {
        $fakeId = rand(10, 100);
        return [
            'id' => $fakeId,
            'phrase' => $value,
            'name' => 'Путин'
        ];
    }
}