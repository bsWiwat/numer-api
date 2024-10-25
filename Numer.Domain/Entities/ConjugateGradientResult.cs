using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class ConjugateGradientResult {
        public Status Status { get; set; }
        public ConjugateGradientData Data { get; set; }
    }

    public class ConjugateGradientData {
        public List<ConjugateGradientIteration> Iterations { get; set; }
        public List<double> Result { get; set; }
    }

    public class ConjugateGradientIteration {
        public int Iter { get; set; }
        public double[] Dk_1 { get; set; }
        public double Lk_1 { get; set; }
        public double[] Xk { get; set; }
        public double[] Rk { get; set; }
        public double Ek { get; set; }
    }
}