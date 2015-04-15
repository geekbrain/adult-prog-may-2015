#include <QtWidgets>
#include "generalstatwidget.h"

GeneralStatWidget::GeneralStatWidget(QWidget *parent) : QWidget(parent)
{
    sitesGroup_ = new QGroupBox("Выбор сайта");
    okBt_ = new QPushButton("Ok");
    sitesCombo_ = new QComboBox;
    sitesCombo_->addItem("Lenta.ru");

    QGridLayout *sitesLayout = new QGridLayout;
    sitesLayout->addWidget(sitesCombo_, 0, 0);
    sitesLayout->addWidget(okBt_, 1, 0);
    sitesGroup_->setLayout(sitesLayout);
}

GeneralStatWidget::~GeneralStatWidget()
{

}

