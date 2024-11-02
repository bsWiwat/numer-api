using MediatR;
using Numer.Domain.Common;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Extrapolation.Commands.SimpleRegression {
    public class SimpleRegressionCommand : IRequest<RegressionResult> {
        public int MOrder { get; set; }
        public PointXY[] Points { get; set; }
    }
}