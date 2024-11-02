using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Models {
    public class RootRequest {
        public string FunctionExpression { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double Tolerance { get; set; } = 0.000001;
        public int MaxIterations { get; set; } = 1000;
    }
}