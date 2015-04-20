#include <QtWidgets>
#include <QDebug>
#include "generalstatwidget.h"

GeneralStatWidget::GeneralStatWidget(NameDao* names, Qt::Orientation orientation, const QString &title,
                                     QWidget *parent)
              : QGroupBox(title, parent),
                table_(new QTableWidget(names->namesList().size(), TableCols_, this))
{
    createControlsArea();
    placementResultsArea();
    finalPlacementAreas(orientation);
    setOkBtBehavior();
}

void GeneralStatWidget::createControlsArea()
{
    okBt_ = new QPushButton("ok", this);
    sitesCombo_ = new QComboBox(this);
    sitesCombo_->addItem("lenta.ru");
    leftGroup_ = new QGroupBox("Выбор сайта", this);
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom, this);
    leftLayout->addWidget(sitesCombo_);
    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftLayout->addStretch();
    leftGroup_->setLayout(leftLayout);
}

void GeneralStatWidget::placementResultsArea()
{
    rightGroup_ = new QGroupBox("Результаты", this);

    QVBoxLayout *rightLay = new QVBoxLayout(this);
    configTableView();
    rightLay->addWidget(table_);
    rightGroup_->setLayout(rightLay);
}

void GeneralStatWidget::finalPlacementAreas(Qt::Orientation orientation)
{
    QBoxLayout::Direction direction;

    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction, this);
    slidersLayout->addWidget(leftGroup_, 1, 0);
    slidersLayout->addWidget(rightGroup_, 3, 0);
    setLayout(slidersLayout);
}

void GeneralStatWidget::fillTableTmpData()
{
    table_->setItem(0, 0, new QTableWidgetItem(""));
    table_->item(0, 0)->setText("Навальный");

    table_->setItem(1, 0, new QTableWidgetItem(""));
    table_->item(1, 0)->setText("Медведев");

    table_->setItem(0, 1, new QTableWidgetItem(""));
    table_->item(0, 1)->setText("100");

    table_->setItem(1, 1, new QTableWidgetItem(""));
    table_->item(1, 1)->setText("390");
}

void GeneralStatWidget::configTableView()
{
    table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

    //Set Header Label Texts Here
    table_->setHorizontalHeaderLabels(QString("персонаж;упоминаний;").split(";"));
}

void GeneralStatWidget::setOkBtBehavior() const
{
    QObject::connect(okBt_, &QPushButton::clicked, [&](){
        fillTableTmpData();
    });
}

//GeneralStatWidget::~GeneralStatWidget()
//{

//}

