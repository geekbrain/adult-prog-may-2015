#include <QtWidgets>

#include "generalstatwidget.h"
#include "dailystatwidget.h"
#include "namestatwidget.h"
#include "window.h"
#include "namedao.h"
#include "statisticsextractor.h"

Window::Window(const StatisticsExtractor& statsExtractor) :
    names_(new NameDao(this)),
//    statExtractor_(new StatisticsExtractor(this)),
    generalStatWidget_(new GeneralStatWidget(statsExtractor, Qt::Vertical, tr("Общая статистика"))),
    dailyStatWidget_(new DailyStatWidget(names_, Qt::Vertical, tr("Ежедневная статистика"))),
    nameStatWidget_(new NameStatWidget(names_, Qt::Vertical, tr("Статистика по имени"))),
    stackedWidget_(new QStackedWidget(this))
{
    fillStackedWidget();
    createControls(tr("Controls"));
    configControls();
    configFinalFace();
}

void Window::createControls(const QString &title)
{
    controlsGroup_ = new QGroupBox(title);

    generalStatBt_ = new QPushButton("Общая статистика", this);
    dailyStatBt_ = new QPushButton("Дневная статистика", this);
    nameStatBt_ = new QPushButton("Имена", this);

    QGridLayout *controlsLayout = new QGridLayout;
    controlsLayout->addWidget(generalStatBt_, 0, 0);
    controlsLayout->addWidget(dailyStatBt_, 1, 0);
    controlsLayout->addWidget(nameStatBt_, 2, 0);
    controlsLayout->setRowStretch(3, 1);
    controlsGroup_->setLayout(controlsLayout);
}

void Window::configControls() const
{
    QObject::connect(generalStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(1);
    });
    QObject::connect(dailyStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(2);
    });
    QObject::connect(nameStatBt_, &QPushButton::clicked, [&](){
        stackedWidget_->setCurrentIndex(3);
    });
}

void Window::fillStackedWidget()
{
    stackedWidget_->addWidget(new QWidget(this));
    stackedWidget_->addWidget(generalStatWidget_.data());
    stackedWidget_->addWidget(dailyStatWidget_.data());
    stackedWidget_->addWidget(nameStatWidget_.data());
}

void Window::configFinalFace()
{
    QHBoxLayout *layout = new QHBoxLayout;
    layout->addWidget(controlsGroup_);
    layout->addWidget(stackedWidget_);
    setLayout(layout);

    setWindowTitle(tr("User Plus Admin Client"));
}
