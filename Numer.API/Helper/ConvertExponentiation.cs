using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numer.API.Helper {
    public static class ExpressionUtils {
        public static string ConvertExponentiation(string expression) {
            expression = expression.Replace("e", "2.71828");
            expression = Regex.Replace(expression, @"(\d+)([a-zA-Z_]\w*)", "$1*$2");
            expression = Regex.Replace(expression, @"(\d*\.?\d+|[a-zA-Z_]\w*)\^([-+]?\d*\.?\d+|[a-zA-Z_]\w*)", "Pow($1, $2)");

            return expression;
        }
    }
}