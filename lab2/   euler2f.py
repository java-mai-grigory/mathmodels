import scipy as np
import numpy as np2
import matplotlib.pyplot as plt

from scipy import integrate
from scipy.integrate import odeint


time = np2.linspace(0, 20, 1000)

k2 = 0

def deriv(y, t):
    return np2.array([ y[1], -y[1] * t - y[0] ])


yinit = np2.array([1, 1])   

y = odeint(deriv, yinit, time)

print(y[:,0])
#plt.axes([0, 10, 0, 0.01])
plt.plot(time, y[:,0])
plt.show()

