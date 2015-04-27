#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include "worksites.h"
#include "gSoap/soapBasicHttpBinding_USCOREIServiceProxy.h"
#include "gSoap/BasicHttpBinding_USCOREIService.nsmap"
#include <QUrl>
#include <QDebug>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent)
{
    _ns1__GetNames ns1__GetNames;
    _ns1__GetNamesResponse ns1__GetNamesResponse;
    BasicHttpBinding_USCOREIServiceProxy server("http://adultprog2015.somee.com/WsSoap.svc");
    if (server.GetNames(&ns1__GetNames, ns1__GetNamesResponse) == SOAP_OK) {
        qDebug() << "True";
    }
    else
        qDebug() << "FAIL";

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
