using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Interpolation.Commands.SplineInterpolation {
    public class SplineInterpolationHandler : IRequestHandler<SplineInterpolationCommand, InterpolationResult> {
        public Task<InterpolationResult> Handle(SplineInterpolationCommand request, CancellationToken cancellationToken) {
            int n = request.Points.Count;
            if (n < 2) {
                throw new ArgumentException("At least two data points are required.");
            }

            // Extract x and fx
            double[] x = request.Points.Select(p => p.X).ToArray();
            double[] fx = request.Points.Select(p => p.Y).ToArray();

            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] d = new double[n];
            double[] e = new double[n];

            // Form the tridiagonal
            for (int i = 1; i < n - 1; i++) {
                a[i] = x[i] - x[i - 1];
                b[i] = 2.0 * (x[i + 1] - x[i - 1]);
                c[i] = x[i + 1] - x[i];
                d[i] = 6.0 * ((fx[i + 1] - fx[i]) / (x[i + 1] - x[i]) -
                              (fx[i] - fx[i - 1]) / (x[i] - x[i - 1]));
            }

            // Boundary
            b[0] = 1.0;
            c[0] = 0.0;
            d[0] = 0.0;
            a[n - 1] = 0.0;
            b[n - 1] = 1.0;
            d[n - 1] = 0.0;

            // Forward
            for (int i = 1; i < n; i++) {
                double m = a[i] / b[i - 1];
                b[i] -= m * c[i - 1];
                d[i] -= m * d[i - 1];
            }

            // Back substitution second derivatives
            e[n - 1] = d[n - 1] / b[n - 1];
            for (int i = n - 2; i >= 0; i--) {
                e[i] = (d[i] - c[i] * e[i + 1]) / b[i];
            }

            // Perform
            double ff = 0.0;
            for (int i = 1; i < n; i++) {
                if (request.X >= x[i - 1] && request.X <= x[i]) {
                    double d1 = x[i] - request.X;
                    double d2 = request.X - x[i - 1];
                    double dd = x[i] - x[i - 1];
                    double t1 = e[i - 1] * d1 * d1 * d1 / (6.0 * dd);
                    double t2 = e[i] * d2 * d2 * d2 / (6.0 * dd);
                    double t3 = (fx[i - 1] / dd - e[i - 1] * dd / 6.0) * d1;
                    double t4 = (fx[i] / dd - e[i] * dd / 6.0) * d2;
                    ff = t1 + t2 + t3 + t4;
                    break;
                }
            }

            return Task.FromResult(new InterpolationResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Interpolation and system solving completed successfully."
                },
                Data = new InterIteration {
                    Result = ff,
                    Fac = e.ToArray()
                }
            });
        }
    }
}