using Numer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Numer.Domain.Entities {
    public class DecompositionResult {
        public Status Status { get; set; }
        public DecompositionData Data { get; set; }
    }
}