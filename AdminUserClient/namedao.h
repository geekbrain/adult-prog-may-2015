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
    void addName(QString&);

signals:

public slots:

private:
    QList<QString> names_;
    void initTempData();
};

class NamesDecoder {
public:
    NamesDecoder();
};

#endif // NAMEDAO_H
