using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public readonly struct Range
    {

        public readonly int Min;

        public readonly int Max;

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool IsInRange(int n)
        {
            return n >= Min && (n <= Max || Max == -1);
        }

        public override string ToString()
        {
            if (Max == -1)
            {
                return $"at least {Min}";
            }
            else if (Max == Min)
            {
                return $"{Max}";
            }
            else
            {
                return $"between {Min} and {Max}";
            }
        }
    }
}
