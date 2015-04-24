#include <QApplication>
#include <QDebug>
#include "window.h"
#include "gSoap/BasicHttpBinding_USCOREIService.nsmap"
#include "gSoap/soapBasicHttpBinding_USCOREIServiceProxy.h"
#include <string>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    BasicHttpBinding_USCOREIServiceProxy basic("http://adultprog2015-001-site1.myasp.net/WsSoap.svc");

    // Установим имя
    _tempuri__SetName tempuri__SetName;
    std::string name = "Medved";
    tempuri__SetName.name = "Medved";
    qDebug() << tempuri__SetName.name;
    _tempuri__SetNameResponse tempuri__SetNameResponse;
    if (basic.SetName(&tempuri__SetName, tempuri__SetNameResponse) == SOAP_OK)
        qDebug() << "Успех установки имени";
    else
        qDebug() << "Увы, имя не прошло";

    _tempuri__GetNames tempuri__GetNames;
    _tempuri__GetNamesResponse tempuri__GetNamesResponse;

    if (basic.GetNames(&tempuri__GetNames, tempuri__GetNamesResponse) == SOAP_OK)
        qDebug() << "Успех";
    else
        qDebug() << "Увы";

//    Window window(*client);
//    window.show();

    return app.exec();
}
