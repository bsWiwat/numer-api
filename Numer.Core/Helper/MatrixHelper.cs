using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Core.Helper {
    public static class MatrixHelper {
        public static double[][] CloneMatrix(double[][] matrix) {
            int n = matrix.Length;
            double[][] clone = new double[n][];
            for (int i = 0; i < n; i++) {
                clone[i] = new double[matrix[i].Length];
                Array.Copy(matrix[i], clone[i], matrix[i].Length);
            }
            return clone;
        }
    }
}
