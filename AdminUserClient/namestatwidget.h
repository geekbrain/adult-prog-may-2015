#ifndef NAMESTATWIDGET_H
#define NAMESTATWIDGET_H

#include <QGroupBox>
#include <QList>

QT_BEGIN_NAMESPACE
class QComboBox;
class QPushButton;
class QGroupBox;
class QTableWidget;
class QCalendarWidget;
QT_END_NAMESPACE

class NameStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit NameStatWidget(QList<QString> names, Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:

private:
    QList<QString> names; // Список лиц, по которым подсчитываем статистику.
    QGroupBox *leftGroup;
    QGroupBox *rightGroup;
    QComboBox *sitesCombo;
    QComboBox *namesCombo;
//    QCalendarWidget *beginPeriod;
//    QCalendarWidget *endPeriod;
    QPushButton *okBt;

};

#endif // NAMESTATWIDGET_H
