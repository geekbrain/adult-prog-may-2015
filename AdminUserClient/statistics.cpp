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


GeneralStatistics::GeneralStatistics()
{
    fieldCount = 2; // Согласно п. 1.1.2.1. документации: это поля «Имя» и «Количество упоминаний».
}
