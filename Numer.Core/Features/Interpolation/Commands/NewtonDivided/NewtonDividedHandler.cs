using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Interpolation.Commands.NewtonDivided {
    public class NewtonDividedHandler : IRequestHandler<NewtonDividedCommand, InterpolationResult> {
        public async Task<InterpolationResult> Handle(NewtonDividedCommand request, CancellationToken cancellationToken) {
            int n = request.Points.Count;
            double[,] fx = new double[n, n];
            double[] x = new double[n];

            for (int i = 0; i < n; i++) {
                x[i] = request.Points[i].X;
                fx[i, 0] = request.Points[i].Y;
            }

            for (int j = 1; j < n; j++) {
                for (int i = 0; i < n - j; i++) {
                    fx[i, j] = (fx[i + 1, j - 1] - fx[i, j - 1]) / (x[i + j] - x[i]);
                }
            }

            double result = fx[0, 0];
            double fac = 1.0;
            List<double> facs = new List<double>();

            for (int i = 1; i < n; i++) {
                fac *= (request.X - x[i - 1]);
                facs.Add(fac);
                result += fx[0, i] * fac;
            }

            return new InterpolationResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Interpolation and system solving completed successfully."
                },
                Data = new InterIteration {
                    Result = result,
                    Fac = facs.ToArray()
                }
            };
        }
    }
}