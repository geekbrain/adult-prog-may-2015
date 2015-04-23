#include "worksites.h"

WorkSites::WorkSites(QObject *parent) :
    QObject(parent)
{
}

QSet<QUrl> WorkSites::sitesList() const
{
    return sitesList_;
}

void WorkSites::append(QUrl &url)
{
    if (!sitesSet_.contains(url))
        sitesSet_ << url;
}
