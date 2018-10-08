#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QtCharts/QtCharts>
#include <vector>
QT_CHARTS_USE_NAMESPACE

struct Point
{
    double X, Y;

    Point(double X, double Y) {
        this->X = X;
        this->Y = Y;
    }
};

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    void setGraph(std::vector<Point*>* points, std::vector<Point*>* analytic);

private:
    Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H
