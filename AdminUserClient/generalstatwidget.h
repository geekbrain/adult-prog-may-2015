#ifndef GENERALSTATWIDGET_H
#define GENERALSTATWIDGET_H

#include <QWidget>

QT_BEGIN_NAMESPACE
class QGroupBox;
class QPushButton;
class QComboBox;
QT_END_NAMESPACE

class GeneralStatWidget : public QWidget
{
    Q_OBJECT
public:
    explicit GeneralStatWidget(QWidget *parent = 0);
    ~GeneralStatWidget();

    QGroupBox *sitesGroup_;
    QComboBox *sitesCombo_;
    QPushButton *okBt_;
signals:

public slots:
};

#endif // GENERALSTATWIDGET_H
