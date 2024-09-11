using Interfaces.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Functions;
public interface IParametricFunction<TFunction> where TFunction : IFunction
{
    TFunction Bind(IVector parameters);
}
