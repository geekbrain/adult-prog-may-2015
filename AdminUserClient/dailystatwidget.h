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
    QGroupBox *resultGroup_;

    void configControlArea() const;
    void configResultsArea() const;
    void resultTableTuning() const;
    void setFinalFace(Qt::Orientation orientation);

private slots:
    void fillTableTempData() const;
};

#endif // DAILYSTATWIDGET_H
