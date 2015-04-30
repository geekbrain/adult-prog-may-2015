#include "namedao.h"
#include <QDebug>

NameDao::NameDao(QObject *parent) : QObject(parent)
{
    // initTempData();
}

NameDao::~NameDao()
{

}

QList<QString> NameDao::namesList() const
{
    return names_;
}

void NameDao::addName(QString & name)
{
    names_.append(name);
}

void NameDao::initTempData()
{
    names_.append("Медведев");
    names_.append("Навальный");
}

NamesDecoder::NamesDecoder()
{
    createMap();
}

QString NamesDecoder::toString(const QByteArray &srcArr) const
{
    QString result(srcArr);
    for (int i = 0; i < srcArr.size(); ++i)
        if (map_.contains(int(srcArr.at(i))))
            result.replace(i, 1, map_[int(srcArr.at(i))]);
    return result;
}

void NamesDecoder::createMap()
{
    map_.insert(28, QString("М"));
    map_.insert(29, QString("Н"));
    map_.insert(31, QString("П"));
    map_.insert(67, QString("у"));
    map_.insert(66, QString("т"));
    map_.insert(56, QString("и"));
    map_.insert(61, QString("н"));
    map_.insert(53, QString("е"));
    map_.insert(52, QString("д"));
    map_.insert(50, QString("в"));
    map_.insert(48, QString("а"));
    map_.insert(59, QString("л"));
    map_.insert(76, QString("ь"));
    map_.insert(75, QString("ы"));
    map_.insert(57, QString("й"));
}
