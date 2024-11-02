using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Integration.Commands.CompositeTrapezoidal {
    public class CompositeTrapezoidalHandler : IRequestHandler<CompositeTrapezoidalCommand, TrapezoidalResult> {

        public async Task<TrapezoidalResult> Handle(CompositeTrapezoidalCommand request, CancellationToken cancellationToken) {
            int n = request.NumberIntervals;
            double h = (request.XEnd - request.XStart) / n;

            double result = (request.Function(request.XStart) + request.Function(request.XEnd)) / 2.0;

            for (int i = 1; i < n; i++) {
                double x = request.XStart + i * h;
                result += request.Function(x);
            }

            result *= h;

            return new TrapezoidalResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Composite trapezoidal integration completed successfully."
                },
                Data = new TrapezoidalData {
                    Result = result,
                    XStart = request.XStart,
                }
            };
        }
    }
}