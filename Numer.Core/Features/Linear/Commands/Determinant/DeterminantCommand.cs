using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.Determinant {
    public class DeterminantCommand : IRequest<double>{
        public double[][] Matrix { get; set; }
    }
}