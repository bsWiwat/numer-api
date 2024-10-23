using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class MatrixResult {
        public Status Status { get; set; }
        public MatrixData Data { get; set; }
    }
}