#include "namedao.h"

NameDao::NameDao(QObject *parent) : QObject(parent)
{
    initTempData();
}

NameDao::~NameDao()
{

}

QList<QString> NameDao::names() const
{
    return names_;
}

void NameDao::initTempData()
{
    names_ = {"Медведев", "Навальный"};
}

