#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include "worksites.h"
#include "gSoap/soapBasicHttpBinding_USCOREIServiceProxy.h"
#include "gSoap/BasicHttpBinding_USCOREIService.nsmap"
#include <QUrl>
#include <QDebug>
#include <string>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent)
{
    getNamesFromService();
}

void StatisticsExtractor::getGeneralStatistics(QSharedPointer<GeneralStatistics>& statistics) const
{
    fillTempGeneralStatistics(statistics);
}

void StatisticsExtractor::getWorkSites(QSharedPointer<WorkSites> &workSites) const
{
    _ns1__GetSites ns1__GetSites;
    _ns1__GetSitesResponse response;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
    if (server.GetSites(&ns1__GetSites, response) == SOAP_OK) {
        //        qDebug() << "Функция GetSites вызвана успешно";

        size_t countOfSites = response.GetSitesResult->__sizeKeyValueOfintstring;
        qDebug() << response.GetSitesResult->KeyValueOfintstring->Key;
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
//    _ns1__GetNames ns1__GetNames;
//    _ns1__GetNamesResponse ns1__GetNamesResponse;
//    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
//    if (server.GetNames(&ns1__GetNames, ns1__GetNamesResponse) == SOAP_OK) {
//        qDebug() << "Функция GetNames вызвана успешно";
//    }
//    else
//        qDebug() << "Провал: функция GetNames не сработала.";
//    std::string str(ns1__GetNamesResponse.GetNamesResult->KeyValueOfintstring->Value);
//    qDebug() << ns1__GetNamesResponse.GetNamesResult->__sizeKeyValueOfintstring;
//    qDebug() << QString::fromStdString(str);
    _ns1__GetNamesDictionary ns1__GetNamesDictionary;
    _ns1__GetNamesDictionaryResponse ns1__GetNamesDictionaryResponse;
    BasicHttpBinding_USCOREIServiceProxy server(SoapServiceAddr.data());
    server.GetNamesDictionary(&ns1__GetNamesDictionary, ns1__GetNamesDictionaryResponse);
    int size = ns1__GetNamesDictionaryResponse.GetNamesDictionaryResult->KeyValueOfstringArrayOfstringty7Ep6D1->Value->__sizestring;
    qDebug() << size;
//    for (int i = 0; i < size; ++i) {
//        qDebug() << QString::fromLatin1(ns1__GetNamesDictionaryResponse.GetNamesDictionaryResult->
//                                        KeyValueOfstringArrayOfstringty7Ep6D1->Value->string[i],
//                                        ns1__GetNamesDictionaryResponse.GetNamesDictionaryResult->KeyValueOfstringArrayOfstringty7Ep6D1->Value->__sizestring);
//    }

}
