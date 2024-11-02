using MediatR;
using Numer.Domain;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.BisectionMethod {
    public class BisectionHandler : IRequestHandler<BisectionCommand, RootResult> {
        public async Task<RootResult> Handle(BisectionCommand request, CancellationToken cancellationToken) {
            double xl = request.LowerBound;
            double xr = request.UpperBound;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;
            double xm = 0;
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

            while (error > tolerance && iteration < maxIterations) {
                xm = (xl + xr) / 2;
                double fc = request.Function(xm);
                error = Math.Abs(xr - xl) / 2;

                iterations.Add(new Iteration {
                    Index = iteration + 1,
                    X = xm,
                    Y = fc,
                    Error = error
                });

                iteration++;

                if (fc == 0 || error < tolerance) {
                    break;
                }

                if (request.Function(xl) * fc < 0) {
                    xr = xm;
                }
                else {
                    xl = xm;
                }
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
