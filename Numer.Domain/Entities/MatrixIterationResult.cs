using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class MatrixIterationResult {
        public Status Status { get; set; }
        public MatrixIterEntity Data { get; set; }
    }

    public class MatrixIterEntity {
        public List<double> Result { get; set; }
        public List<MatrixIteration> Iterations { get; set; }
    }

    public class MatrixIteration {
        public int Iteration { get; set; }
        public double[] Error { get; set; }
        public double[] X { get; set; }
    }
}