#include "mainwindow.h"
#include <QApplication>

#include <vector>
#include <iostream>
#include <math.h>

using  namespace std;



double deriv(double x, double y)
{
    return -x / y;
}

double f(double x)
{
    return sqrt(4 * 4 - x * x);
}

vector<Point*>* ode_solve()
{
    double xmax = 4;
    int n = 10;
    double delta_x = xmax / n;
    double y_curr, y_prev, x_prev, _deriv, deriv_tilda;
    x_prev = 0;
    y_prev = 4;
    vector<Point*>* points = new vector<Point*>();
    while(x_prev < xmax){
        _deriv = deriv(x_prev, y_prev);
        y_curr = y_prev + _deriv * delta_x;
        deriv_tilda = (_deriv + deriv(x_prev, y_curr)) /  2;
        y_curr = y_prev + deriv_tilda * delta_x;
        points->push_back( new Point(x_prev, y_prev));
        x_prev += delta_x;
        y_prev = y_curr;
    }
    return points;
}

int main(int argc, char *argv[])
{
    vector<Point*>* points = ode_solve();
    vector<Point*>* analytic = new vector<Point*>();
   for(int i = 0; i < points->size(); i++){
        //cout << points->at(i)->X << " " <<  points->at(i)->Y << " "  << f(points->at(i)->X) << "\n";
        analytic->push_back( new Point(points->at(i)->X, f(points->at(i)->X)));
    }


    QApplication a(argc, argv);
    MainWindow w;
    w.setGraph(points, analytic);

    w.show();

    return a.exec();
}
