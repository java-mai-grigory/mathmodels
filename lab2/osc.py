import math
import pylab
from scipy.integrate import odeint
from matplotlib import mlab

def deriv(start, t):
    x0 = 0.09
    ks = 0.06
    kg = 0.14
    m = 0.14

    a = ks / m 
    b = kg / m
    x, v = start
    return [v, -a * v - b * x]

def rk_lib(start, tlist):
    return odeint(deriv, start, tlist)



def euler(tlist, dt):
    xlist = []
    x0 = 0.09
    ks = 0.06
    kg = 0.14
    m = 0.14

    a = ks / m 
    b = kg / m
    x = x0
    v = 0
    xprev = x0
    vprev = 0
    for t in tlist:
        x = xprev + vprev * dt
        v = vprev + (- a * vprev - b * xprev) * dt
        xprev = x
        vprev = v
        xlist.append(x)
    return xlist

def func (t):
    x0 = 0.09
    ks = 0.06
    kg = 0.14
    m = 0.14
    k = math.sqrt(kg / m)
    n =  ks / (2 * m)
    D = 4 * n * n - 4 * k * k
    alpha = -n
    beta = math.sqrt( math.fabs(D) ) / 2
    C1 = x0
    C2 = - alpha * x0 / beta

    return math.exp(alpha * t) * (C1 * math.cos(beta * t) + C2 * math.sin(beta * t))

def analitic(tlist):
    xlist = [func(t) for t in tlist]
    return xlist

tmin = 0
tmax = 0.25
dt = 0.1
x0 = 0.09
                
tlist = mlab.frange(tmin, tmax, dt)

pylab.plot (tlist, analitic(tlist))
pylab.plot (tlist, euler(tlist, dt))
pylab.plot (tlist, rk_lib([x0, 0], tlist)[:, 0] )

print euler(tlist, dt)
print analitic(tlist)
print rk_lib([x0, 0], tlist)[:, 0]
pylab.show()