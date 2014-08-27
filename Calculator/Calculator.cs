using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        private double _first;
        private double _second;
        private double _result;

        private delegate double OperatorDelegate(double x, double y);
        private Dictionary<string, OperatorDelegate> _operators;

        public string Operator { get; set; }

        public double Result
        {
            get
            {
                if (!Double.IsNaN(_result))
                {
                    return _result;
                }
                else
                {
                    if (!Double.IsNaN(CurrentOperandValue))
                    {
                        return CurrentOperandValue;
                    }
                    return 0d;
                }
            }
        }

        private double CurrentOperandValue
        {
            get
            {
                return (IsFirstOpReady ? _second : _first);
            }
            set
            {
                if (IsFirstOpReady)
                {
                    _second = value;
                }
                else
                {
                    _first = value;
                }
            }
        }

        public bool IsFirstOpReady
        {
            get
            {
                return !String.IsNullOrEmpty(Operator);
            }
        }

        private static Calculator _instance = new Calculator();
        public static Calculator Instance
        {
            get { return _instance; }
        }

        private Calculator()
        {
            _first = Double.NaN;
            _second = Double.NaN;
            _result = Double.NaN;

            _operators = new Dictionary<string, OperatorDelegate>
            {
                { "+", (a, b) => (a + b) },
                { "-", (a, b) => (a - b) },
                { "*", (a, b) => (a * b) },
                { "/", (a, b) => (a / b) },
            };
        }

        public void AddDigitToCurrentOperand(byte digit)
        {
            if (Double.IsNaN(CurrentOperandValue))
            {
                CurrentOperandValue = digit;
            }
            else
            {
                CurrentOperandValue = CurrentOperandValue * 10 + digit;
            }
        }

        public void AddOperator(string symbolToAdd)
        {
            if (!String.IsNullOrEmpty(Operator))
            {
                ExecuteOperation();
                _first = _result;
            }
            Operator = symbolToAdd;
            _second = Double.NaN;
        }

        public List<string> GetAvailableOperators()
        {
            return _operators.Keys.ToList();
        }

        public bool IsAvailableOperator(string operatorToCheck)
        {
            return GetAvailableOperators().Contains(operatorToCheck);
        }

        public void ExecuteOperation()
        {
            if (Double.IsNaN(_first))
            {
                throw new Exception("First Operand Is Missing");
            }
            if (Double.IsNaN(_second))
            {
                throw new Exception("Second Operand Is Missing");
            }
            if (String.IsNullOrEmpty(Operator))
            {
                throw new Exception("Operator Is Missing");
            }
            if (!IsAvailableOperator(Operator))
            {
                throw new Exception(String.Format("Operator '{0}' Is Not Available", Operator));
            }
            _result = _operators[Operator](_first, _second);
        }

        public void AfterOperation()
        {
            _first = _second = Double.NaN;
            Operator = String.Empty;
        }
    }
}
