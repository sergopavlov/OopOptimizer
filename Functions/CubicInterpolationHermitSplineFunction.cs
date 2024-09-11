using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;
/// <summary>
/// Сплайн (не линейный)
/// </summary>
public class CubicInterpolationHermitSplineFunction : IParametricFunction<IFunction>
{
    public IFunction Bind(IVector parameters)
    {
        throw new NotImplementedException();
    }
}
