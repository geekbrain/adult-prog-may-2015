#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include "worksites.h"
#include "gSoap/soapBasicHttpBinding_USCOREIServiceProxy.h"
#include "gSoap/BasicHttpBinding_USCOREIService.nsmap"
#include <QUrl>
#include <QDebug>
#include <QTextCodec>
#include <string>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent)
{
}

void StatisticsExtractor::getGeneralStatistics(QSharedPointer<GeneralStatistics>& statistics) const
{
    _ns1__GetStats ns1__GetStats;
    _ns1__GetStatsResponse response;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());

    if (server.GetStats(&ns1__GetStats, response) == SOAP_OK) {
        size_t countOfNames = response.GetStatsResult->__sizeKeyValueOfstringint;
        for (size_t nameIndex = 0; nameIndex < countOfNames; ++nameIndex) {
            QByteArray encodedString = response.GetStatsResult->KeyValueOfstringint[nameIndex].Key;
            NamesDecoder namesDecoder;
            QString name = namesDecoder.toString(encodedString);

            statistics->setNameStat(
                name,
                response.GetStatsResult->KeyValueOfstringint[nameIndex].Value
            );
        }
    }
    else {
        // ... Проинформировать об ошибке.
    }
}

int StatisticsExtractor::getNameStatistics(QSharedPointer<StatsByName> &statistics) const
{
    _ns1__GetStatsByName ns1__GetStatsByName;
    _ns1__GetStatsByNameResponse response;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
    int status = -1;
    if (server.GetStatsByName(&ns1__GetStatsByName, response) == SOAP_OK) {
        status = 0;
    }
    return status;
}

void StatisticsExtractor::getWorkSites(QSharedPointer<WorkSites> &workSites) const
{
    _ns1__GetSites ns1__GetSites;
    _ns1__GetSitesResponse response;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
    if (server.GetSites(&ns1__GetSites, response) == SOAP_OK) {
//        qDebug() << "Функция GetSites вызвана успешно";

        size_t countOfSites = response.GetSitesResult->__sizeKeyValueOfintstring;
        for (size_t siteIndex = 0; siteIndex < countOfSites; ++siteIndex) {
            std::string stdSiteName =
                    std::string(response.GetSitesResult->KeyValueOfintstring[siteIndex].Value);
            QUrl::ParsingMode parsingMode = QUrl::StrictMode;
            QUrl url(QString::fromStdString(stdSiteName), parsingMode);
            workSites->append(url);
        }
    }
    else {
//        qDebug() << "Провал: функция GetNames не сработала.";
    }
}

int StatisticsExtractor::getNamesFromService(QSharedPointer<NameDao> &names) const
{
    int operationStatus = -1;
    _ns1__GetNames ns1__GetNames;
    _ns1__GetNamesResponse ns1__GetNamesResponse;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
    if (server.GetNames(&ns1__GetNames, ns1__GetNamesResponse) == SOAP_OK) {
        operationStatus = 0;

        size_t countOfNames = ns1__GetNamesResponse.GetNamesResult->__sizeKeyValueOfintstring;
        for (size_t nameIndex = 0; nameIndex < countOfNames; ++nameIndex) {
            QByteArray encodedString = ns1__GetNamesResponse.GetNamesResult->
                                            KeyValueOfintstring[nameIndex].Value;
            NamesDecoder namesDecoder;
            QString name = namesDecoder.toString(encodedString);
            names->addName(name);
        }
    }
    return operationStatus;
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


