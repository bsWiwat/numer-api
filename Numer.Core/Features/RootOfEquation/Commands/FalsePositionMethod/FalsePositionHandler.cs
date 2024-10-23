using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.FalsePositionMethod {
    public class FalsePositionHandler : IRequestHandler<FalsePositionCommand, RootResult> {
        public async Task<RootResult> Handle(FalsePositionCommand request, CancellationToken cancellationToken) {
            double xl = request.LowerBound;
            double xr = request.UpperBound;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;
            double xm = xl;
            double error = double.MaxValue;
            int iteration = 0;

            var iterations = new List<Iteration>();

            if (request.Function(xl) * request.Function(xr) >= 0) {
                return new RootResult {
                    Status = new Status {
                        StatusCode = (int)EnumMasterType.MasterType.BadRequest,
                        StatusName = EnumMasterType.MasterType.BadRequest.ToString(),
                        Message = "The function must have different signs at the bounds."
                    }
                };
            }

            while (error > tolerance || iteration < maxIterations) {
                xm = xr - (request.Function(xr) * (xl - xr)) / (request.Function(xl) - request.Function(xr));
                double fxm = request.Function(xm);
                error = Math.Abs(fxm);

                iterations.Add(new Iteration {
                    Index = iteration + 1,
                    X = xm,
                    Y = fxm,
                    Error = error,
                });

                if (fxm * request.Function(xl) < 0) {
                    xr = xm;
                }
                else {
                    xl = xm;
                }

                iteration++;
            }

            return new RootResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Bisection method completed successfully."
                },
                Data = new Data {
                    Result = xm,
                    Iterations = iterations
                }
            };
        }
    }
}