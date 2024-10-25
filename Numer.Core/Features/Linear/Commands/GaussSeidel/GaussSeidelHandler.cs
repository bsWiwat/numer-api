using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.GaussSeidel {
    public class GaussSeidelHandler : IRequestHandler<GaussSeidelCommand, MatrixIterationResult> {
        public async Task<MatrixIterationResult> Handle(GaussSeidelCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            double[] initialX = request.InitialX;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;
            int n = matrixA.Length;

            double[] x = (double[])initialX.Clone(); // Initial guess
            double[] errors = new double[n];
            List<MatrixIteration> iterations = new List<MatrixIteration>();

            for (int iteration = 0; iteration < maxIterations; iteration++) {
                double maxError = 0;

                for (int i = 0; i < n; i++) {
                    double sum = 0;

                    for (int j = 0; j < n; j++) {
                        if (i != j) {
                            sum += matrixA[i][j] * x[j];
                        }
                    }

                    double newXi = (arrayB[i] - sum) / matrixA[i][i];

                    // Calculate current error
                    errors[i] = Math.Abs(newXi - x[i]);
                    maxError = Math.Max(maxError, errors[i]);

                    // Update x[i] immediately for next calculations within the same iteration
                    x[i] = newXi;
                }

                iterations.Add(new MatrixIteration {
                    Iteration = iteration + 1,
                    Error = (double[])errors.Clone(),
                    X = (double[])x.Clone()
                });

                if (maxError < tolerance) {
                    break;
                }
            }

            return new MatrixIterationResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Result = x.ToList(),
                Iterations = iterations
            };
        }
    }
}