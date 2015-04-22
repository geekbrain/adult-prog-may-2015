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
