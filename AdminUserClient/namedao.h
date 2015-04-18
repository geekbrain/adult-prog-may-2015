#ifndef NAMEDAO_H
#define NAMEDAO_H

#include <QObject>

class NameDao : public QObject
{
    Q_OBJECT
public:
    explicit NameDao(QObject *parent = 0);
    ~NameDao();

signals:

public slots:
};

#endif // NAMEDAO_H
