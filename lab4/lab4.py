import numpy as np

p = np.array([[0.3, 0.7], [0.4, 0.6]])
r = np.dot(p, p)

r = np.dot(r, p)
r = np.dot(r, p)


print p

print r