#include <QtWidgets>

#include "area2.h"

//! [0]
Area2::Area2(Qt::Orientation orientation, const QString &title,
                           QWidget *parent)
    : QGroupBox(title, parent)
{
    slider = new QSlider(orientation);
    slider->setFocusPolicy(Qt::StrongFocus);
    slider->setTickPosition(QSlider::TicksBothSides);
    slider->setTickInterval(10);
    slider->setSingleStep(1);

    scrollBar = new QScrollBar(orientation);
    scrollBar->setFocusPolicy(Qt::StrongFocus);

    dial = new QDial;
    dial->setFocusPolicy(Qt::StrongFocus);

    connect(slider, SIGNAL(valueChanged(int)), scrollBar, SLOT(setValue(int)));
    connect(scrollBar, SIGNAL(valueChanged(int)), dial, SLOT(setValue(int)));
    connect(dial, SIGNAL(valueChanged(int)), slider, SLOT(setValue(int)));
//! [0] //! [1]
    connect(dial, SIGNAL(valueChanged(int)), this, SIGNAL(valueChanged(int)));
//! [1] //! [2]

//! [2] //! [3]
    QBoxLayout::Direction direction;
//! [3] //! [4]

    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
    slidersLayout->addWidget(slider);
    slidersLayout->addWidget(scrollBar);
    slidersLayout->addWidget(dial);
    setLayout(slidersLayout);
}
//! [4]

//! [5]
void Area2::setValue(int value)
//! [5] //! [6]
{
    slider->setValue(value);
}
//! [6]

//! [7]
void Area2::setMinimum(int value)
//! [7] //! [8]
{
    slider->setMinimum(value);
    scrollBar->setMinimum(value);
    dial->setMinimum(value);
}
//! [8]

//! [9]
void Area2::setMaximum(int value)
//! [9] //! [10]
{
    slider->setMaximum(value);
    scrollBar->setMaximum(value);
    dial->setMaximum(value);
}
//! [10]

//! [11]
void Area2::invertAppearance(bool invert)
//! [11] //! [12]
{
    slider->setInvertedAppearance(invert);
    scrollBar->setInvertedAppearance(invert);
    dial->setInvertedAppearance(invert);
}
//! [12]

//! [13]
void Area2::invertKeyBindings(bool invert)
//! [13] //! [14]
{
    slider->setInvertedControls(invert);
    scrollBar->setInvertedControls(invert);
    dial->setInvertedControls(invert);
}
//! [14]
