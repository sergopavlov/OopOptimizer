using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Interfaces.Optimizators;
public interface IOptimizator<TFunctional, TFunction>
    where TFunctional : IFunctional<TFunction>
    where TFunction : IFunction
{
    IVector Minimize(TFunctional objective,
                     IParametricFunction<TFunction> function,
                     IVector initialParameters,
                     IVector? minimumParameters = default,
                     IVector? maximumParameters = default);
}
