using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Operand
    {
        public double Value { get; set; }

        public Operand()
        {
            Value = Double.NaN;
        }
    }
}
