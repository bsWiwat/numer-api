using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.ConjugateGradient {
    public class ConjugateGradientHandler : IRequestHandler<ConjugateGradientCommand, ConjugateGradientResult> {
        public async Task<ConjugateGradientResult> Handle(ConjugateGradientCommand request, CancellationToken cancellationToken) {
            double[][] matrixA = request.MatrixA;
            double[] arrayB = request.ArrayB;
            double[] x = request.InitialX;
            int maxIterations = request.MaxIterations;
            double tolerance = request.Tolerance;
            int n = matrixA.Length;

            // Initialize residual
            double[] r = new double[n];
            for (int i = 0; i < n; i++) {
                double sum = 0;
                for (int j = 0; j < n; j++) {
                    sum += matrixA[i][j] * x[j];
                }
                r[i] = arrayB[i] - sum;
            }

            double[] d = (double[])r.Clone();
            double[] previousR = new double[n];
            List<ConjugateGradientIteration> iterations = new List<ConjugateGradientIteration>();

            for (int iter = 1; iter <= maxIterations; iter++) {
                double[] Ad = new double[n];
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        Ad[i] += matrixA[i][j] * d[j];
                    }
                }

                double alphaNumerator = 0;
                double alphaDenominator = 0;
                for (int i = 0; i < n; i++) {
                    alphaNumerator += r[i] * r[i];
                    alphaDenominator += d[i] * Ad[i];
                }
                double alpha = alphaNumerator / alphaDenominator;

                // Update x
                for (int i = 0; i < n; i++) {
                    x[i] += alpha * d[i];
                }

                // Update r
                Array.Copy(r, previousR, n);
                for (int i = 0; i < n; i++) {
                    r[i] -= alpha * Ad[i];
                }

                double error = 0;
                for (int i = 0; i < n; i++) {
                    error += r[i] * r[i];
                }
                if (Math.Sqrt(error) < tolerance) break;

                double betaNumerator = 0;
                double betaDenominator = 0;
                for (int i = 0; i < n; i++) {
                    betaNumerator += r[i] * r[i];
                    betaDenominator += previousR[i] * previousR[i];
                }
                double beta = betaNumerator / betaDenominator;

                // Update d
                for (int i = 0; i < n; i++) {
                    d[i] = r[i] + beta * d[i];
                }

                iterations.Add(new ConjugateGradientIteration {
                    Iter = iter,
                    Dk_1 = (double[])d.Clone(),
                    Lk_1 = alpha,
                    Xk = (double[])x.Clone(),
                    Rk = (double[])r.Clone(),
                    Ek = Math.Sqrt(error)
                });

                if (Math.Sqrt(error) < tolerance) break;
            }

            return new ConjugateGradientResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Cholesky decomposition and system solving completed successfully."
                },
                Data = new ConjugateGradientData {
                    Iterations = iterations,
                    Result = x.ToList()
                }
            };
        }
    }
}