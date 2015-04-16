#ifndef WINDOW_H
#define WINDOW_H

#include <QWidget>

QT_BEGIN_NAMESPACE
class QGroupBox;
class QPushButton;
class QStackedWidget;
QT_END_NAMESPACE
class Area2;
class GeneralStatWidget;
class DailyStatWidget;
class NameStatWidget;

//! [0]
class Window : public QWidget
{
    Q_OBJECT

public:
    Window();

private:
    void createControls(const QString &title);

    Area2 *horizontalSliders_;
    Area2 *verticalSliders_;
    GeneralStatWidget* generalStatWidget_;
    DailyStatWidget* dailyStatWidget_;
    NameStatWidget* nameStatWidget_;

    QStackedWidget *stackedWidget_;

    QGroupBox *controlsGroup_;
    QPushButton *generalStatBt_;
    QPushButton *dailyStatBt_;
    QPushButton *nameStatBt_;
};
//! [0]

#endif
