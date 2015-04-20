#ifndef NAMEDAO_H
#define NAMEDAO_H

#include <QObject>
#include <QList>

class NameDao : public QObject
{
    Q_OBJECT
public:
    explicit NameDao(QObject *parent = 0);
    ~NameDao();

    QList<QString> namesList() const;
signals:

public slots:

private:
    QList<QString> names_;
    void initTempData();
};

#endif // NAMEDAO_H
