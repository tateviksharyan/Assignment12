using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12
{
    class IMathServiceImpl : IMathService
    {
        public double Add(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        public double Div(double firstValue, double secondValue)
        {
            if (secondValue == 0)
            {
                throw new Exception(" Second value can not be 0.");
            }

            return firstValue / secondValue;
        }

        public double Mult(double firstValue, double secondValue)
        {
            if (firstValue == 0 || secondValue == 0)
            {
                return 0;
            }

            return firstValue * secondValue;
        }

        public double Sub(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }
    }
}
