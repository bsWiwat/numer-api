using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class GaussResult {
        public Status Status { get; set; }
        public GaussData Data { get; set; }
    }

    public class GaussData {
        public List<IterationGauss> Iterations { get; set; }
        public double[][] Matrix { get; set; }
        public double[] Result { get; set; }
    }

        public class IterationGauss {
        public string Type { get; set; }
        public int I { get; set; }
        public int J { get; set; }
        public double Factor { get; set; }
        public double[][] Matrix { get; set; }
        public double Value { get; set; }
        public List<int> SumIdx { get; set; }
    }

}