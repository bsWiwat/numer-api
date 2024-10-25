using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class JacobiResult {
        public Status Status { get; set; }
        public List<double> Result { get; set; }
        public List<JacobiIteration> Iterations { get; set; }
    }

    public class JacobiIteration {
        public int Iteration { get; set; }
        public double[] Error { get; set; }
        public double[] X { get; set; }
    }
}