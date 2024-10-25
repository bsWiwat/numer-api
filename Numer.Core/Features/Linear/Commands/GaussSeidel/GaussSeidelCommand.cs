using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.GaussSeidel {
    public class GaussSeidelCommand : IRequest<MatrixIterationResult>{
        public double[][] MatrixA { get; set; }
        public double[] ArrayB { get; set; }
        public double[] InitialX { get; set; }
        public double Tolerance { get; set; }
        public int MaxIterations { get; set; } = 1000;
    }
}