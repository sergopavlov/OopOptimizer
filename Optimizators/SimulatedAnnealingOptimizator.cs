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
/// алгоритм имитации отжига
/// </summary>
public class SimulatedAnnealingOptimizator : IOptimizator<IFunctional<IFunction>, IFunction>
{
    public IVector Minimize(IFunctional<IFunction> objective,
                            IParametricFunction<IFunction> function,
                            IVector initialParameters,
                            IVector? minimumParameters = null,
                            IVector? maximumParameters = null)
    {
        throw new NotImplementedException();
    }
}
