QT += widgets
QMAKE_CXXFLAGS += -std=c++0x

HEADERS     = \
              window.h \
    generalstatwidget.h \
    dailystatwidget.h \
    namestatwidget.h \
    namedao.h \
    statisticsextractor.h
SOURCES     = main.cpp \
              window.cpp \
    generalstatwidget.cpp \
    dailystatwidget.cpp \
    namestatwidget.cpp \
    namedao.cpp \
    statisticsextractor.cpp

#DESTDIR = /bin
