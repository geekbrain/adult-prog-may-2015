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
class NameDao;
class StatisticsExtractor;

class Window : public QWidget
{
    Q_OBJECT

public:
    Window(const StatisticsExtractor& statsExtractor);

private:
    /**
     * @brief createControls Создание и размещение виджета с котрольными элементами.
     * @param title Заголовок группы.
     */
    void createControls(const QString &title);

    /**
     * @brief configControls Настройка работы контрольных элементов.
     */
    void configControls() const;

    void fillStackedWidget();
    void configFinalFace();

    NameDao* names_; // Список личностей, о которых смотрится статистика.
    //    StatisticsExtractor *statExtractor_;

    QSharedPointer<GeneralStatWidget> generalStatWidget_;
    QSharedPointer<DailyStatWidget> dailyStatWidget_;
    QSharedPointer<NameStatWidget> nameStatWidget_;

    QStackedWidget *stackedWidget_;
    QGroupBox *controlsGroup_;
    QPushButton *generalStatBt_;
    QPushButton *dailyStatBt_;
    QPushButton *nameStatBt_;

};

#endif
