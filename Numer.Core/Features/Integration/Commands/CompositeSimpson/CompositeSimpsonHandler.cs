using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Integration.Commands.CompositeSimpson {
    public class SimpsonHandler : IRequestHandler<CompositeSimpsonCommand, IntegrateResult> {
        public async Task<IntegrateResult> Handle(CompositeSimpsonCommand request, CancellationToken cancellationToken) {
            int n = request.NumberIntervals;

            if (n % 2 != 0 && n != 1) {
                throw new ArgumentException("Number of intervals must be even for Simpson's rule.");
            }

            double h = (request.XEnd - request.XStart) / n;

            double result = request.Function(request.XStart) + request.Function(request.XEnd);

            var fxValues = new Dictionary<string, FxValue>();
            int index = 1;

            for (int i = 1; i < n; i++) {
                double x = request.XStart + i * h;
                double y = request.Function(x);

                if (i % 2 == 0) {
                    result += 2 * y;
                }
                else {
                    result += 4 * y;
                }

                fxValues[index.ToString()] = new FxValue { X = x, Y = y };
                index++;
            }

            result *= h / 3;

            return new IntegrateResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Composite Simpson integration completed successfully."
                },
                Data = new IntegrateData {
                    Result = result,
                    Fx = fxValues,
                    Start = request.XStart,
                    End = request.XEnd
                }
            };
        }
    }

}