using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class TrapezoidalResult {
        public Status Status { get; set; }
        public TrapezoidalData Data { get; set; }
    }
    public class TrapezoidalData {
        public double Result { get; set; }
        public double XStart { get; set; }
        public double XEnd { get; set; }
    }

    public class IntegrateResult {
        public Status Status { get; set; }
        public IntegrateData Data { get; set; }
    }

    public class IntegrateData {
        public double Result { get; set; }
        public Dictionary<string, FxValue> Fx { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
    }

    public class FxValue {
        public double X { get; set; }
        public double Y { get; set; }
    }

}