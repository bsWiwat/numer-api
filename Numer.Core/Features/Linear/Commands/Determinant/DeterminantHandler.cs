using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.Determinant {
    public class DeterminantHandler : IRequestHandler<DeterminantCommand, double> {
        public async Task<double> Handle(DeterminantCommand request, CancellationToken cancellationToken) {
            double[][] matrix = request.Matrix;
            int n = matrix.Length;

            if (n != matrix[0].Length) {
                throw new ArgumentException("Matrix must be square.");
            }

            return CalculateDeterminant(matrix, n);
        }

        private double CalculateDeterminant(double[][] matrix, int n) {
            if (n == 1)
                return matrix[0][0];

            double result = 0;
            double[][] temp = new double[n - 1][];

            int sign = 1;

            for (int f = 0; f < n; f++) {
                GetCofactor(matrix, temp, 0, f, n);
                result += sign * matrix[0][f] * CalculateDeterminant(temp, n - 1);
                sign = -sign;
            }

            return result;
        }

        private void GetCofactor(double[][] matrix, double[][] temp, int rowToRemove, int colToRemove, int n) {
            int i = 0, j = 0;

            for (int row = 0; row < n; row++) {
                for (int col = 0; col < n; col++) {
                    if (row != rowToRemove && col != colToRemove) {
                        temp[i] = temp[i] ?? new double[n - 1];
                        temp[i][j++] = matrix[row][col];

                        if (j == n - 1) {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }
    }
}
