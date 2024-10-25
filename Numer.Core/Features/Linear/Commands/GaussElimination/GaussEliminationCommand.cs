using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Linear.Commands.GaussElimination {
    public class GaussEliminationCommand : IRequest<GaussResult> {
        public double[][] MatrixA { get; set; }
        public double[] ArrayB { get; set;}
    }
}