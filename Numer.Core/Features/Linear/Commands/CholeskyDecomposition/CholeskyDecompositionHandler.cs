using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.CholeskyDecomposition {
    public class CholeskyDecompositionHandler : IRequestHandler<CholeskyDecompositionCommand, DecompositionResult> {
        public async Task<DecompositionResult> Handle(CholeskyDecompositionCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            int n = matrixA.Length;

            double[][] L = new double[n][];
            for (int i = 0; i < n; i++) {
                L[i] = new double[n];
            }

            // A = L * L^T
            for (int i = 0; i < n; i++) {
                for (int j = 0; j <= i; j++) {
                    double sum = 0;
                    for (int k = 0; k < j; k++) {
                        sum += L[i][k] * L[j][k];
                    }
                    if (i == j) {
                        L[i][j] = Math.Sqrt(matrixA[i][i] - sum);
                    }
                    else {
                        L[i][j] = (1.0 / L[j][j] * (matrixA[i][j] - sum));
                    }
                }
            }

            // LY = B
            double[] y = new double[n];
            for (int i = 0; i < n; i++) {
                double sum = 0;
                for (int j = 0; j < i; j++) {
                    sum += L[i][j] * y[j];
                }
                y[i] = (arrayB[i] - sum) / L[i][i];
            }

            // L^Tx = Y
            double[] result = new double[n];
            for (int i = n - 1; i >= 0; i--) {
                double sum = 0;
                for (int j = i + 1; j < n; j++) {
                    sum += L[j][i] * result[j];
                }
                result[i] = (y[i] - sum) / L[i][i];
            }

            return new DecompositionResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Cholesky decomposition and system solving completed successfully."
                },
                Data = new DecompositionData {
                    MatrixL = L,
                    Result = result
                }
            };
        }
    }
}
