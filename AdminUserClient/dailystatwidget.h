#ifndef DAILYSTATWIDGET_H
#define DAILYSTATWIDGET_H

#include <QGroupBox>

QT_BEGIN_NAMESPACE
class QComboBox;
class QPushButton;
class QGroupBox;
class QTableWidget;
QT_END_NAMESPACE

class DailyStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit DailyStatWidget(Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:

private:
    QGroupBox *sitesGroup_;
    QComboBox *sitesCombo_;
    QPushButton *okBt_;
    QTableWidget *table_;
    QGroupBox *leftGroup_;
    QGroupBox *rightGroup_;
};

#endif // DAILYSTATWIDGET_H
