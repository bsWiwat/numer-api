using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.Differentiation.Commands.NumericalDifferentiation {
    public class NumericalDifferentiationHandler : IRequestHandler<NumericalDifferentiationCommand, DifferentiationResult> {
        public Task<DifferentiationResult> Handle(NumericalDifferentiationCommand request, CancellationToken cancellationToken) {
            double result;
            double[] fx;

            // function values
            fx = CalculateFxValues(request.Function, request.X, request.H, request.Order, request.Direction);

            // fx values
            result = CalculateDerivative(fx, request.H, request.Order, request.Direction, request.Error);

            double exactResult = request.Function(request.X);
            double errorValue = Math.Abs(result - exactResult);

            var data = new DifferentiationData {
                Result = result,
                Fx = fx,
                ExactResult = exactResult,
                ErrorValue = errorValue
            };

            return Task.FromResult(new DifferentiationResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Numerical differentiation completed successfully."
                },
                Data = data
            });
        }

        private double[] CalculateFxValues(Func<double, double> func, double x, double h, int order, string direction) {
            var fx = new List<double>();
            int n = order + (direction.ToLower() == "central" ? 1 : 0);

            switch (direction.ToLower()) {
                case "forward":
                    for (int i = 0; i <= order; i++) {
                        fx.Add(func(x + i * h));
                    }
                    break;
                case "backward":
                    for (int i = 0; i <= order; i++) {
                        fx.Add(func(x - i * h));
                    }
                    break;
                case "central":
                    for (int i = -order / 2; i <= order / 2; i++) {
                        fx.Add(func(x + i * h));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid direction specified.");
            }

            return fx.ToArray();
        }

        private double CalculateDerivative(double[] fx, double h, int order, string direction, string error) {
            double result = 0;

            switch (order) {
                case 1:
                    if (error == "h") {
                        result = direction == "forward" ? (fx[1] - fx[0]) / h :
                                 direction == "backward" ? (fx[0] - fx[1]) / h :
                                 (fx[2] - fx[0]) / (2 * h); // Central
                    }
                    else if (error == "h2") {
                        result = direction == "forward" ? (-3 * fx[0] + 4 * fx[1] - fx[2]) / (2 * h) :
                                 direction == "backward" ? (3 * fx[0] - 4 * fx[1] + fx[2]) / (2 * h) :
                                 (fx[2] - 2 * fx[1] + fx[0]) / (h * h); // Central
                    }
                    break;

                case 2:
                    if (error == "h2") {
                        result = direction == "forward" ? (2 * fx[0] - 5 * fx[1] + 4 * fx[2] - fx[3]) / (h * h) :
                                 direction == "backward" ? (2 * fx[0] - 5 * fx[1] + 4 * fx[2] - fx[3]) / (h * h) :
                                 (-fx[2] + 16 * fx[1] - 30 * fx[0] + 16 * fx[3] - fx[4]) / (12 * h * h);
                    }
                    break;

                case 3:
                    if (error == "h") {
                        result = direction == "forward" ? (-11 * fx[0] + 18 * fx[1] - 9 * fx[2] + 2 * fx[3]) / (6 * h) :
                                 direction == "backward" ? (11 * fx[0] - 18 * fx[1] + 9 * fx[2] - 2 * fx[3]) / (6 * h) :
                                 (fx[3] - 3 * fx[2] + 3 * fx[1] - fx[0]) / (h * h * h);
                    }
                    break;

                default:
                    throw new ArgumentException("Unsupported order specified.");
            }

            return result;
        }
    }
}
