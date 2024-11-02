using MediatR;
using Numer.Core.Features.Linear.Commands.Cramer;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Extrapolation.Commands.MultipleRegression {
    public class MultipleRegressionHandler : IRequestHandler<MultipleRegressionCommand, RegressionResult> {
        private readonly IMediator _mediator;

        public MultipleRegressionHandler(IMediator mediator) {
            _mediator = mediator;
        }

        public async Task<RegressionResult> Handle(MultipleRegressionCommand request, CancellationToken cancellationToken) {
            int n = request.Points.Count();
            int k = request.ArrayX.Length;

            if (n < k + 1) {
                throw new ArgumentException("Number of data points must be greater than the number of independent variables.");
            }

            // Initialize matrices
            double[][] matrixA = new double[k + 1][];
            double[] matrixB = new double[k + 1];

            for (int i = 0; i <= k; i++) {
                matrixA[i] = new double[k + 1];
                matrixB[i] = 0.0;
            }

            // matrixA and matrixB
            for (int i = 0; i < n; i++) {
                var point = request.Points[i];
                for (int ir = 0; ir <= k; ir++) {
                    for (int ic = 0; ic <= k; ic++) {
                        double fr = (ir == 0) ? 1.0 : point.X[ir - 1];
                        double fc = (ic == 0) ? 1.0 : point.X[ic - 1];
                        matrixA[ir][ic] += fr * fc;
                    }
                    if (ir == 0) {
                        matrixB[ir] += point.Y; // Only add Y when ir is 0
                    }
                    else {
                        matrixB[ir] += point.X[ir - 1] * point.Y; // For other cases
                    }
                }
            }

            var command = new CramerCommand {
                MatrixA = matrixA,
                ArrayB = matrixB,
            };

            var resultAi = await _mediator.Send(command, cancellationToken);

            double[] coefficients = resultAi?.Data?.Result;
            double result = coefficients[0]; // Starting with the intercept

            if (coefficients != null) {
                for (int i = 1; i <= k; i++) {
                    result += coefficients[i] * request.ArrayX[i - 1];
                }
            }
            

            return new RegressionResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Multiple regression completed successfully."
                },
                Data = new RegressionData {
                    Result = result,
                    MatrixA = matrixA,
                    MatrixB = matrixB,
                    Ai = coefficients
                }
            };
        }
    }
}
