using MediatR;
using Numer.Domain;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.OnePointMethod {
    public class OnePointHandler : IRequestHandler<OnePointCommand, RootResult> {
        public async Task<RootResult> Handle(OnePointCommand request, CancellationToken cancellationToken) {
            double xOld = request.XInitial;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;
            int iteration = 0;
            double error = double.MaxValue;
            double xNew = xOld;

            var iterations = new List<Iteration>();

            while (error > tolerance && iteration < maxIterations) {
                xNew = request.Function(xOld);
                error = Math.Abs(xNew - xOld);

                iterations.Add(new Iteration {
                    Index = iteration + 1,
                    X = xNew,
                    Y = request.Function(xNew),
                    Error = error
                });

                xOld = xNew;
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
