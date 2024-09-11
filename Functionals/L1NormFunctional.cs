using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionals;

/// <summary>
/// l1 норма разности с требуемыми значениями в наборе точек (реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
/// </summary>
public class L1NormFunctional : IDifferentiableFunctional
{
    public IVector Gradient(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }

    public double Value(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }
}
