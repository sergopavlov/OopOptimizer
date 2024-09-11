using Interfaces.Functionals;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionals;
/// <summary>
/// linf норма разности с требуемыми значениями в наборе точек (не реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
/// </summary>
public class LinfNormFunctional : IFunctional<IFunction>
{
    public double Value(IFunction function)
    {
        throw new NotImplementedException();
    }
}
