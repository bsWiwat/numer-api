using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.GraphicalMethod {
    public class GraphicalHandler : IRequestHandler<GraphicalCommand, RootResult> {
        public async Task<RootResult> Handle(GraphicalCommand request, CancellationToken cancellationToken) {
            double lowerBound = request.LowerBound;
            double upperBound = request.UpperBound;
            double tolerance = request.Tolerance;
            int maxIterations = request.MaxIterations;

            var iterations = new List<Iteration>();
            double root = double.NaN;
            double error = double.MaxValue;

            // Use the optimized graphical method
            root = FindRootByEfficientGraphicalMethod(request.Function, lowerBound, upperBound, tolerance, maxIterations, iterations);

            if (!double.IsNaN(root)) {
                error = Math.Abs(request.Function(root));
            }

            return new RootResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Computation completed successfully."
                },
                Data = new Data {
                    Result = root,
                    Iterations = iterations
                }
            };
        }

        private double FindRootByEfficientGraphicalMethod(Func<double, double> function, double lowerBound, double upperBound, double tolerance, int maxIterations, List<Iteration> iterations) {
            double root = double.NaN;
            int iteration = 0;

            double step = (upperBound - lowerBound) / 10;
            double previousValue = function(lowerBound);

            while (step > tolerance && iteration < maxIterations) {
                for (double x = lowerBound; x <= upperBound && iteration < maxIterations; x += step) {
                    double currentValue = function(x);

                    iterations.Add(new Iteration {
                        Index = iteration + 1,
                        X = x,
                        Y = currentValue,
                    });

                    if (Math.Abs(currentValue) < tolerance) {
                        root = x;
                        return root;
                    }

                    // Detect sign change
                    if (previousValue * currentValue < 0) {
                        // Root between previous x - current x
                        root = RefineRoot(function, x - step, x, tolerance, iteration, iterations);
                        return root;
                    }

                    previousValue = currentValue;
                    iteration++;
                }

                // Reduce step size
                step /= 2;
            }

            return root;
        }

        private double RefineRoot(Func<double, double> function, double lowerBound, double upperBound, double tolerance,int iteration, List<Iteration> iterations) {
            double root = double.NaN;

            while ((upperBound - lowerBound) > tolerance) {
                double midPoint = (lowerBound + upperBound) / 2;
                double fMid = function(midPoint);
                iterations.Add(new Iteration {
                    Index = iteration + 1,
                    X = midPoint,
                    Y = fMid
                });

                if (Math.Abs(fMid) < tolerance) {
                    return midPoint;
                }

                if (function(lowerBound) * fMid < 0) {
                    upperBound = midPoint;  // Root in lower
                }
                else {
                    lowerBound = midPoint;  // Root in upper
                }
            }

            root = (lowerBound + upperBound) / 2;
            return root;
        }
    }
}
