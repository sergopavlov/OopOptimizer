using FluentAssertions;
using Functions;
using Interfaces.DataStorage;
using Tests.DataGeneration;

namespace Tests.FunctionsTests;

/// <summary>
/// Tests for the polynomial function.
/// </summary>
public sealed class PolynomialFunctionTests(PolynomialFunction function) : IClassFixture<PolynomialFunction>
{
    [Theory]
    [MemberData(nameof(TestDataGenerator.GetPolynomialFunctionValueData), MemberType = typeof(TestDataGenerator))]
    public void Value_PolynomialFunction_ShouldReturnCorrectResult(IVector parameters, IVector points, double expected)
    {
        // Arrange
        var boundFunction = function.Bind(parameters);

        // Act
        var result = boundFunction.Value(points);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Bind_EmptyParameters_ShouldThrowArgumentException()
    {
        // Arrange
        var parameters = new Vector();

        // Act
        var act = () => function.Bind(parameters);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Value_DifferentDimensions_ShouldThrowArgumentException()
    {
        // Arrange
        var parameters = new Vector { 1.0, 2.0 };
        var points = new Vector { 1.0, 2.0 };
        var boundFunction = function.Bind(parameters);

        // Act
        var act = () => boundFunction.Value(points);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}