#include <QApplication>

#include "window.h"
#include "statisticsextractor.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    StatisticsExtractor *client = new StatisticsExtractor(0);
//    Window window(*client);
//    window.show();
    return app.exec();
}
