using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.JacobiMethod {
    public class JacobiHandler : IRequestHandler<JacobiCommand, MatrixIterationResult> {
        public async Task<MatrixIterationResult> Handle(JacobiCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            double[] initialX = request.InitialX;
            double tolerance = request.Tolerance;
            int n = matrixA.Length;

            double[] previousX = (double[])initialX.Clone();
            double[] x = new double[n];
            double[] errors = new double[n];
            List<MatrixIteration> iterations = new List<MatrixIteration>();

            for (int iteration = 0; iteration < request.MaxIterations; iteration++) {
                double maxError = 0;

                for (int i = 0; i < n; i++) {
                    double sum = 0;

                    for (int j = 0; j < n; j++) {
                        if (i != j) {
                            sum += matrixA[i][j] * previousX[j];
                        }
                    }

                    x[i] = (arrayB[i] - sum) / matrixA[i][i];

                    // error
                    errors[i] = Math.Abs(x[i] - previousX[i]);
                    maxError = Math.Max(maxError, errors[i]);
                }

                iterations.Add(new MatrixIteration {
                    Iteration = iteration + 1,
                    Error = (double[])errors.Clone(),
                    X = (double[])x.Clone()
                });

                if (maxError < tolerance) {
                    break;
                }

                Array.Copy(x, previousX, n);
            }

            return new MatrixIterationResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Data = new MatrixIterEntity {
                    Result = x?.ToList() ?? null,
                    Iterations = iterations
                }
            };
        }
    }
}