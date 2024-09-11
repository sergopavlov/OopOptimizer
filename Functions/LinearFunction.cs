using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;

/// <summary>
/// Линейная функция в n-мерном пространстве (число параметров n+1, реализует IDifferentiableFunction)
/// </summary>
public class LinearFunction : IParametricFunction<IDifferentiableFunction>
{
    public IDifferentiableFunction Bind(IVector parameters)
    {
        throw new NotImplementedException();
    }
}
