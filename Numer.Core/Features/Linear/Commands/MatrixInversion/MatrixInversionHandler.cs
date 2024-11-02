using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.MatrixInversion {
    public class MatrixInversionHandler : IRequestHandler<MatrixInversionCommand, MatrixInversionResult> {
        public async Task<MatrixInversionResult> Handle(MatrixInversionCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            // [A | 1]
            double[][] augmentedMatrix = new double[n][];
            for (int i = 0; i < n; i++) {
                augmentedMatrix[i] = new double[2 * n];
                Array.Copy(matrixA[i], augmentedMatrix[i], n);
                augmentedMatrix[i][i + n] = 1.0;
            }

            // Gauss-Jordan
            for (int j = 0; j < n; j++) {
                // swap
                for (int i = j; i < n; i++) {
                    if (augmentedMatrix[i][j] != 0) {
                        if (i != j) {
                            var temp = augmentedMatrix[j];
                            augmentedMatrix[j] = augmentedMatrix[i];
                            augmentedMatrix[i] = temp;
                        }
                        break;
                    }
                }

                double pivot = augmentedMatrix[j][j];
                if (pivot != 0) {
                    for (int k = 0; k < 2 * n; k++) {
                        augmentedMatrix[j][k] /= pivot;
                    }
                }

                for (int i = 0; i < n; i++) {
                    if (i != j) {
                        double factor = augmentedMatrix[i][j];
                        for (int k = 0; k < 2 * n; k++) {
                            augmentedMatrix[i][k] -= factor * augmentedMatrix[j][k];
                        }
                    }
                }
            }

            // Extract the inverse matrix
            double[][] invertedMatrix = new double[n][];
            for (int i = 0; i < n; i++) {
                invertedMatrix[i] = new double[n];
                Array.Copy(augmentedMatrix[i], n, invertedMatrix[i], 0, n);
            }

            // Multiply inverse of A by arrayB
            double[] result = new double[n];
            for (int i = 0; i < n; i++) {
                result[i] = 0;
                for (int j = 0; j < n; j++) {
                    result[i] += invertedMatrix[i][j] * arrayB[j];
                }
            }

            return new MatrixInversionResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Matrix inversion and result computation completed successfully."
                },
                Data = new Matrix {
                    MatrixI = invertedMatrix,
                    Result = result
                }
            };
        }
    }
}
