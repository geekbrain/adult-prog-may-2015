#include <QtWidgets>
#include <QDebug>
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
    sitesCombo_ = new QComboBox();
    sitesCombo_->addItem("lenta.ru");
    leftGroup_ = new QGroupBox("left");
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo_);
    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftLayout->addStretch();
    leftGroup_->setLayout(leftLayout);

    rightGroup_ = new QGroupBox("right");
    table_ = new QTableWidget(4, 2);
    QVBoxLayout *rightLay = new QVBoxLayout;
    rightLay->addWidget(table_);
    rightGroup_->setLayout(rightLay);
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

    QObject::connect(okBt_, &QPushButton::clicked, [&](){
        //        this->m_objects.remove(sender);
        //        QTableWidgetItem medvedev("Медведев");
        //        table_->setItem(0, 0, &medvedev);
        qDebug() << "Сработала лямбда";
        int row = 0;
        int col = 0;
        table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

        //Set Header Label Texts Here
        table_->setHorizontalHeaderLabels(QString("HEADER 1;HEADER 2;HEADER 3").split(";"));

        //Add Table items here With Default Cell Texts
        table_->setItem(row, col, new QTableWidgetItem("ITEM 1"));

        //Change Cell Texts Here
        table_->item(row, col)->setText("Putin");

        table_->item(row, col)->setText("Naval'ny");
    });


}

//GeneralStatWidget::~GeneralStatWidget()
//{

//}

