using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.NewConjugate {
    public class ConjugateHandler : IRequestHandler<ConjugateCommand, ConjugateGradientResult> {
        public async Task<ConjugateGradientResult> Handle(ConjugateCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] matrixB = request.ArrayB;
            double[] x = request.InitialX;
            int maxIterations = request.MaxIterations;
            double tolerance = request.Tolerance;
            int n = matrixA.Length;

            double[] matrixR = new double[n];
            for (int i = 0; i < n; i++) {
                double sum = 0;
                for (int j = 0; j < n; j++) {
                    sum += matrixA[i][j] * x[j];
                }
                matrixR[i] = matrixB[i] - sum;
            }

            double[] matrixD = (double[])matrixR.Clone();
            double[] matrixRi = new double[n];

            for (int i = 0; i < maxIterations; i++) {
                double[] matrixAD = new double[n];
                for (int j = 0; j < n; j++) {
                    for (int k = 0; k < n; k++) {
                        matrixAD[i] += matrixA[i][k] * matrixD[i];
                    }
                }

                double lamdaUp = 0;
                double lamdaDown = 0;
                for (int j = 0; j < n; j++) {
                    lamdaUp += matrixD[j] * matrixR[j];
                    lamdaDown += matrixAD[j] * matrixD[j];
                }

                double lamda = lamdaUp / lamdaDown;

                for (int j = 0; j < n; j++) {
                    x[i] += lamda * matrixD[j];
                }

                Array.Copy(matrixR, matrixRi, n);
                double err = 0;
                for (int j = 0; j < n; j++) {
                    err += matrixRi[j] * matrixRi[j];
                }
                if (err < tolerance) {
                    break;
                }


            }

            return new ConjugateGradientResult {
            };
        }
    }
}