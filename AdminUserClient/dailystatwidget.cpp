#include <QtWidgets>
#include "dailystatwidget.h"

DailyStatWidget::DailyStatWidget(Qt::Orientation orientation, const QString &title,
                                 QWidget *parent)
          : QGroupBox(title, parent),
            sitesGroup_(new QGroupBox("Выбор сайта", this)),
            sitesCombo_(new QComboBox(this)),
            okBt_(new QPushButton("ok", this))
{        
    sitesCombo_->addItem("lenta.ru");

    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo_);
    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftLayout->addStretch();
    sitesGroup_->setLayout(leftLayout);

    rightGroup_ = new QGroupBox("Результаты");
    table_ = new QTableWidget(4, 2);
    QVBoxLayout *rightLay = new QVBoxLayout;
    rightLay->addWidget(table_);
    rightGroup_->setLayout(rightLay);
    QBoxLayout::Direction direction;

    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
    slidersLayout->addWidget(sitesGroup_, 1, 0);
    slidersLayout->addWidget(rightGroup_, 3, 0);
    setLayout(slidersLayout);

    QObject::connect(okBt_, &QPushButton::clicked, [&](){
        table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

        //Set Header Label Texts Here
        table_->setHorizontalHeaderLabels(QString("персонаж;упоминаний;").split(";"));

        fillTableTempData();
    });
}

void DailyStatWidget::fillTableTempData() const
{
    int row = 0;
    int col = 0;

    table_->setItem(row, col, new QTableWidgetItem(""));
    table_->item(row, col)->setText("Навальный");

    table_->setItem(++row, col, new QTableWidgetItem(""));
    table_->item(row, col)->setText("Медведев");

    table_->setItem(--row, ++col, new QTableWidgetItem(""));
    table_->item(row, col)->setText("1");

    table_->setItem(++row, col, new QTableWidgetItem(""));
    table_->item(row, col)->setText("3");
}


