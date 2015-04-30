#include "statistics.h"

Statistics::Statistics()
{
}

size_t Statistics::getTupleCount() const
{
    return tupleCount;
}

size_t Statistics::getFieldCount() const
{
    return fieldCount;
}


GeneralStatistics::GeneralStatistics(const QUrl& site) :
    url_(site)
{
    fieldCount = 2; // Согласно п. 1.1.2.1. документации: это поля «Имя» и «Количество упоминаний».
    tupleCount = 0;
}

void GeneralStatistics::setNameStat(const QString &name, quint32 mentionCount)
{
    if (!namesMentions_.contains(name))
        tupleCount++;
    namesMentions_[name] = mentionCount;
}

QMap<QString, quint32> GeneralStatistics::getNamesMentions() const
{
    return namesMentions_;
}


StatsByName::StatsByName()
    : status_(false)
{

}

bool StatsByName::status() const
{
    return status_;
}

void StatsByName::setSite(const QUrl &u)
{
    parameters_.site = u;
    resetStatus();
}

void StatsByName::setPerson(const QString &name)
{
    parameters_.person = name;
    resetStatus();
}

void StatsByName::setCountOfPage(size_t count)
{
    parameters_.countOfPage = count;
    resetStatus();
}

void StatsByName::setDate(const QDate &first, const QDate &last)
{
    parameters_.firstDay = first;
    parameters_.lastDay = last;
    resetStatus();
}

void StatsByName::resetStatus()
{
    status_ = false;
}
