using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        public static void Main(string[] args)
        {
            var calc = new Calculator();
            while (true)
            {
                var key = Console.ReadKey();

                if (Char.IsDigit(key.KeyChar))
                {
                    calc.AddDigitToCurrentOperand(Convert.ToByte(key.KeyChar.ToString()));
                }
                if (calc.IsAvailableOperator(key.KeyChar.ToString()))
                {
                    calc.AddOperator(key.KeyChar.ToString());
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    calc.ExecuteOperation();
                    Console.WriteLine();
                    Console.WriteLine(calc.Result);
                }
            }
        }
    }
}
