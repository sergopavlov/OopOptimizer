using Functions;
using Interfaces.DataStorage;
using Interfaces.Functions;

namespace Tests.FunctionsTests.Common;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed record InterpolationHermiteSplineFunctionFixture
{
    private readonly CubicInterpolationHermitSplineFunction _function;
    private readonly IVector _parameters;

    public InterpolationHermiteSplineFunctionFixture()
    {
        IVector mesh = new Vector { 0.0, 1.0, 2.0 };
        _parameters = new Vector { 0.0, 0.0, 1.0, 1.0, 4.0, 0.0 }; 
        _function = new(mesh);
    }

    public IFunction Bind() => _function.Bind(_parameters);
}