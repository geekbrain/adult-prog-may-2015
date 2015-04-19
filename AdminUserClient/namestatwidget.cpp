#include <QtWidgets>
#include <QCalendarWidget>
#include "namestatwidget.h"

NameStatWidget::NameStatWidget(NameDao* names, Qt::Orientation orientation, const QString &title,
                               QWidget *parent)
        : QGroupBox(title, parent),
          names_(names),
          leftGroup_(new QGroupBox("Параметры", this)),
          rightGroup_(new QGroupBox("Результаты", this)),
          sitesCombo_(new QComboBox(this)),
          namesCombo_(new QComboBox(this)),
          beginPeriod_(new QDateEdit(this)),
          endPeriod_(new QDateEdit(this)),
          okBt_(new QPushButton("Ok", this)),
          pageCountEdit_(new QLineEdit("1", this)),
          table_(new QTableWidget()),
          rowsCount_(0)
{
    configLeftArea(*names);
    congigRightArea();
//    table_ = new QTableWidget(4, 2);
    QVBoxLayout *rightLay = new QVBoxLayout(this);
//    rightLay->addWidget(table_);
    rightGroup_->setLayout(rightLay);
    setFinalFace(orientation);
}

void NameStatWidget::configLeftArea(const NameDao& names)
{
    sitesCombo_->addItem("lenta.ru");
    auto namesList = names.names();
    // Заполняю выпадающий список именами.
    foreach (auto var, namesList) {
        namesCombo_->addItem(var);
    }

    QBoxLayout* leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo_);
    leftLayout->addWidget(namesCombo_);

    QIntValidator* pagesValidator = new QIntValidator(MinPagesCount, MaxPagesCount, this);
    pageCountEdit_->setValidator(pagesValidator); // Разрешим вводить в поле только цифры.
    QLabel* pages = new QLabel(tr("Страниц (шт):"));
    pages->setBuddy(pageCountEdit_);
    leftLayout->addWidget(pages);
    leftLayout->addWidget(pageCountEdit_);

    QLabel* labelFrom = new QLabel(tr("&От:"));
    labelFrom->setBuddy(beginPeriod_);
    leftLayout->addWidget(labelFrom);
    leftLayout->addWidget(beginPeriod_);

    QLabel* labelTo = new QLabel(tr("&До:"));
    labelTo->setBuddy(endPeriod_);
    leftLayout->addWidget(labelTo);
    leftLayout->addWidget(endPeriod_);

    leftLayout->addWidget(okBt_, 2, Qt::AlignRight);
    leftLayout->addStretch();
    leftGroup_->setLayout(leftLayout);

    connect(okBt_, SIGNAL(clicked()), this, SLOT(showResults()));
}

void NameStatWidget::congigRightArea() const
{

}

void NameStatWidget::setFinalFace(Qt::Orientation orientation)
{
    QBoxLayout::Direction direction;
    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout* slidersLayout = new QBoxLayout(direction, this);
    slidersLayout->addWidget(leftGroup_, 1, 0);
    slidersLayout->addWidget(rightGroup_, 3, 0);
    setLayout(slidersLayout);
}

void NameStatWidget::fillTableTmpData() const
{

}

void NameStatWidget::showResults()
{

}


