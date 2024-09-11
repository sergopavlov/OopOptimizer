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
/// l2 норма разности с требуемыми значениями в наборе точек (реализует IDifferentiableFunctional, реализует ILeastSquaresFunctional)
/// </summary>
public class L2NormFunctional : ILeastSquaresFunctional, IDifferentiableFunctional
{
    public IVector Gradient(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }

    public IMatrix Jacobian(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }

    public IVector Residual(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }

    public double Value(IDifferentiableFunction function)
    {
        throw new NotImplementedException();
    }
}
