#ifndef NAMESTATWIDGET_H
#define NAMESTATWIDGET_H

#include <QGroupBox>

class NameStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit NameStatWidget(Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:
};

#endif // NAMESTATWIDGET_H
