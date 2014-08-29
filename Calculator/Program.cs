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
            var key = Console.ReadKey(true);
            while (!IsEscapePressed(key))
            {
                if (IsEnterPressed(key))
                {
                    Calculator.Instance.TryExecuteOperation();
                }
                else if (IsDigitKeyPressed(key))
                {
                    Calculator.Instance.AddDigitToCurrentOperand(Convert.ToByte(key.KeyChar.ToString()));
                }
                else
                {
                    Calculator.Instance.AddOperatorOrExecuteChainOfOperations(key.KeyChar.ToString());
                }
                Console.Write(key.KeyChar);
                key = Console.ReadKey(true);
            }
        }

        private static bool IsEscapePressed(ConsoleKeyInfo key)
        {
            return key.Key == ConsoleKey.Escape;
        }

        private static bool IsEnterPressed(ConsoleKeyInfo key)
        {
            return key.Key == ConsoleKey.Enter;
        }

        private static bool IsDigitKeyPressed(ConsoleKeyInfo key)
        {
            return Char.IsDigit(key.KeyChar);
        }
    }
}
