using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;
/// <summary>
/// Кусочно-линейная функция (реализует IDifferentiableFunction)
/// </summary>
public class PolyLinearFunction : IParametricFunction<IDifferentiableFunction>
{
    public IDifferentiableFunction Bind(IVector parameters)
    {
        throw new NotImplementedException();
    }
}
