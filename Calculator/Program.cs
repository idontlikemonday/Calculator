using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        // TODO: Refactor this
        public static void Main(string[] args)
        {
            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    try
                    {
                        Calculator.Instance.ExecuteOperation();
                        Console.WriteLine(Calculator.Instance.Result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        Calculator.Instance.AfterOperation();
                    }
                }
                else if (Char.IsDigit(key.KeyChar))
                {
                    Calculator.Instance.AddDigitToCurrentOperand(Convert.ToByte(key.KeyChar.ToString()));
                }
                else
                {
                    Calculator.Instance.AddOperator(key.KeyChar.ToString());
                }
            }
        }
    }
}
