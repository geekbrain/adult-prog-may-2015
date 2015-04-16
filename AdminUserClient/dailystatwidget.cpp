#include <QtWidgets>
#include "dailystatwidget.h"

DailyStatWidget::DailyStatWidget(Qt::Orientation orientation, const QString &title,
                                 QWidget *parent)
          : QGroupBox(title, parent)
{
    okBt_ = new QPushButton("ok");
    sitesCombo_ = new QComboBox();
    sitesCombo_->addItem("lenta.ru");
    leftGroup_ = new QGroupBox("Выбор сайта");
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo_);
    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftLayout->addStretch();
    leftGroup_->setLayout(leftLayout);

    rightGroup_ = new QGroupBox("Результаты");
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
        // по кнопке ок пока заполняем тестовыми данными
        int row = 0;
        int col = 0;
        table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

        //Set Header Label Texts Here
        table_->setHorizontalHeaderLabels(QString("персонаж;упоминаний;").split(";"));

        //
        // Создаю 4 ячейки, заполняю данными наугад.
        //
        table_->setItem(row, col, new QTableWidgetItem(""));
        table_->item(row, col)->setText("Навальный");

        table_->setItem(++row, col, new QTableWidgetItem(""));
        table_->item(row, col)->setText("Медведев");

        table_->setItem(--row, ++col, new QTableWidgetItem(""));
        table_->item(row, col)->setText("1");

        table_->setItem(++row, col, new QTableWidgetItem(""));
        table_->item(row, col)->setText("3");
    });


}


