#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include "worksites.h"
#include <QUrl>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent)
{
}

void StatisticsExtractor::getGeneralStatistics(QSharedPointer<GeneralStatistics>& statistics) const
{
    fillTempGeneralStatistics(statistics);
}

void StatisticsExtractor::getWorkSites(QSharedPointer<WorkSites> &workSites) const
{
    fillTempSitesList(workSites);
}

void StatisticsExtractor::fillTempGeneralStatistics(QSharedPointer<GeneralStatistics>& statistics) const
{
    QUrl url("localhost");
    statistics.reset(new GeneralStatistics(url));
    statistics->setNameStat("Медведев", 405);
    statistics->setNameStat("Навальный", 220);
}

void StatisticsExtractor::fillTempSitesList(QSharedPointer<WorkSites> &workSites) const
{
    workSites.reset(new WorkSites);
    workSites->append(QUrl::fromUserInput("lenta.ru"));
}

void StatisticsExtractor::getNamesFromService()
{
}
