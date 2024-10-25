using MediatR;
using Numer.Core.Features.Linear.Commands.GaussJordan;
using Numer.Core.Helper;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.GaussJordanElimination {
    public class GaussJordanEliminationHandler : IRequestHandler<GaussJordanCommand, GaussResult> {
        public async Task<GaussResult> Handle(GaussJordanCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            // Create augmented matrix
            double[][] augmentedMatrix = new double[n][];
            for (int i = 0; i < n; i++) {
                augmentedMatrix[i] = new double[n + 1];
                Array.Copy(matrixA[i], augmentedMatrix[i], n);
                augmentedMatrix[i][n] = arrayB[i]; // Add B to the last column
            }

            List<IterationGauss> iterations = new List<IterationGauss>();

            for (int j = 0; j < n; j++) {
                for (int i = j; i < n; i++) {
                    if (augmentedMatrix[i][j] != 0) {
                        // Swap
                        if (i != j) {
                            double[] temp = augmentedMatrix[j];
                            augmentedMatrix[j] = augmentedMatrix[i];
                            augmentedMatrix[i] = temp;
                        }
                        break;
                    }
                }

                double pivot = augmentedMatrix[j][j];
                double factor = 0;
                if (pivot != 0) {
                    for (int k = j; k <= n; k++) {
                        augmentedMatrix[j][k] /= pivot;
                    }
                }

                for (int i = 0; i < n; i++) {
                    if (i != j) {
                        factor = augmentedMatrix[i][j];
                        for (int k = j; k <= n; k++) {
                            augmentedMatrix[i][k] -= factor * augmentedMatrix[j][k];
                        }
                    }
                }

                iterations.Add(new IterationGauss {
                    I = j,
                    J = j,
                    Factor = -factor,
                    Matrix = MatrixHelper.CloneMatrix(augmentedMatrix)
                });
            }

            double[] result = new double[n];
            for (int i = 0; i < n; i++) {
                result[i] = augmentedMatrix[i][n];
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
