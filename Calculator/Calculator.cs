using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    // TODO: Make it singleton
    public class Calculator
    {
        private Operand _first = new Operand();
        private Operand _second = new Operand();
        private double _result;

        private delegate double OperatorDelegate(double x, double y);
        private Dictionary<string, OperatorDelegate> _operations;

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


        public Calculator()
        {
            _result = Double.NaN;

            _operations =
            new Dictionary<string, OperatorDelegate>
            {
                { "+", delegate(double a, double b) {return a + b;} },
               // { "-", this.DoSubtraction },
               // { "*", this.DoMultiplication },
               // { "/", this.DoDivision },
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

        public void AddOperator(string operatorToAdd)
        {
            Operator = operatorToAdd;
            _secondOp = Double.NaN;
        }

        public List<string> GetAvailableOperators()
        {
            return _operations.Keys.ToList();
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
                throw new Exception("Operator Is Not Available");
            }
            _result = _operations[Operator](_firstOp, _secondOp);

            AfterOperation();
        }

        private void AfterOperation()
        {
            _firstOp = _result;
        }
    }
}
