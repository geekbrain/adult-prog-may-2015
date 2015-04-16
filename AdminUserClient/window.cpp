#include <QtWidgets>

#include "area2.h"
#include "generalstatwidget.h"
#include "dailystatwidget.h"
#include "namestatwidget.h"
#include "window.h"

//! [0]
Window::Window() :
    generalStatWidget_(new GeneralStatWidget(Qt::Vertical, tr("Общая статистика"))),
    dailyStatWidget_(new DailyStatWidget(Qt::Vertical, tr("Ежедневная статистика"))),
    nameStatWidget_(new NameStatWidget(Qt::Vertical, tr("Статистика по именам")))
{
    horizontalSliders_ = new Area2(Qt::Horizontal, tr("Horizontal"));
    verticalSliders_ = new Area2(Qt::Vertical, tr("Vertical"));


    stackedWidget_ = new QStackedWidget;
    stackedWidget_->addWidget(generalStatWidget_.data());
    stackedWidget_->addWidget(dailyStatWidget_.data());
    stackedWidget_->addWidget(nameStatWidget_.data());
    stackedWidget_->addWidget(horizontalSliders_);
    stackedWidget_->addWidget(verticalSliders_);

    createControls(tr("Controls"));
//! [0]

//! [1]
    connect(horizontalSliders_, SIGNAL(valueChanged(int)),
//! [1] //! [2]
            verticalSliders_, SLOT(setValue(int)));

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

//    minimumLabel = new QLabel(tr("Minimum value:"));
//    maximumLabel = new QLabel(tr("Maximum value:"));
//    valueLabel = new QLabel(tr("Current value:"));

//    invertedAppearance = new QCheckBox(tr("Inverted appearance"));
//    invertedKeyBindings = new QCheckBox(tr("Inverted key bindings"));

//! [4] //! [5]
//    minimumSpinBox = new QSpinBox;
////! [5] //! [6]
//    minimumSpinBox->setRange(-100, 100);
//    minimumSpinBox->setSingleStep(1);

//    maximumSpinBox = new QSpinBox;
//    maximumSpinBox->setRange(-100, 100);
//    maximumSpinBox->setSingleStep(1);

//    valueSpinBox = new QSpinBox;
//    valueSpinBox->setRange(-100, 100);
//    valueSpinBox->setSingleStep(1);

//    orientationCombo = new QComboBox;
//    orientationCombo->addItem(tr("Horizontal slider-like widgets"));
//    orientationCombo->addItem(tr("Vertical slider-like widgets"));
    generalStatBt_ = new QPushButton("Общая статистика", this);
    dailyStatBt_ = new QPushButton("Дневная статистика", this);
    nameStatBt_ = new QPushButton("Имена", this);
//    connect(orientationCombo, SIGNAL(activated(int)),
//            stackedWidget, SLOT(setCurrentIndex(int)));
//    connect(minimumSpinBox, SIGNAL(valueChanged(int)),
//            horizontalSliders, SLOT(setMinimum(int)));
//    connect(minimumSpinBox, SIGNAL(valueChanged(int)),
//            verticalSliders, SLOT(setMinimum(int)));
//    connect(maximumSpinBox, SIGNAL(valueChanged(int)),
//            horizontalSliders, SLOT(setMaximum(int)));
//    connect(maximumSpinBox, SIGNAL(valueChanged(int)),
//            verticalSliders, SLOT(setMaximum(int)));
//    connect(invertedAppearance, SIGNAL(toggled(bool)),
//            horizontalSliders, SLOT(invertAppearance(bool)));
//    connect(invertedAppearance, SIGNAL(toggled(bool)),
//            verticalSliders, SLOT(invertAppearance(bool)));
//    connect(invertedKeyBindings, SIGNAL(toggled(bool)),
//            horizontalSliders, SLOT(invertKeyBindings(bool)));
//    connect(invertedKeyBindings, SIGNAL(toggled(bool)),
//            verticalSliders, SLOT(invertKeyBindings(bool)));

    QGridLayout *controlsLayout = new QGridLayout;
    controlsLayout->addWidget(generalStatBt_, 0, 0);
    controlsLayout->addWidget(dailyStatBt_, 1, 0);
    controlsLayout->addWidget(nameStatBt_, 2, 0);
//    controlsLayout->addWidget(minimumSpinBox, 0, 1);
//    controlsLayout->addWidget(maximumSpinBox, 1, 1);
//    controlsLayout->addWidget(valueSpinBox, 2, 1);
//    controlsLayout->addWidget(invertedAppearance, 0, 2);
//    controlsLayout->addWidget(invertedKeyBindings, 1, 2);
//    controlsLayout->addWidget(orientationCombo, 3, 0, 1, 3);
    controlsGroup_->setLayout(controlsLayout);
}
//! [8]
