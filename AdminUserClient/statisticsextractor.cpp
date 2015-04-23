#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include <QUrl>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent)
{
}

void StatisticsExtractor::getGeneralStatistics(QScopedPointer<GeneralStatistics>& statistics) const
{
    fillTempGeneralStatistics(statistics);
}

void StatisticsExtractor::fillTempGeneralStatistics(QScopedPointer<GeneralStatistics>& statistics) const
{
    QUrl url("localhost");
    statistics.reset(new GeneralStatistics(url));

}

void StatisticsExtractor::getNamesFromService()
{
}
