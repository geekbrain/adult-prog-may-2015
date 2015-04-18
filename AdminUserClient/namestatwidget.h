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

    void configLeftArea(const NameDao& names) const;
    void setFinalFace(Qt::Orientation orientation);
};

#endif // NAMESTATWIDGET_H
