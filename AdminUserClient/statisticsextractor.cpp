#include "statisticsextractor.h"
#include "statistics.h"
#include "namedao.h"
#include "worksites.h"
#include <QUrl>
#include <QDebug>

StatisticsExtractor::StatisticsExtractor(QObject *parent) :
    QObject(parent),
    service_(new Service(this))
{
    // Адрес сервиса.
    service_->setEndPoint(QLatin1String("http://adultprog2015.somee.com/WsSoap.svc"));

    service_->setSoapVersion(KDSoapClientInterface::SOAP1_2); //-- Протокол

    //** Присоединяем сигналы к слотам **//

    connect(service_, SIGNAL(getNamesError(KDSoapMessage)),
            this, SLOT(onGetNamesError(KDSoapMessage)));

    connect(service_, SIGNAL(getNamesDone(TNS__GetNamesResponse)),
            this, SLOT(onGetNamesDone(TNS__GetNamesResponse)));

    /*TNS__GetNames*/TNS__GetStatsByName params; // Парaметры для вызова метода

    // Вызываем метод асинхронно, что бы не блокировать выполнение приложения.
    service_->asyncGetStatsByName(params);
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

void StatisticsExtractor::onGetNamesDone(const TNS__GetNamesResponse &parameters)
{
    qDebug() << "Get Names Done!";
}

void StatisticsExtractor::onGetNamesError(const KDSoapMessage &fault)
{
    qDebug() << "Get Names Error!";
}
