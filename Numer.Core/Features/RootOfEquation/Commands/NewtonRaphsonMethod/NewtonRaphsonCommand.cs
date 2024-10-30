using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.NewtonRaphsonMethod {
    public class NewtonRaphsonCommand : IRequest<RootResult> {
        public Func<double, double> Function { get; set; }
        public double XInitial { get; set; }
        public double Tolerance { get; set; }
        public int MaxIterations { get; set; }
    }
}