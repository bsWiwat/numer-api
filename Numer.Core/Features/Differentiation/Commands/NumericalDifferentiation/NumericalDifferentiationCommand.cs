using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Differentiation.Commands.NumericalDifferentiation {
    public class NumericalDifferentiationCommand : IRequest<DifferentiationResult> {
        public Func<double, double> Function { get; set; }
        public double X { get; set; }
        public double H { get; set; }
        public int Order { get; set; }
        public string Error { get; set; }
        public string Direction { get; set; }
    }

    public class NumericalDifferentiationRequest {
        public string FunctionExpression { get; set; }
        public double X { get; set; }
        public double H { get; set; }
        public int Order { get; set; }
        public string Error { get; set; }
        public string Direction { get; set; }
    }
}