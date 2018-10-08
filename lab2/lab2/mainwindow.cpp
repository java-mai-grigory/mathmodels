#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

}

 void MainWindow::setGraph(std::vector<Point*>* points, std::vector<Point*>* analytic)
 {
     ui->widget->chart()->setTitle("График");
     QLineSeries* line = new QLineSeries();
     QLineSeries* linea = new QLineSeries();
     line->setName("Метод эйлера");
     linea->setName("Аналитический");
     for (Point* p: *points)
     {

         line->append(p->X, p->Y);

     }
     for (Point* p: *analytic)
     {

         linea->append(p->X, p->Y);

     }
     ui->widget->chart()->addSeries(line );
     ui->widget->chart()->addSeries(linea );


     QValueAxis *axisX = new QValueAxis();
     axisX->setTickCount(10);
     axisX->setTitleText("");
     ui->widget->chart()->addAxis(axisX, Qt::AlignBottom);
     line->attachAxis(axisX);

     QValueAxis *axisY = new QValueAxis;
     axisY->setLabelFormat("%i");
     axisY->setTitleText("");
     ui->widget->chart()->addAxis(axisY, Qt::AlignLeft);
     line->attachAxis(axisY);
 }

MainWindow::~MainWindow()
{
    delete ui;
}
