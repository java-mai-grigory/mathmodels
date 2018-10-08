# -*- coding: utf-8 -*-
import math
import scipy.integrate as integrate

def f(x):  
    return math.sqrt(1 - x**2 )

def integral(a, b, N, f):
    dx = 1.0 *(b - a) / N
    S = 0
    for i in range(0, N):
        S += f(0.5 * dx + dx * i)
    return S * dx


def simpson(a, b, N, f):
    dx = 1.0 * (b - a) / N
    S2 = S4 = 0
    SA = f(a)
    SB = f(b) 
    for i in range(0, N):
        S4 += f(0.5 * dx + i * dx)
        S2 += f(dx * i + dx)

    return dx * (SA + 2 * S2 + 4 * S4 - SB) / 6
     
def lib(a, b,  f):
    return integrate.quad(f, a, b)[0]

N = 10000
res = 4 * integral(0, 1, N, f)
delta = math.fabs(res - math.pi) 
print('Результат %.12f (погрешность: %.12f)' %  (res, delta))
res = 4 * simpson(0, 1, N, f)
delta = math.fabs(res - math.pi) 
print('Результат %.12f (погрешность: %.12f)' %  (res, delta))
res = 4 * lib(0, 1, f)
delta = math.fabs(res - math.pi) 
print('Результат %.12f (погрешность: %.12f)' %  (res, delta))

