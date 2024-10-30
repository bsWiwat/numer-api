using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Interpolation.Commands.LagrangePolynomials {
    public class LagrangePolynomialsHandler : IRequestHandler<LagrangePolynomialsCommand, InterpolationResult> {
        public async Task<InterpolationResult> Handle(LagrangePolynomialsCommand request, CancellationToken cancellationToken) {
            int n = request.Points.Count;
            double[] x = new double[n];
            double[] fx = new double[n];

            for (int i = 0; i < n; i++) {
                x[i] = request.Points[i].X;
                fx[i] = request.Points[i].Y;
            }

            double result = 0.0;
            List<double> facs = new List<double>();

            for (int i = 0; i < n; ++i) {
                double li = 1.0;
                for (int j = 0; j < n; ++j) {
                    if (j != i) {
                        li *= (request.X - x[j]) / (x[i] - x[j]);
                    }
                }
                facs.Add(li);
                result += li * fx[i];
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