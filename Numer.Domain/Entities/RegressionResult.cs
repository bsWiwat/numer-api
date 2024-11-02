using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class RegressionResult {
        public Status Status { get; set; }
        public RegressionData Data { get; set; }
    }

    public class RegressionData {
        public double Result { get; set; }
        public double[][] MatrixA { get; set; }
        public double[] MatrixB { get; set; }
        public double[] Ai { get; set; }
    }
}