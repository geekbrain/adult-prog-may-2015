#include <QtWidgets>
#include "generalstatwidget.h"

GeneralStatWidget::GeneralStatWidget(Qt::Orientation orientation, const QString &title,
                                     QWidget *parent)
              : QGroupBox(title, parent)
{
//    sitesGroup_ = new QGroupBox("Выбор сайта");
//    okBt_ = new QPushButton("Ok");
//    sitesCombo_ = new QComboBox();
//    sitesCombo_->addItem("Lenta.ru");

//    QGridLayout *sitesLayout = new QGridLayout;
//    sitesLayout->addWidget(sitesCombo_, 0, 0);
//    sitesLayout->addWidget(okBt_, 1, 0);
//    sitesGroup_->setLayout(sitesLayout);
    okBt_ = new QPushButton("ok");
    leftGroup_ = new QGroupBox("left");
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftGroup_->setLayout(leftLayout);

    rightGroup_ = new QGroupBox("right");
    QBoxLayout::Direction direction;
//! [3] //! [4]

    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
    slidersLayout->addWidget(leftGroup_, 1, 0);
    slidersLayout->addWidget(rightGroup_, 3, 0);
    setLayout(slidersLayout);
}

//GeneralStatWidget::~GeneralStatWidget()
//{

//}

