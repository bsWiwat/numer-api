using MediatR;
using Numer.Domain;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.BisectionMethod {
    public class BisectionCommand : IRequest<RootResult> {
        public Func<double, double> Function { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Tolerance { get; set; } = 0.000001;
        public int MaxIterations { get; set; } = 100;
    }
}