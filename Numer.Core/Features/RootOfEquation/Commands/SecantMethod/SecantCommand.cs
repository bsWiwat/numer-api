using MediatR;
using Numer.Core.Models;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.RootOfEquation.Commands.SecantMethod {
    public class SecantCommand : IRequest<RootResult> {
        public Func<double, double> Function { get; set; }
        public double X0 { get; set; }
        public double X1 { get; set; }
        public double Tolerance { get; set; }
        public int MaxIterations { get; set; }
    }
}