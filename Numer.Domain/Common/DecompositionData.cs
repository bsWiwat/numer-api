using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Common {
    public class DecompositionData {
        public double[] ArrayY { get; set; }
        public double[][] MatrixL { get; set; }
        public double[][] MatrixU { get; set; }
        public double[] Result { get; set; }
    }
}