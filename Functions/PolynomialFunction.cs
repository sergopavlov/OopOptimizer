using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;
/// <summary>
/// Полином n-й степени в одномерном пространстве (число параметров n+1, не реализует IDifferentiableFunction)
/// </summary>
public class PolynomialFunction : IParametricFunction<IFunction>
{
    public IFunction Bind(IVector parameters)
    {
        throw new NotImplementedException();
    }
}
