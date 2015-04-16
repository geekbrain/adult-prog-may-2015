#ifndef GENERALSTATWIDGET_H
#define GENERALSTATWIDGET_H

#include <QGroupBox>

QT_BEGIN_NAMESPACE
class QComboBox;
class QPushButton;
class QGroupBox;
class QTableWidget;
QT_END_NAMESPACE

class GeneralStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit GeneralStatWidget(Qt::Orientation orientation, const QString &title,
                               QWidget *parent = 0);
//    ~GeneralStatWidget();
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

#endif // GENERALSTATWIDGET_H
