#include <stdio.h>
#include <math.h>
#include <omp.h>

double f(double x)
{
    return sqrt(1 - x * x);
}

double elip(double x)
{
    double k = sin(M_PI / 4);
    return 1 / sqrt(1 - sin(x) * sin(x) * k * k);
}

double test_seq()
{
    double S, P, k, N, prev_S, eps;
    eps = 1e-9;
    N = 1000;
    P = 1.0;
    S = 0;
    k = sin(M_PI / 4);
    int i = 1;
    do
    {
        prev_S = S;
        P *= 1.0 * (2* i - 1) * k / (2 * i);
        S += P * P;
        i++;
    }
    while( fabs(S - prev_S) > eps );
    return S; // * M_PI / 2;
}


double integral2(double a, double b, double (*f)(double), int N )
{
    double dx, S2, S4, SA, SB;

    dx = (b - a) / N;

    S2 = S4 = 0;
    SA = f(a);
    SB = f(b);
    for(int i = 0; i < N; i++)
    {
       S4 += f(0.5 * dx + i * dx);
       S2 += f(dx * i + dx);
    }
    return dx * (SA + 2 * S2 + 4 * S4 - SB) / 6;
}


double integral(double a, double b, double (*f)(double), int N )
{
    double dx, S;

    dx = (b - a) / N;

    S = 0;
    for(int i = 0; i < N; i++)
    {
       S += f(0.5 * dx + dx * i + a);

    }
    return S * dx;
}

void test()
{
    double l =3;
    double g =  9.80655;

    double T0  = sqrt(l / g) * 2 * M_PI;

    double T = 4 * integral2(0, M_PI/2, elip, 10000) * T0 / (2 * M_PI);

    printf("T0 = %lf, T = %lf (%lf)", T0, T, 100 * fabs(T - T0)/T);
    printf("\n%lf ", (100 * T0 * test_seq()) / T );
}

int main()
{
   /* int N = 10000000;
    double res = 4 * integral(0, 1, f, N);
    printf("%.12lf  (погрешность = %.12lf)\n", res, fabs(res - M_PI)  );
    res = 4 * integral2(0, 1, f, N);
    printf("%.12lf  (погрешность = %.12lf)\n", res, fabs(res - M_PI)  );

    res = integral(0, M_PI/2, elip, N);
    printf("%.12lf  (погрешность = %.12lf)\n", res, fabs(res - test_seq()  ));
    res = integral2(0, M_PI/2, elip, N);
    printf("%.12lf  (погрешность = %.12lf)\n", res, fabs(res - test_seq()  ));*/
    test();
   // printf("\n%lf, ", test_seq());
    return 0;
}
