using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Functionals;
public interface IFunctional<TFunction>where TFunction : IFunction
{
    double Value(TFunction function);
}
