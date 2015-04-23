QT += widgets network core
QMAKE_CXXFLAGS += -std=c++0x
LIBS += -LC:/Projects/kdsoap/lib -lkdsoapd1
INCLUDEPATH += C:/Projects/kdsoap/include
HEADERS     = \
              window.h \
    generalstatwidget.h \
    dailystatwidget.h \
    namestatwidget.h \
    namedao.h \
    statisticsextractor.h \
    statistics.h \
    wssoap.h
SOURCES     = main.cpp \
              window.cpp \
    generalstatwidget.cpp \
    dailystatwidget.cpp \
    namestatwidget.cpp \
    namedao.cpp \
    statisticsextractor.cpp \
    statistics.cpp \
    wssoap.cpp

#DESTDIR = /bin
