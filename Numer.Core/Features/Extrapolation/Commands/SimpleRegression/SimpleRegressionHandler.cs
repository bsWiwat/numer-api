using MediatR;
using Numer.Core.Features.Linear.Commands.Cramer;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Extrapolation.Commands.SimpleRegression {
    public class SimpleRegressionHandler : IRequestHandler<SimpleRegressionCommand, RegressionResult> {
        private readonly IMediator _mediator;

        public SimpleRegressionHandler(IMediator mediator) {
            _mediator = mediator;
        }

        public async Task<RegressionResult> Handle(SimpleRegressionCommand request, CancellationToken cancellationToken) {
            int n = request.Points.Count();
            int m = request.MOrder;

            if (n < m + 1 || m <= 0) {
                throw new ArgumentException("Number of data points must be greater than the polynomial order.");
            }

            double[] x = new double[n];
            double[] y = new double[n];

            for (int i = 0; i < n; i++) {
                x[i] = request.Points[i].X;
                y[i] = request.Points[i].Y;
            }

            // Initialize matrixA
            double[][] matrixA = new double[m + 1][];
            for (int i = 0; i <= m; i++) {
                matrixA[i] = new double[m + 1];
            }

            double[] matrixB = new double[m + 1];

            // matrixA and matrixB
            for (int i = 0; i <= m; i++) {
                for (int j = 0; j <= m; j++) {
                    double sum = 0.0;
                    for (int k = 0; k < n; k++) {
                        sum += Math.Pow(x[k], i + j);
                    }
                    matrixA[i][j] = sum;
                }

                double sumB = 0.0;
                for (int k = 0; k < n; k++) {
                    sumB += y[k] * Math.Pow(x[k], i);
                }
                matrixB[i] = sumB;
            }

            var command = new CramerCommand {
                MatrixA = matrixA,
                ArrayB = matrixB,
            };

            var resultAi = await _mediator.Send(command, cancellationToken);

            double result = 0;
            if (resultAi?.Data?.Result != null) {
                for (int i = 0; i <= m; i++) {
                    result += resultAi.Data.Result[i] * Math.Pow(x.Average(), i);
                }
            }

            return new RegressionResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Interpolation and system solving completed successfully."
                },
                Data = new RegressionData {
                    Result = result,
                    MatrixA = matrixA.ToArray(),
                    MatrixB = matrixB.ToArray(),
                    Ai = resultAi?.Data?.Result.ToArray() ?? null
                }
            };
        }
    }
}
