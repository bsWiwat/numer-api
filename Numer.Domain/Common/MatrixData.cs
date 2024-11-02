using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Common {
    public class MatrixData {
        public double[] Result { get; set; }
        public double[][][] MatrixAi { get; set; }
        public double[] DetAi { get; set; }
        public double DetA { get; set; }
    }
}