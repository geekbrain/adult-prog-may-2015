#include <QtWidgets>

#include "generalstatwidget.h"
#include "dailystatwidget.h"
#include "namestatwidget.h"
#include "window.h"

//! [0]
Window::Window() :
    names_({"Медведев", "Навальный"}),
    generalStatWidget_(new GeneralStatWidget(Qt::Vertical, tr("Общая статистика"))),
    dailyStatWidget_(new DailyStatWidget(Qt::Vertical, tr("Ежедневная статистика"))),
    nameStatWidget_(new NameStatWidget(names_, Qt::Vertical, tr("Статистика по имени")))
{

    stackedWidget_ = new QStackedWidget;
    stackedWidget_->addWidget(new QWidget(this));
    stackedWidget_->addWidget(generalStatWidget_.data());
    stackedWidget_->addWidget(dailyStatWidget_.data());
    stackedWidget_->addWidget(nameStatWidget_.data());

    createControls(tr("Controls"));
            //! [0]

    //! [1]
    QObject::connect(generalStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(1);
    });
    QObject::connect(dailyStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(2);
    });
    QObject::connect(nameStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(3);
    });
//! [1] //! [2]

    QHBoxLayout *layout = new QHBoxLayout;
    layout->addWidget(controlsGroup_);
    layout->addWidget(stackedWidget_);
    setLayout(layout);

    setWindowTitle(tr("User Plus Admin Client"));
}

void Window::createControls(const QString &title)
//! [3] //! [4]
{
    controlsGroup_ = new QGroupBox(title);

    generalStatBt_ = new QPushButton("Общая статистика", this);
    dailyStatBt_ = new QPushButton("Дневная статистика", this);
    nameStatBt_ = new QPushButton("Имена", this);
//    connect(orientationCombo, SIGNAL(activated(int)),
//            stackedWidget, SLOT(setCurrentIndex(int)));

    QGridLayout *controlsLayout = new QGridLayout;
    controlsLayout->addWidget(generalStatBt_, 0, 0);
    controlsLayout->addWidget(dailyStatBt_, 1, 0);
    controlsLayout->addWidget(nameStatBt_, 2, 0);
    controlsGroup_->setLayout(controlsLayout);
}
//! [8]
