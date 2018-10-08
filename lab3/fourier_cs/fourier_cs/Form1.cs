using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*y = lambda x:  -0.5 * x * x + 2 * x

y2 = lambda x:  numpy.sin(numpy.pi* x / L)

y3 = lambda x: 2 * y2(x)

#y4 = lambda x:  2.064 * numpy.sin(numpy.pi * x / L) + 0.076 * numpy.sin(3 * numpy.pi * x / L) + 0.0165 * numpy.sin(5 * numpy.pi * x / L) + 0.006 *  numpy.sin(7 * numpy.pi * x / L)
y4 = lambda x:  1.80 * numpy.sin(numpy.pi* x / L) - 0.6 * numpy.sin(3 * numpy.pi* x / L) - 0.36 * numpy.sin(5 * numpy.pi* x / L) +  0.257  * numpy.sin(7 * numpy.pi* x / L) #+ 0.20 *  numpy.sin(9 * numpy.pi * x / L) - 0.16 *  numpy.sin(11 * numpy.pi * x / L)    -0.13 *  numpy.sin(13 * numpy.pi * x / L) +0.12 *  numpy.sin(15 * numpy.pi * x / L) + 0.1 *numpy.sin(17 * numpy.pi * x / L) - 0.09 * numpy.sin(19 * numpy.pi * x / L) - 0.08 *  numpy.sin(21 * numpy.pi * x / L) + 0.07 *  numpy.sin(23 * numpy.pi * x / L) + 0.072 * numpy.sin(25 * numpy.pi * x / L) - 0.06  * numpy.sin(27 * numpy.pi * x / L) - 0.062  * numpy.sin(29 * numpy.pi * x / L) + 0.05 *numpy.sin(31 * numpy.pi * x / L)  + 0.054 *numpy.sin(33 * numpy.pi * x / L) */



namespace fourier_cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Calculate();
        }

        public double f(double x, int N, double L)
        {
            double S = 0;
            double a;
            for (int i = 0; i < N; i++)
            {
                a = 4 * (Math.Cos(Math.PI * (2 * i + 1) / L) - Math.Cos(3 * Math.PI * (2 * i + 1) / L)) / (Math.PI * (2 * i + 1));
                S += a * Math.Sin((2 * i + 1) * Math.PI * x / L);
            }
            return S;
        }

        public double fa2(double x)
        {
            //return -0.5 * x * x + 2 * x;
            return - x * x + 4* x;
        }

        public double f2(double x, double L, int N)
        {
            double S = 0;
            double a = -1, b = 4;
            for (int i = 0; i < N; i++)
            {
                double beta = (2 * i + 1) * Math.PI / L;
                double alpha = 2 * ((a * L * L + b * L) / beta - 4 * a / (beta * beta * beta)) / L;
                S += alpha * Math.Sin(beta * x);
            }
            return S;
            //return 2.064 * Math.Sin(Math.PI * x / L);
        }

        public void Calculate()
        {
            chart1.Series[0].BorderWidth = 2;

            double L = 4.0;
            int J = 1000;
            double dx = L / (J - 1);

            double[] x_grid = new double[J];
            double[] y = new double[J];
            for (int i = 0; i < J; i++)
                x_grid[i] = i * dx;

            for (int i = 0; i < J; i++)
                y[i] = f(x_grid[i], 5000, L);

            for (int i = 0; i < J; i++)
                chart1.Series[0].Points.AddXY(x_grid[i], y[i]);

        }

        public void Calculate2()
        {
            chart1.Series[0].BorderWidth = 2;

            double L = 4.0;
            int J = 1000;
            double dx = L / (J - 1);

            double[] x_grid = new double[J];
            double[] y = new double[J];
            for (int i = 0; i < J; i++)
                x_grid[i] = i * dx;

            for (int i = 0; i < J; i++)
                y[i] = fa2(x_grid[i]);

            for (int i = 0; i < J; i++)
                chart1.Series[0].Points.AddXY(x_grid[i], y[i]);

            for (int i = 0; i < J; i++)
                y[i] = f2(x_grid[i], 4, 1);

            for (int i = 0; i < J; i++)
                chart1.Series[1].Points.AddXY(x_grid[i], y[i]);

        }
    }
}
