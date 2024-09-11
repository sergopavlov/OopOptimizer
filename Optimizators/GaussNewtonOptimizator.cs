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
/// Алгоритм Гаусса-Ньютона
/// </summary>
public class GaussNewtonOptimizator : IOptimizator<ILeastSquaresFunctional, IDifferentiableFunction>
{
    public IVector Minimize(ILeastSquaresFunctional objective, IParametricFunction<IDifferentiableFunction> function, IVector initialParameters, IVector? minimumParameters = null, IVector? maximumParameters = null)
    {
        throw new NotImplementedException();
    }
}
