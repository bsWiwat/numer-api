using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Integration.Commands.Trapezoidal {
    public class TrapezoidalCommand : IRequest<TrapezoidalResult> {
        public Func<double, double> Function { get; set; }
        public double XEnd { get; set; }
        public double XStart { get; set; }
    }

    public class TrapezoidalRequest {
        public string FunctionExpression { get; set; }
        public double XEnd { get; set; }
        public double XStart { get; set; }
    }
}