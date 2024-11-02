using MediatR;
using Numer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Features.Extrapolation.Commands.MultipleRegression {
    public class MultipleRegressionCommand : IRequest<RegressionResult> {
        public double[] ArrayX { get; set; }
        public List<DataPoint> Points { get; set; }
    }

    public class DataPoint {
        public double[] X { get; set; }
        public double Y { get; set; }
    }
}