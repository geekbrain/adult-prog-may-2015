#ifndef STATISTICSEXTRACTOR_H
#define STATISTICSEXTRACTOR_H

#include <QObject>
#include <QScopedPointer>

class GeneralStatistics;
class NameDao;

class StatisticsExtractor : public QObject
{
    Q_OBJECT
public:
    explicit StatisticsExtractor(QObject *parent = 0);
    void getGeneralStatistics(QScopedPointer<GeneralStatistics>&) const;

signals:

public slots:

private:
    void fillTempGeneralStatistics(QScopedPointer<GeneralStatistics>&) const;
    void fillTempSitesList();
    void getNamesFromService();
};

#endif // STATISTICSEXTRACTOR_H
