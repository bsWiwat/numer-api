using MediatR;
using Numer.Domain;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.BisectionMethod;

public class BisectionHandler : IRequestHandler<BisectionCommand, RootResult> {
    public BisectionHandler() { }

    public async Task<RootResult> Handle(BisectionCommand request, CancellationToken cancellationToken) {
        double xl = request.LowerBound;
        double xr = request.UpperBound;
        double tolerance = request.Tolerance;
        int maxIterations = request.MaxIterations;
        double xm = 0;
        double error = double.MaxValue;
        int iteration = 0;

        var iterations = new List<BaseIteration>();

        if (request.Function(xl) * request.Function(xr) >= 0) {
            throw new ArgumentException("The function must have different signs at the bounds.");
        }

        while (error > tolerance || iteration < maxIterations) {
            xm = (xl + xr) / 2;
            double fc = request.Function(xm);
            error = Math.Abs(xr - xl) / 2;

            iterations.Add(new BaseIteration {
                Index = iteration + 1,
                X = xm,
                Y = fc,
                Error = error,
            });

            iteration++;

            if (fc < 0) {
                xl = xm;
            }
            else {
                xr = xm;
            }
        }

        return new RootResult {
            Root = xm,
            Error = error,
            Iterations = iterations
        };
    }
}
