using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class RootResult {
        public double Root { get; set; }
        public double Error { get; set; }
        public List<BaseIteration> Iterations { get; set; }
    }
}