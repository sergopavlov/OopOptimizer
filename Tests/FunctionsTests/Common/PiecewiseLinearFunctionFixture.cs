using Functions;
using Interfaces.DataStorage;
using Interfaces.Functions;

namespace Tests.FunctionsTests.Common;

// ReSharper disable once ClassNeverInstantiated.Global
/// <summary>
/// Fixture for piecewise linear function.
/// </summary>
public sealed record PiecewiseLinearFunctionFixture
{
    private readonly PolyLinearFunction _function;
    private readonly IVector _parameters;

    public PiecewiseLinearFunctionFixture()
    {
        IVector mesh = new Vector { 0.0, 1.0, 2.0 };
        _parameters = new Vector { 0.0, 2.0, 4.0 };
        _function = new(mesh);
    }

    public IDifferentiableFunction Bind() => _function.Bind(_parameters);
}