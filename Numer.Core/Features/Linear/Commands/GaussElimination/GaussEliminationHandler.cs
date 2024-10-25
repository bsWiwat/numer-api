using MediatR;
using Numer.Core.Helper;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.GaussElimination {
    public class GaussEliminationHandler : IRequestHandler<GaussEliminationCommand, GaussResult> {
        public async Task<GaussResult> Handle(GaussEliminationCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            double[][] augmentedMatrix = new double[n][];
            for (int i = 0; i < n; i++) {
                augmentedMatrix[i] = new double[n + 1];
                Array.Copy(matrixA[i], augmentedMatrix[i], n);
                augmentedMatrix[i][n] = arrayB[i]; // Add B to the last column
            }

            List<IterationGauss> iterations = new List<IterationGauss>();

            // Forward
            for (int j = 0; j < n; j++) {
                for (int i = j + 1; i < n; i++) {
                    if (i > j) {
                        double c = augmentedMatrix[i][j] / augmentedMatrix[j][j];
                        iterations.Add(new IterationGauss {
                            Type = "forward",
                            I = j,
                            J = i,
                            Factor = -c,
                            Matrix = MatrixHelper.CloneMatrix(augmentedMatrix)
                        });

                        for (int k = j; k <= n; k++) {
                            augmentedMatrix[i][k] -= c * augmentedMatrix[j][k];
                        }
                    }
                }
            }

            // Backward
            double[] result = new double[n];
            result[n - 1] = augmentedMatrix[n - 1][n] / augmentedMatrix[n - 1][n - 1];

            for (int i = n - 2; i >= 0; i--) {
                double sum = 0;
                for (int j = i + 1; j < n; j++) {
                    sum += augmentedMatrix[i][j] * result[j];
                }
                result[i] = (augmentedMatrix[i][n] - sum) / augmentedMatrix[i][i];

                iterations.Add(new IterationGauss {
                    Type = "backward",
                    I = i,
                    J = i,
                    Value = result[i],
                    SumIdx = new List<int>(Enumerable.Range(i + 1, n - i - 1))
                });
            }

            return new GaussResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Data = new GaussData {
                    Iterations = iterations,
                    Matrix = GetFinalMatrix(augmentedMatrix),
                    Result = result
                }
            };
        }

        private static double[][] GetFinalMatrix(double[][] matrix) {
            int n = matrix.Length;
            double[][] finalMatrix = new double[n][];
            for (int i = 0; i < n; i++) {
                finalMatrix[i] = new double[n];
                Array.Copy(matrix[i], finalMatrix[i], n);
            }
            return finalMatrix;
        }
    }
}