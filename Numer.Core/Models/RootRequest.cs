using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Models {
    public class RootRequest {
        public string FunctionExpression { get; set; }
        public double X0 { get; set; }
        public double X1 { get; set; }
        public double Tolerance { get; set; } = 0.000001;
        public int MaxIterations { get; set; } = 100;
    }
}