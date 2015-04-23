#ifndef STATISTICSEXTRACTOR_H
#define STATISTICSEXTRACTOR_H

#include <QObject>
#include <QScopedPointer>

class GeneralStatistics;
class NameDao;
class WorkSites;

class StatisticsExtractor : public QObject
{
    Q_OBJECT
public:
    explicit StatisticsExtractor(QObject *parent = 0);
    void getGeneralStatistics(QScopedPointer<GeneralStatistics>&) const;

    /**
     * @brief getWorkSites Сообщает о наборе сайтов, с которых собирается статистика.
     */
    void getWorkSites(QScopedPointer<WorkSites>&) const;

signals:

public slots:

private:
    void fillTempGeneralStatistics(QScopedPointer<GeneralStatistics>&) const;
    void fillTempSitesList(QScopedPointer<WorkSites> &workSites);
    void getNamesFromService();
};

#endif // STATISTICSEXTRACTOR_H
