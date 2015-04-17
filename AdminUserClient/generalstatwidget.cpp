#include <QtWidgets>
#include <QDebug>
#include "generalstatwidget.h"

GeneralStatWidget::GeneralStatWidget(Qt::Orientation orientation, const QString &title,
                                     QWidget *parent)
              : QGroupBox(title, parent),
                table_(new QTableWidget(4, 2, this))
{
    createControls();
    placementResultsArea();
    finalPlacementAreas(orientation);

    QObject::connect(okBt_, &QPushButton::clicked, [&](){
        // по кнопке ок пока заполняем тестовыми данными
        int row = 0;
        int col = 0;
        table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

        //Set Header Label Texts Here
        table_->setHorizontalHeaderLabels(QString("персонаж;упоминаний;").split(";"));

        fillTableTmpData();
    });


}

void GeneralStatWidget::createControls()
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
}

void GeneralStatWidget::placementResultsArea()
{
    rightGroup_ = new QGroupBox("Результаты", this);

    QVBoxLayout *rightLay = new QVBoxLayout(this);
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

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
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

//GeneralStatWidget::~GeneralStatWidget()
//{

//}

