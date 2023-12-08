using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yisp_Interpreter
{
    public interface ICallable
    {
        public Range Arity();
        public object Call(Interpreter interpreter, List<SExpr> arguments);
    }
}
