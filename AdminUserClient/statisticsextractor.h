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

signals:

public slots:

private:
    void fillTempGeneralStatistics(QSharedPointer<GeneralStatistics>&) const;
    void fillTempSitesList(QSharedPointer<WorkSites> &workSites) const;
    void getNamesFromService();
};

#endif // STATISTICSEXTRACTOR_H
