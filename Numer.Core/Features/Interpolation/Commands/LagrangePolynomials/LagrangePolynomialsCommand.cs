using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Interpolation.Commands.LagrangePolynomials {
    public class LagrangePolynomialsCommand : IRequest<InterpolationResult> {
        public double X { get; set; }
        public List<PointXY> Points { get; set; }
    }
}