#ifndef NAMESTATWIDGET_H
#define NAMESTATWIDGET_H

#include <QGroupBox>
#include <memory>
#include "statisticsextractor.h"

QT_BEGIN_NAMESPACE
class QComboBox;
class QPushButton;
class QGroupBox;
class QTableWidget;
class QDateEdit;
class QLineEdit;
QT_END_NAMESPACE

class NameStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit NameStatWidget(const StatisticsExtractor& statsExtractor, Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:

private:
    QGroupBox *leftGroup_;
    QGroupBox *rightGroup_;
    QComboBox *sitesCombo_;
    QComboBox *namesCombo_;
    QDateEdit *beginPeriod_;
    QDateEdit *endPeriod_;
    QPushButton *okBt_;
    QLineEdit* pageCountEdit_;
    QTableWidget *table_;
    size_t rowsCount_;
    const size_t ColCount = 2; //«Адрес страницы», «Количество упоминаний», номер в таблице и так
                                // отобразится

    const int MinPagesCount = 0; // Наименьшая глубина в страницах для сбора статистики.
    const int MaxPagesCount = 9; // Наибольшая глубина в страницах для сбора статистики.

    void configLeftArea(const StatisticsExtractor& statsExtractor);
    void congigRightArea();
    void setFinalFace(Qt::Orientation orientation);
    void fillNamesCombo(const StatisticsExtractor& statsExtractor);

private slots:
    void fillTableTmpData();
    void showResults();
};

#endif // NAMESTATWIDGET_H
