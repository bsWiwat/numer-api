using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Common {
    public class BaseIteration {
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Error { get; set; }
    }
}