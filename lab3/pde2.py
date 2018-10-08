import numpy
from matplotlib import pyplot
numpy.set_printoptions(precision=3)
L = 4.
J = 5
dx = float(L)/float(J-1)
x_grid = numpy.array([j*dx for j in range(J)])
T = 2
N = 3
dt = float(T)/float(N-1)
t_grid = numpy.array([n*dt for n in range(N)])
a = 1
U0 = 8 * numpy.sin(x_grid * numpy.pi / L)
UA = 8 * numpy.sin(x_grid * numpy.pi / L) * numpy.exp(-a * a * numpy.pi * numpy.pi * T / (L * L))

u = [[0] * J for i in range(N)]
for i in range (0, J):
      u[0][i] = 8 * numpy.sin(x_grid[i] * numpy.pi / L)

for k in range (1, N):
      for i in range(1, J - 1):
           u[k][i] = (dt / dx ) * (u[k - 1][i + 1] + u[k - 1][i - 1] - 2 * u[k - 1][i]) + u[k - 1][i]; 


            


#for i in range(1, 

    
#pyplot.ylim((0., 2.1))
#pyplot.xlabel('x')
#pyplot.ylabel('T')
#pyplot.plot(x_grid, U)
#pyplot.plot(x_grid, U0)
#pyplot.plot(x_grid, UA)
#pyplot.show()
 
print 'initial state'
print U0
print 'analytic calculation'
print UA
print 'solution'
for i in range(0, N):
      print u[i]