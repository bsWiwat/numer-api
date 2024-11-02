using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.SecantMethod {
    public class SecantHandler : IRequestHandler<SecantCommand, RootResult> {
        public async Task<RootResult> Handle(SecantCommand request, CancellationToken cancellationToken) {
            double xOld = request.LowerBound;
            double xNew = request.UpperBound;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;
            double error = double.MaxValue;
            int iteration = 0;

            var iterations = new List<Iteration>();

            while (error > tolerance && iteration < maxIterations) {
                double fxNew = request.Function(xNew);
                double fxOld = request.Function(xOld);

                if (fxNew == fxOld) {
                    return new RootResult {
                        Status = new Status {
                            StatusCode = (int)EnumMasterType.MasterType.BadRequest,
                            StatusName = EnumMasterType.MasterType.BadRequest.ToString(),
                            Message = "Division by zero in Secant method due to equal function values at consecutive points."
                        }
                    };
                }

                double xNext = xNew - (fxNew * (xNew - xOld)) / (fxNew - fxOld);
                error = Math.Abs(xNext - xNew);

                iterations.Add(new Iteration {
                    Index = iteration + 1,
                    X = xNext,
                    Y = request.Function(xNext),
                    Error = error
                });

                xOld = xNew;
                xNew = xNext;
                iteration++;
            }

            return new RootResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Data = new Data {
                    Result = xNew,
                    Iterations = iterations
                }
            };
        }
    }
}
