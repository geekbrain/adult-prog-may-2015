<?php

namespace Geekbrains\WSWesite\Model;

interface StatsModelInterface
{
    public function getStats();
    public function getDailyStats($period);
    public function getStatsByName($name);
    public function getNames();
    public function getSites();
    public function getPages();
    public function getSearchPhrases($name);
    public function setSite($value);
    public function setName($value);
    public function setSearchPhrase($name, $value);
}