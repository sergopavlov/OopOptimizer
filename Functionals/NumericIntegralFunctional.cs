using Interfaces.Functionals;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionals;
/// <summary>
/// Интеграл по некоторой области (численно)
/// </summary>
public class NumericIntegralFunctional : IFunctional<IFunction>
{
    public double Value(IFunction function)
    {
        throw new NotImplementedException();
    }
}
