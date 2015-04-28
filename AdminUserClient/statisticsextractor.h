#ifndef STATISTICSEXTRACTOR_H
#define STATISTICSEXTRACTOR_H

#include <QObject>
#include <QSharedPointer>

class GeneralStatistics;
class NameDao;
class WorkSites;

class StatisticsExtractor : public QObject
{
    Q_OBJECT
public:
    explicit StatisticsExtractor(QObject *parent = 0);
    void getGeneralStatistics(QSharedPointer<GeneralStatistics>&) const;

    /**
     * @brief getWorkSites Сообщает о наборе сайтов, с которых собирается статистика.
     */
    void getWorkSites(QSharedPointer<WorkSites>&) const;

    void getNamesFromService(QSharedPointer<NameDao>&) const;

signals:

public slots:

private:
    void fillTempGeneralStatistics(QSharedPointer<GeneralStatistics>&) const;
    void fillTempSitesList(QSharedPointer<WorkSites> &workSites) const;

    const std::string SoapServiceAddr = "http://adultprog2015.somee.com/WsSoap.svc";
};

#endif // STATISTICSEXTRACTOR_H
