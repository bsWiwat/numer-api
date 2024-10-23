using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Common {
    public class Data {
        public double Result { get; set; }
        public List<Iteration> Iterations { get; set; }
    }

    public class Iteration {
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Error { get; set; }
    }
}