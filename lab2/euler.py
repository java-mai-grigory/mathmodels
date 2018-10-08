import math
import pylab
from scipy.integrate import odeint
from matplotlib import mlab

def func (t):
    return math.exp(t ** 2)

def deriv(y, t):
    return 2 * t * y


def analitic(xlist):
    ylist = [func(x) for x in xlist]
    return ylist

def euler(tlist, y0, dt):
    y = y0
    ylist = []
    for t in tlist:
        dy = 2.0 * t *  dt  * y
        y += dy
        ylist.append(y)
    return ylist

def rk_lib(tlist, y0):
    ylist =  odeint(deriv, y0, tlist)
    return ylist

tmin = 0
tmax = 1.0
dt = 0.1
y0 = 1
                
tlist = mlab.frange(tmin, tmax, dt)

pylab.plot (tlist, analitic(tlist))
pylab.plot (tlist, euler(tlist, y0, dt))
pylab.plot (tlist, rk_lib(tlist, y0))

pylab.show()