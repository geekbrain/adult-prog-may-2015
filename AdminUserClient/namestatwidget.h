#ifndef NAMESTATWIDGET_H
#define NAMESTATWIDGET_H

#include <QGroupBox>
#include <memory>
#include "namedao.h"

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
    explicit NameStatWidget(NameDao* names, Qt::Orientation orientation, const QString &title,
                             QWidget *parent = 0);

signals:

public slots:

private:
    NameDao* names_; // Список лиц, по которым подсчитываем статистику.
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
    const size_t ColCount = 3; // Число исходя из документации п. 3121 «Номер по порядку», «Адрес
                               // страницы», «Количество упоминаний».

    const int MinPagesCount = 0; // Наименьшая глубина в страницах для сбора статистики.
    const int MaxPagesCount = 9; // Наибольшая глубина в страницах для сбора статистики.

    void configLeftArea(const NameDao& names);
    void congigRightArea();
    void setFinalFace(Qt::Orientation orientation);

private slots:
    void fillTableTmpData();
    void showResults();
};

#endif // NAMESTATWIDGET_H
