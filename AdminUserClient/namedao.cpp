#include "namedao.h"

NameDao::NameDao(QObject *parent) : QObject(parent)
{
    initTempData();
}

NameDao::~NameDao()
{

}

QList<QString> NameDao::namesList() const
{
    return names_;
}

void NameDao::initTempData()
{
    names_.append("Медведев");
    names_.append("Навальный");
}

