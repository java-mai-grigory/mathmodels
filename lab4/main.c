#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#define N 2

int main()
{
    float a[][N] = {{0.3, 0.7}, {0.4, 0.6}}; 
    
    for(int i = 0; i < N; i++)
    {
        for(int j = 0; j < N; j++)
            printf("%f ", a[i][j] );
        
        printf("\n");
    }

    float** p = malloc(sizeof(float*) * N);
    for(int i = 0; i < N; i++)
        p[i] = malloc(sizeof(float) * N);

    for(int i = 0; i < N; i++)
        for(int j = 0; j < N; j++)
              for(int k = 0; k < N; k++)
                p[i][j] += a[i][k] * a[k][j];
            
        
    for(int i = 0; i < N; i++)
    {
        for(int j = 0; j < N; j++)
            printf("%f ", p[i][j] );
        
        printf("\n");
    }

    


    return 0;
}