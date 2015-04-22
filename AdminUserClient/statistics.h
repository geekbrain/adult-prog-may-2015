#ifndef STATISTICS_H
#define STATISTICS_H

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

#endif // STATISTICS_H
