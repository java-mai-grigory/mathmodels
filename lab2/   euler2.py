import scipy as np
import numpy as np2
import matplotlib.pyplot as plt

from scipy import integrate
from scipy.integrate import odeint

g = 9.8

m = 0.08	
kg = 0.1	
ks = 0	
x0 = -0.03

time = np2.linspace(0, 55, 200);

k2 = 0

def deriv(y, t):
    a = kg / m
    b = ks / m
    return np2.array([y[1], -a * y[0] - b * y[1]])


yinit = np2.array([x0, 0])   

y = odeint(deriv, yinit, time)

print(y[:,0])
#plt.axes([0, 10, 0, 0.01])
plt.plot(time, y[:,0])
plt.show()

