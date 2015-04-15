#ifndef SLIDERSGROUP_H
#define SLIDERSGROUP_H

#include <QGroupBox>

QT_BEGIN_NAMESPACE
class QDial;
class QScrollBar;
class QSlider;
class QComboBox;
class QPushButton;
QT_END_NAMESPACE

//! [0]
class Area2 : public QGroupBox
{
    Q_OBJECT

public:
    Area2(Qt::Orientation orientation, const QString &title,
                 QWidget *parent = 0);

signals:
    void valueChanged(int value);

public slots:
    void setValue(int value);
    void setMinimum(int value);
    void setMaximum(int value);
    void invertAppearance(bool invert);
    void invertKeyBindings(bool invert);

private:
    QSlider *slider;
    QScrollBar *scrollBar;
    QDial *dial;

    QGroupBox *sitesGroup_;
    QComboBox *sitesCombo_;
    QPushButton *okBt_;
};
//! [0]

#endif
