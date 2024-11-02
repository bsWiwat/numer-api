using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Integration.Commands.Trapezoidal {
    public class TrapezoidalHandler : IRequestHandler<TrapezoidalCommand, TrapezoidalResult> {
        public async Task<TrapezoidalResult> Handle(TrapezoidalCommand request, CancellationToken cancellationToken) {
            double h = request.XEnd - request.XStart;

            double fStart = request.Function(request.XStart);
            double fEnd = request.Function(request.XEnd);

            double result = (h / 2) * (fStart + fEnd);

            return new TrapezoidalResult {
                Status = new Status {
                    StatusCode = (int)EnumMasterType.MasterType.Success,
                    StatusName = EnumMasterType.MasterType.Success.ToString(),
                    Message = "Interpolation and system solving completed successfully."
                },
                Data = new TrapezoidalData {
                    Result = result,
                    XStart = request.XStart,
                }
            };
        }
    }
}