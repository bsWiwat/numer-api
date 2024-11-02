using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class InterpolationResult {
        public Status Status { get; set; }
        public InterIteration Data { get; set; }
    }

    public class InterIteration {
        public double Result { get; set; }
        public double[] Fac { get; set; }
    }
}