using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Integration.Commands.CompositeTrapezoidal {
    public class CompositeTrapezoidalCommand : IRequest<TrapezoidalResult> {
        public Func<double, double> Function { get; set; }
        public double XEnd { get; set; }
        public double XStart { get; set; }
        public int NumberIntervals { get; set; }
    }

    public class CompositeTrapezoidalRequest {
        public string FunctionExpression { get; set; }
        public double XEnd { get; set; }
        public double XStart { get; set; }
        public int NumberIntervals { get; set; }
    }
}