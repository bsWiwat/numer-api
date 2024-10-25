using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.LUDecomposition {
    public class LUDecompositionHandler : IRequestHandler<LUDecompositionCommand, LUDecompositionResult> {
        public async Task<LUDecompositionResult> Handle(LUDecompositionCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            double[][] L = new double[n][];
            double[][] U = new double[n][];
            for (int i = 0; i < n; i++) {
                L[i] = new double[n];
                U[i] = new double[n];
            }

            for (int i = 0; i < n; i++) {
                // U matrix
                for (int j = i; j < n; j++) {
                    U[i][j] = matrixA[i][j];
                    for (int k = 0; k < i; k++) {
                        U[i][j] -= L[i][k] * U[k][j];
                    }
                }

                // L matrix
                for (int j = i; j < n; j++) {
                    if (i == j) {
                        L[i][i] = 1;
                    }
                    else {
                        L[j][i] = matrixA[j][i];
                        for (int k = 0; k < i; k++) {
                            L[j][i] -= L[j][k] * U[k][i];
                        }
                        L[j][i] /= U[i][i];
                    }
                }
            }

            // Forward LY = B
            double[] arrayY = new double[n];
            for (int i = 0; i < n; i++) {
                arrayY[i] = arrayB[i];
                for (int j = 0; j < i; j++) {
                    arrayY[i] -= L[i][j] * arrayY[j];
                }
                arrayY[i] /= L[i][i];
            }

            // Backward UX = Y
            double[] result = new double[n];
            for (int i = n - 1; i >= 0; i--) {
                result[i] = arrayY[i];
                for (int j = i + 1; j < n; j++) {
                    result[i] -= U[i][j] * result[j];
                }
                result[i] /= U[i][i];
            }

            return new LUDecompositionResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "LU decomposition and result computation completed successfully."
                },
                Data = new LUData {
                    ArrayY = arrayY,
                    MatrixL = L,
                    MatrixU = U,
                    Result = result
                }
            };
        }
    }
}
