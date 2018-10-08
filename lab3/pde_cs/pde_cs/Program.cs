using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace pde_cs
{
    class Point
    {
        public double X, Y;
    }

    class Program
    {
        static double L = 4.0;
        static int J = 500;

        static double T = 6;
        static int N = 2000;

        static double a = 1.0;

        static double f(double x)
        {
            return Math.Sin(x * Math.PI / L);
        }

        static double fa(double x, double t)
        {
            return Math.Sin(x * Math.PI / L) * Math.Exp( -a * a * Math.PI * Math.PI * t / (L * L));
        }

        struct Diag
        {
            public double C0, B0, A, C, B, CN, AN;
        }

        static double[] solve_diag(Diag A_u, Diag B_u, double[] U)
        {
            int N = U.Count();
            double[] X = new double[N];
            double[] F = new double[N];
            double[] CC = new double[N];
            double[] FF = new double[N];

            F[0] = B_u.C0 * U[0] + B_u.B0 * U[1];
            for (int i = 1; i < N - 1; i++)
                F[i] = B_u.A * U[i - 1] + B_u.C * U[i] + B_u.B * U[i + 1];
            F[N - 1] = B_u.AN * U[N - 2] + B_u.CN * U[N - 1];

         //   PrintMatrix(A_u,  F);

            CC[0] = A_u.C0;
            FF[0] = F[0];
            CC[1] = A_u.C - A_u.A * A_u.B0 / CC[0];
            FF[1] = F[1] - A_u.A * FF[0] / CC[0];
            for (int i = 2; i < N - 1; i++)
            {
                CC[i] = A_u.C - A_u.A * A_u.B / CC[i - 1];
                FF[i] = F[i] - A_u.A * FF[i - 1] / CC[i - 1];
            }

            CC[N - 1] = A_u.CN - A_u.AN * A_u.B / CC[N - 2];
            FF[N - 1] = F[N - 1] - A_u.AN * FF[N - 2] / CC[N - 2];

            X[N - 1] = FF[N - 1] / CC[N - 1];
            for (int i = N - 2; i > 0; i--)
            {
                X[i] = (FF[i] - A_u.B * X[i + 1]) / CC[i];
            }
            X[0] = (FF[0] - A_u.B0 * X[1]) / CC[0];

            return X;
        }


        static void scheme3()
        {
            double[] U0;
            double[] U;
            double sigma_u;

            double dx = L / (J - 1);
            double[] x_grid = new double[J];
            for (int i = 0; i < J; i++)
                x_grid[i] = i * dx;

            double dt = T / (N - 1);
            double[] t_grid = new double[N];
            for (int i = 0; i < N; i++)
                t_grid[i] = i * dt;

            U0 = new double[J];
            for (int i = 0; i < J; i++)
                U0[i] = f(x_grid[i]);

            double[] UA = new double[J];
            for (int i = 0; i < J; i++)
                UA[i] = fa(x_grid[i], T);

            double []V = new double[J - 1];

            U = new double[J];
            Array.Copy(U0, 0, U, 0, J - 1);

            sigma_u = (a * dt) / (2.0 * dx * dx);
                        
            Print(U0);

            for (int k = 1; k < N - 1; k++)
            {
                for (int i = 1; i < J - 1; i++)
                    V[i] = U[i] + 2 * sigma_u * (U[i - 1] + U[i + 1] - 2 * U[i]);
                Array.Copy(V, 0, U, 0, J - 1);
            }
            Print(U);
            Print(UA);
        }

        static void scheme1()
        {
            double[] U0;
            double[] U;
            double sigma_u;

            double dx = L / (J - 1);
            double[] x_grid = new double[J];
            for (int i = 0; i < J; i++)
                x_grid[i] = i * dx;

            double dt = T / (N - 1);
            double[] t_grid = new double[N];
            for (int i = 0; i < N; i++)
                t_grid[i] = i * dt;

            U0 = new double[J];
            for (int i = 0; i < J; i++)
                U0[i] = f(x_grid[i]);

            double[] UA = new double[J];
            for (int i = 0; i < J; i++)
                UA[i] = fa(x_grid[i], T);

            U = new double[J - 2];

            Array.Copy(U0, 1, U, 0, J - 2);

            sigma_u = (a * dt) / (2.0 * dx * dx);

            Diag A_u = new Diag();
            A_u.C = A_u.C0 = A_u.CN = 1.0 + 2.0 * sigma_u;
            A_u.B = A_u.B0 = -sigma_u;
            A_u.A = A_u.AN = -sigma_u;

            Diag B_u = new Diag();
            B_u.C = B_u.C0 = B_u.CN = 1.0 - 2.0 * sigma_u;
            B_u.B = B_u.B0 = sigma_u;
            B_u.A = B_u.AN = sigma_u;
            
            for (int i = 1; i < N; i++)
            {
                solve_diag(A_u, B_u, U).CopyTo(U, 0);
            }
            Print(U0);

            graph frm = new graph();
            frm.DrawGraph(x_grid, U0, Color.Green);
            U.CopyTo(U0, 1);
    
            Print(U0);
            Print(UA);

            frm.DrawGraph(x_grid, U0, Color.Blue);
            frm.DrawGraph(x_grid, UA, Color.Red);
            frm.ShowDialog();
        }

        static void scheme2()
        {
            double[] U0;
            double[] U;
            double sigma_u;

            double dx = L / (J - 1);
            double[] x_grid = new double[J];
            for (int i = 0; i < J; i++)
                x_grid[i] = i * dx;

            double dt = T / (N - 1);
            double[] t_grid = new double[N];
            for (int i = 0; i < N; i++)
                t_grid[i] = i * dt;

            U0 = new double[J];
            for (int i = 0; i < J; i++)
                U0[i] = f(x_grid[i]);
            Print(U0);

            U = new double[J];

            Array.Copy(U0, 0, U, 0, J - 1);

            sigma_u = (a * dt) / (2.0 * dx * dx);

            Diag A_u = new Diag();
            A_u.C = 1.0 + 2.0 * sigma_u;
            A_u.C0 = A_u.CN = 1 + sigma_u;
            A_u.B =  A_u.B0 = A_u.A = A_u.AN = -sigma_u;

            Diag B_u = new Diag();
            B_u.C0 = B_u.CN = 1.0 - sigma_u;
            B_u.C = 1.0 - 2.0 * sigma_u;
            B_u.B = B_u.B0 = B_u.A = B_u.AN = sigma_u;

            for (int i = 1; i < N; i++)
            {
                solve_diag(A_u, B_u, U).CopyTo(U, 0);
            }
            // Print(U0);
            graph frm = new graph();
            frm.DrawGraph(x_grid, U0, Color.Green);
            U.CopyTo(U0, 0);
            frm.DrawGraph(x_grid, U0, Color.Blue);
            Print(U0);
            
            
            frm.ShowDialog();
        }


        static void Print(double[] A)
        {
            foreach (double a in A)
                Console.Write("{0,7:F3}",  a);
            Console.WriteLine();
        }

        static void PrintMatrix(Diag DM, double[] F)
        {
            int N = F.Count();
            double A, B, C; 
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                    { B = DM.B0; C = DM.C0; }
                else if (i == N - 1)
                    { B = DM.B0; C = DM.C0; }
                else
                    { B = DM.B; C = DM.C; A = DM.A; }
            

                for (int j = 0; j < N; j++)
                {
                    if (i > 0 && j == i - 1)
                        Console.Write("{0,7:F3}", DM.A);
                    else if (j == i)
                        Console.Write("{0,7:F3}", DM.C);
                    else if (i < N - 1 && j == i + 1)
                        Console.Write("{0,7:F3}", DM.B);
                   else
                        Console.Write("{0,7:F3}", 0);
                }
                Console.Write("{0,7:F3}", F[i]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            scheme2();

            Console.ReadKey();
        }
    }
}
