import numpy
from matplotlib import pyplot
numpy.set_printoptions(precision=3)
#M = numpy.diagflat([1 for i in range(6)], 0) + numpy.diagflat([5 for i in range(5)], -1) + numpy.diagflat([7 for i in range(5)], 1)
#print(M)
L = 4.
J = 25
dx = float(L)/float(J-1)
x_grid = numpy.array([j*dx for j in range(J)])
T = 2
N = 1000
dt = float(T)/float(N-1)
t_grid = numpy.array([n*dt for n in range(N)])
D_u = 1
U0 = numpy.sin(x_grid * numpy.pi / L)
Ua = numpy.sin(x_grid * numpy.pi / L) * numpy.exp(-D_u * D_u * numpy.pi * numpy.pi * T / (L * L))
#U = U0
#U = U0[slice(1, J - 1)]
U = U0[1 : J - 1]
#pyplot.ylim((0., 2.1))
#pyplot.xlabel('x'); 
#pyplot.ylabel('concentration')
#pyplot.plot(x_grid, U)
#pyplot.show()
sigma_u = float(D_u*dt)/float((2.*dx*dx))
#A_u = numpy.diagflat([-sigma_u for i in range(J-1)], -1) +\
  #    numpy.diagflat([1.+sigma_u]+[1.+2.*sigma_u for i in range(J-2)]+[1.+sigma_u]) +\
   #   numpy.diagflat([-sigma_u for i in range(J-1)], 1)
A_u = numpy.diagflat([-sigma_u for i in range(J-3)], -1) +\
      numpy.diagflat([1.+2.*sigma_u for i in range(J-2)]) +\
      numpy.diagflat([-sigma_u for i in range(J-3)], 1)
print(A_u)
        
#B_u = numpy.diagflat([sigma_u for i in range(J-1)], -1) +\
 #     numpy.diagflat([1.-sigma_u]+[1.-2.*sigma_u for i in range(J-2)]+[1.-sigma_u]) +\
 #     numpy.diagflat([sigma_u for i in range(J-1)], 1)
      
B_u = numpy.diagflat([sigma_u for i in range(J-3)], -1) +\
      numpy.diagflat([1.- 2.*sigma_u for i in range(J-2)]) +\
      numpy.diagflat([sigma_u for i in range(J-3)], 1)

for ti in range(1,N):
    f = B_u.dot(U)
    U_new = numpy.linalg.solve(A_u, B_u.dot(U) )
   
    U = U_new

U = numpy.append( U0[0], numpy.append( U, U0[J - 1]) )

print(U)
    
pyplot.ylim((0., 2.1))
pyplot.xlabel('x')
pyplot.ylabel('temperature')
pyplot.plot(x_grid, U)
pyplot.plot(x_grid, U0)
pyplot.plot(x_grid, Ua)
pyplot.show()
 

