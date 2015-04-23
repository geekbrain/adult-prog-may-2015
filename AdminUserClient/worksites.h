#ifndef WORKSITES_H
#define WORKSITES_H

#include <QObject>
#include <QUrl>
#include <QSet>

/**
 * @brief The WorkSites class
 * Набор сайтов.
 */
class WorkSites : public QObject
{
    Q_OBJECT
public:
    explicit WorkSites(QObject *parent = 0);
    QSet<QUrl> sitesSet() const;
    void append(QUrl&);
signals:

public slots:

private:
    QSet<QUrl> sitesList_;

};

#endif // WORKSITES_H
