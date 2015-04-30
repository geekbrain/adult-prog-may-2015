#ifndef STATISTICS_H
#define STATISTICS_H

#include <QMap>
#include <QUrl>

class NameDao;

/**
 * @brief The Statistics class
 * Хранит данные статистики в удобном для табличного отображения виде.
 */
class Statistics
{
public:
    Statistics();

    size_t getTupleCount() const;
    size_t getFieldCount() const;
protected:
    size_t tupleCount;
    size_t fieldCount;
};

/**
 * @brief The GeneralStatistics class
 * Для представления общей статистики.
 */
class GeneralStatistics : public Statistics
{
public:
    GeneralStatistics(const QUrl& site);
    void setNameStat(const QString& name, quint32 mentionCount);
    QMap<QString,quint32> getNamesMentions() const;
private:
    QUrl url_; // Сайт, на котором собираем статистику.
    NameDao *names_;
    QMap<QString,quint32> namesMentions_;
};

class StatsByName : public Statistics
{
public:
    StatsByName(const QString& name);
private:
    QString name_; // По которому берем статистику.
};

#endif // STATISTICS_H
