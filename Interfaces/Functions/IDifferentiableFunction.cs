using Interfaces.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Functions;
public interface IDifferentiableFunction : IFunction
{
    /// <summary>
    /// Возвращает градиент функции по параметрам исходной IParametricFunction в заданной точке
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    IVector Gradient(IVector point);
}
