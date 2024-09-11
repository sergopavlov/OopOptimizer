using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;
using Interfaces.Optimizators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizators;
/// <summary>
/// метод сопряжённых градиентов
/// </summary>
public class ConjugateGradientOptimizator : IOptimizator<IDifferentiableFunctional, IDifferentiableFunction>
{
    public IVector Minimize(IDifferentiableFunctional objective, IParametricFunction<IDifferentiableFunction> function, IVector initialParameters, IVector? minimumParameters = null, IVector? maximumParameters = null)
    {
        throw new NotImplementedException();
    }
}
