using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator
{

    public interface IOperation
    {
        ExtendedAnsClass Operate(double value1, double value2); // value1 - valueMemoried; value2 - valueNow
    }

    public class Sum : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = value1 + value2;
            return ans;
        }
    }

    public class Subtraction : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = value1 - value2;
            return ans;
        }
    }

    public class Division : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            try
            {
                ans._Answer = value1 / value2;
                return ans;
            }
            catch (Exception)
            {
                ans._Desc_id = 1;
                return ans;
            }
        }
    }
    public class Multiplication : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = value1 * value2;
            return ans;
        }
    }

    public class DegreeOfValue : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = Math.Pow(value1, value2);
            return ans;
        }
    }

    public class RootOfValue : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = Math.Pow(value1, 1 / value2);
            return ans;
        }
    }

    public class Log10Value : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            try
            {
                ans._Answer = Math.Log10(value2);
                return ans;
            }
            catch (Exception)
            {
                ans._Desc_id = 2;
                return ans;
            }
        }
    }

    public class LogEValue : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            try
            {
                ans._Answer = Math.Log(value2);
                return ans;
            }
            catch (Exception)
            {
                ans._Desc_id = 2;
                return ans;
            }
        }
    }

    public class Exp : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2) // value1 - valueMemoried; value2 - valueNow
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            ans._Answer = value1 * (Math.Pow(10, value2));
            return ans;
        }
    }

    public class ModDivision : IOperation
    {
        public ExtendedAnsClass Operate(double value1, double value2)
        {
            ExtendedAnsClass ans = new ExtendedAnsClass();
            try
            {
                ans._Answer = value1 % value2;
                return ans;
            }
            catch (Exception)
            {
                ans._Desc_id = 1;
                return ans;
            }
        }
    }

    /*public class PercentageOfTwo : IOperation
    {
        *//*public decimal Operate(decimal value1, decimal value2) => value1 % value2;*//*
        public decimal Operate(decimal value1, decimal value2)
        {
            try
            {
                return (value1 / 100) * value2;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }*/
}
