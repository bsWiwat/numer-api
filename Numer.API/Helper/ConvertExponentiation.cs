using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numer.API.Helper {
    public static class ExpressionUtils {
        public static string ConvertExponentiation(string expression) {
            var regex = new Regex(@"(\w+)\^(\d+)");

            return regex.Replace(expression, "Pow($1, $2)");
        }
    }
}