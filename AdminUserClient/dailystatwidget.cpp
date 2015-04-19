#include <QtWidgets>
#include "dailystatwidget.h"

DailyStatWidget::DailyStatWidget(NameDao* names, Qt::Orientation orientation, const QString &title,
                                 QWidget *parent)
          : QGroupBox(title, parent),
            sitesGroup_(new QGroupBox("Выбор сайта", this)),
            sitesCombo_(new QComboBox(this)),
            okBt_(new QPushButton("ok", this)),
            table_(new QTableWidget(names->namesList().size(), TableCols_, this)),
            resultGroup_(new QGroupBox("Результаты", this))
{
    configControlArea();
    configResultsArea();
    resultTableTuning();
    setFinalFace(orientation);
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

void DailyStatWidget::configControlArea() const
{
    sitesCombo_->addItem("lenta.ru");
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo_);
    int okBtStretch = 2;
    leftLayout->addWidget(okBt_, okBtStretch, Qt::AlignRight);
    leftLayout->addStretch();
    sitesGroup_->setLayout(leftLayout);

    connect(okBt_, SIGNAL(clicked()), this, SLOT(fillTableTempData()));
}

void DailyStatWidget::configResultsArea() const
{
    QVBoxLayout *rightLay = new QVBoxLayout;
    rightLay->addWidget(table_);
    resultGroup_->setLayout(rightLay);
}

void DailyStatWidget::resultTableTuning() const
{
    table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

    //Set Header Label Texts Here
    table_->setHorizontalHeaderLabels(QString("персонаж;упоминаний;").split(";"));
}

void DailyStatWidget::setFinalFace(Qt::Orientation orientation)
{
    QBoxLayout::Direction direction;

    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
    int stretch = 1; // Фактор растяжения для левой группы.
    slidersLayout->addWidget(sitesGroup_, stretch);
    stretch = 3; // Фактор рястяжения для провой группы виджетов.
    slidersLayout->addWidget(resultGroup_, stretch);
    setLayout(slidersLayout);
}


