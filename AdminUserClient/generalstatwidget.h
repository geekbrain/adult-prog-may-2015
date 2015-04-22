#ifndef GENERALSTATWIDGET_H
#define GENERALSTATWIDGET_H

#include <QGroupBox>
#include "namedao.h"

QT_BEGIN_NAMESPACE
class QComboBox;
class QPushButton;
class QGroupBox;
class QTableWidget;
QT_END_NAMESPACE

class GeneralStatWidget : public QGroupBox
{
    Q_OBJECT
public:
    explicit GeneralStatWidget(NameDao* names, Qt::Orientation orientation, const QString &title,
                               QWidget *parent = 0);
//    ~GeneralStatWidget();
signals:

public slots:

private:
    /**
     * @brief createControlsArea Создание и размещение элементов управления показом статистики.
     */
    void createControlsArea();

    /**
     * @brief placementResultsArea Размещение группы элементов для отображения общей статистики.
     */
    void placementResultsArea();

    /**
     * @brief placementAreas Соединение 2 областей - 1) с контрольными элементами и 2) таблицей
     * в результате данный виджет получит окончательный вид.
     * @param orientation Горизонтально либо вертикально.
     */
    void finalPlacementAreas(Qt::Orientation orientation = Qt::Horizontal);

    void fillTableTmpData();
    void configTableView();
    void setOkBtBehavior();

    QGroupBox *sitesGroup_;
    QComboBox *sitesCombo_;
    QPushButton *okBt_;
    const int TableCols_ = 2; // Столбцов у таблицы: "имя" и "число упоминаний".
    QTableWidget *table_;
    QGroupBox *leftGroup_;
    QGroupBox *rightGroup_;
};

#endif // GENERALSTATWIDGET_H
