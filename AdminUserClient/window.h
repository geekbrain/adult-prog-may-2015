#ifndef WINDOW_H
#define WINDOW_H

#include <QWidget>
#include <QSharedPointer>
#include <QList>

QT_BEGIN_NAMESPACE
class QGroupBox;
class QPushButton;
class QStackedWidget;
QT_END_NAMESPACE
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

    QList<QString> names_; // Список личностей, о которых смотрится статистика.

    QSharedPointer<GeneralStatWidget> generalStatWidget_;
    QSharedPointer<DailyStatWidget> dailyStatWidget_;
    QSharedPointer<NameStatWidget> nameStatWidget_;

    QStackedWidget *stackedWidget_;

    QGroupBox *controlsGroup_;
    QPushButton *generalStatBt_;
    QPushButton *dailyStatBt_;
    QPushButton *nameStatBt_;

};
//! [0]

#endif
