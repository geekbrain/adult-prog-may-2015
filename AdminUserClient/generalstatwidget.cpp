#include <QtWidgets>
#include <QDebug>
#include <QUrl>
#include "generalstatwidget.h"
#include "statistics.h"

GeneralStatWidget::GeneralStatWidget(const StatisticsExtractor& statsExtractor, Qt::Orientation orientation, const QString &title,
                                     QWidget *parent)
              : QGroupBox(title, parent),
                table_(new QTableWidget(this))
{
    createControlsArea(statsExtractor);
    placementResultsArea();
    finalPlacementAreas(orientation);
    setOkBtBehavior(statsExtractor);
}

void GeneralStatWidget::createControlsArea(const StatisticsExtractor& statsExtractor)
{
    okBt_ = new QPushButton("ok", this);
    sitesCombo_ = new QComboBox(this);
    sitesCombo_->addItem("lenta.ru");

    leftGroup_ = new QGroupBox("Выбор сайта", this);    
    QBoxLayout *leftLayout = new QBoxLayout(QBoxLayout::TopToBottom, this);
    leftLayout->addWidget(sitesCombo_);
    int stretchForOkBt = 2; // Фактор растягивания для кнопки ок.
    leftLayout->addWidget(okBt_, stretchForOkBt, Qt::AlignRight);
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
    int stretch = 1;
    slidersLayout->addWidget(leftGroup_, stretch);
    stretch = 3;
    slidersLayout->addWidget(rightGroup_, stretch);
    setLayout(slidersLayout);
}

void GeneralStatWidget::fillTableTmpData()
{
    table_->setItem(0, 0, new QTableWidgetItem("")); // Столбец 0, строка 0.
    table_->item(0, 0)->setText("Навальный");

    table_->setItem(1, 0, new QTableWidgetItem("")); // Столбец 0, строка 1.
    table_->item(1, 0)->setText("Медведев");

    table_->setItem(0, 1, new QTableWidgetItem("")); // Столбец 1, строка 0.
    table_->item(0, 1)->setText("100");

    table_->setItem(1, 1, new QTableWidgetItem("")); // Столбец 1, строка 1.
    table_->item(1, 1)->setText("390");
}

void GeneralStatWidget::configTableView()
{
    table_->setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);

    //Set Header Label Texts Here
    QStringList tableHeader;
    tableHeader<<"имя"<<"упоминаний";
    table_->setHorizontalHeaderLabels(tableHeader);
    table_->verticalHeader()->setVisible(true);
    table_->setEditTriggers(QAbstractItemView::NoEditTriggers); // Редактировать нельзя будет.
    table_->setSelectionBehavior(QAbstractItemView::SelectRows);
    table_->setSelectionMode(QAbstractItemView::SingleSelection);
    table_->setShowGrid(true);
    table_->setStyleSheet("QTableView {selection-background-color: red;}");
    table_->setGeometry(QApplication::desktop()->screenGeometry());
}

void GeneralStatWidget::setOkBtBehavior(const StatisticsExtractor& statsExtractor)
{
    QObject::connect(okBt_, &QPushButton::clicked, [&](){
        QUrl url = QUrl::fromUserInput(sitesCombo_->currentText());
        QScopedPointer<GeneralStatistics> genStats(new GeneralStatistics(url));
        statsExtractor.getGeneralStatistics(genStats);
        inputStatsToTable(genStats);
    });
}

void GeneralStatWidget::inputStatsToTable(const QScopedPointer<GeneralStatistics> &stat)
{
    size_t tupleCount = stat->getTupleCount();
    table_->setRowCount(tupleCount);
    table_->setColumnCount(stat->getFieldCount());
    QMap<QString,quint32> rowStats = stat->getNamesMentions();
    int row = 0;
    foreach (QString name, rowStats.keys()) {
        int col = 0;
        table_->setItem(row, col, new QTableWidgetItem(name));
        col++;
        table_->setItem(row, col, new QTableWidgetItem(""));
        table_->item(row, col)->setData(Qt::DisplayRole, rowStats.value(name));
        row++; // Переходим на следующую строку.
    }
}

//GeneralStatWidget::~GeneralStatWidget()
//{

//}

