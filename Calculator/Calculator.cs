using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calculator
    {
        private Operand _first;
        private Operand _second;
        private double _result;

        private delegate double OperatorDelegate(double x, double y);
        private Dictionary<string, OperatorDelegate> _operators;

        public string Operator { get; set; }

        private double _firstOp
        {
            get
            {
                return _first.Value;
            }
            set
            {
                _first.Value = value;
            }
        }
        private double _secondOp
        {
            get
            {
                return _second.Value;
            }
            set
            {
                _second.Value = value;
            }
        }
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
                    if (!Double.IsNaN(CurrentOperand.Value))
                    {
                        return CurrentOperand.Value;
                    }
                    return 0d;
                }
            }
        }

        private Operand CurrentOperand
        {
            get
            {
                return (IsFirstOpReady ? _second : _first);
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
            _first = new Operand();
            _second = new Operand();
            _result = Double.NaN;

            _operators = new Dictionary<string, OperatorDelegate>
            {
                { "+", (double a, double b) => (a + b) },
                { "-", (double a, double b) => (a - b) },
                { "*", (double a, double b) => (a * b) },
                { "/", (double a, double b) => (a / b) },
            };
        }

        public void AddDigitToCurrentOperand(byte digit)
        {
            if (Double.IsNaN(CurrentOperand.Value))
            {
                CurrentOperand.Value = digit;
            }
            else
            {
                CurrentOperand.Value = CurrentOperand.Value * 10 + digit;
            }
        }

        public void AddOperator(string symbolToAdd)
        {
            if (!String.IsNullOrEmpty(Operator))
            {
                ExecuteOperation();
                _firstOp = _result;
            }
            Operator = symbolToAdd;
            _secondOp = Double.NaN;
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
            if (Double.IsNaN(_firstOp))
            {
                throw new Exception("First Operand Is Missing");
            }
            if (Double.IsNaN(_secondOp))
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
            _result = _operators[Operator](_firstOp, _secondOp);
        }

        public void AfterOperation()
        {
            _firstOp = _secondOp = Double.NaN;
            Operator = String.Empty;
        }
    }
}
