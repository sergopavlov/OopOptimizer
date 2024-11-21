using FluentAssertions;
using Functionals;
using Functions;
using Interfaces.DataStorage;

namespace Tests.FunctionalsTests;

/// <summary>
/// Tests for numeric integral functional.
/// </summary>
public sealed class NumericIntegralFunctionalTests(LinearFunction linearFunction) : IClassFixture<LinearFunction>
{
    private const int Precision = 14;

    [Fact]
    public void Value_OneDimensional_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 0.5;
        var lower = new Vector { 0.0 };
        var upper = new Vector { 1.0 };
        var function = linearFunction.Bind(new Vector { 0.0, 1.0 }); // f(x) = x
        var integralFunctional = new NumericIntegralFunctional(lower, upper);

        // Act
        var result = integralFunctional.Value(function);

        // Assert
        expected.Should().BeApproximately(result, Precision);
    }

    [Fact]
    public void Value_TwoDimensional_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 1.0;
        var lower = new Vector { 0.0, 0.0 };
        var upper = new Vector { 1.0, 1.0 };
        var function = linearFunction.Bind(new Vector { 0.0, 1.0, 1.0 }); // f(x, y) = x + y
        var integralFunctional = new NumericIntegralFunctional(lower, upper);

        // Act
        var result = integralFunctional.Value(function);

        // Assert
        expected.Should().BeApproximately(result, Precision);
    }

    [Fact]
    public void Value_ThreeDimensional_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 1.5;
        var lower = new Vector { 0.0, 0.0, 0.0 };
        var upper = new Vector { 1.0, 1.0, 1.0 };
        var function = linearFunction.Bind(new Vector { 0.0, 1.0, 1.0, 1.0 }); // f(x, y, z) = x + y + z
        var integralFunctional = new NumericIntegralFunctional(lower, upper);

        // Act
        var result = integralFunctional.Value(function);

        // Assert
        expected.Should().BeApproximately(result, Precision);
    }

    [Fact]
    public void Value_FourDimensional_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 2.0;
        var lower = new Vector { 0.0, 0.0, 0.0, 0.0 };
        var upper = new Vector { 1.0, 1.0, 1.0, 1.0 };
        var function = linearFunction.Bind(new Vector { 0.0, 1.0, 1.0, 1.0, 1.0 }); // f(x, y, z, w) = x + y + z + w
        var integralFunctional = new NumericIntegralFunctional(lower, upper);

        // Act
        var result = integralFunctional.Value(function);

        // Assert
        expected.Should().BeApproximately(result, Precision);
    }

    [Fact]
    public void Constructor_DifferentDimensions_ShouldThrowArgumentException()
    {
        // Arrange
        var lower = new Vector { 0.0, 0.0 };
        var upper = new Vector { 1.0 };

        // Act
        Action action = () => _ = new NumericIntegralFunctional(lower, upper);

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}