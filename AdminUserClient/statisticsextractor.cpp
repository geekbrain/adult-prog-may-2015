#include "statisticsextractor.h"
#include "statistics.h"

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
    statistics.reset(new GeneralStatistics());
}
