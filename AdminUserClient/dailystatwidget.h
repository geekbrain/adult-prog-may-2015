#ifndef DAILYSTATWIDGET_H
#define DAILYSTATWIDGET_H

#include <QGroupBox>

class DailyStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit DailyStatWidget(Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:
};

#endif // DAILYSTATWIDGET_H
