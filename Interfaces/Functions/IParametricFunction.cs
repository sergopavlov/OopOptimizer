using Interfaces.DataStorage;

namespace Interfaces.Functions;

public interface IParametricFunction<out TFunction> where TFunction : IFunction
{
    TFunction Bind(IVector parameters);
}