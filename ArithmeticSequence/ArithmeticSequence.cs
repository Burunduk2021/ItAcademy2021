using System;

namespace ArithmeticSequence
{
    public static class ArithmeticSequence
    {
        public static int Calculate(int number, int add, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException($"{count} is less or equal then 0");
            }

            if (number == int.MaxValue && add > 0)
            {
                throw new OverflowException($"{nameof(number)}:{number} is int.MaxValue and {nameof(add)}:{add} more then 0");
            }
            else if (number == int.MinValue && add < 0)
            {
                throw new OverflowException($"{nameof(number)}:{number} is int.MinValue and {nameof(add)}:{add} less then 0");
            }

            double result = 0;

            for (int i = 1; i <= count; i++)
            {
                result += number + (add * (i - 1));
            }
            
            return (int)result;
        }
    }
}
