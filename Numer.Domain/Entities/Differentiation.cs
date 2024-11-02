using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class DifferentiationResult {
        public Status Status { get; set; }
        public DifferentiationData Data { get; set; }
    }

    public class DifferentiationData {
        public double Result { get; set;}
        public double[] Fx { get; set; }
        public double ExactResult { get; set; }
        public double ErrorValue { get; set; }
     }
}