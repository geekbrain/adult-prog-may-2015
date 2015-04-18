#include <QtWidgets>
#include <QCalendarWidget>
#include "namestatwidget.h"

NameStatWidget::NameStatWidget(QList<QString> names, Qt::Orientation orientation, const QString &title,
                               QWidget *parent)
        : QGroupBox(title, parent),
          leftGroup(new QGroupBox("Параметры", this)),
          rightGroup(new QGroupBox("Результаты", this)),
          sitesCombo(new QComboBox(this)),
          namesCombo(new QComboBox(this)),
          beginPeriod(new QDateEdit(this)),
          endPeriod(new QDateEdit(this)),
          okBt(new QPushButton("Ok", this))
{
    configLeftArea(names);
//    table_ = new QTableWidget(4, 2);
    QVBoxLayout *rightLay = new QVBoxLayout;
//    rightLay->addWidget(table_);
    rightGroup->setLayout(rightLay);


    QBoxLayout::Direction direction;
    if (orientation == Qt::Horizontal)
        direction = QBoxLayout::TopToBottom;
    else
        direction = QBoxLayout::LeftToRight;

    QBoxLayout *slidersLayout = new QBoxLayout(direction);
    slidersLayout->addWidget(leftGroup, 1, 0);
    slidersLayout->addWidget(rightGroup, 3, 0);
    setLayout(slidersLayout);
}

void NameStatWidget::configLeftArea(QList<QString> names) const
{
    sitesCombo->addItem("lenta.ru");

    // Заполняю выпадающий список именами.
    foreach (auto var, names) {
        namesCombo->addItem(var);
    }

    QBoxLayout* leftLayout = new QBoxLayout(QBoxLayout::TopToBottom);
    leftLayout->addWidget(sitesCombo);
    leftLayout->addWidget(namesCombo);    
    QLabel* labelFrom = new QLabel(tr("&От:"));
    labelFrom->setBuddy(beginPeriod);
    leftLayout->addWidget(labelFrom);
    leftLayout->addWidget(beginPeriod);

    QLabel* labelTo = new QLabel(tr("&До:"));
    labelTo->setBuddy(endPeriod);
    leftLayout->addWidget(labelTo);
    leftLayout->addWidget(endPeriod);

    leftLayout->addWidget(okBt, 2, Qt::AlignRight);
    leftLayout->addStretch();
    leftGroup->setLayout(leftLayout);
}


