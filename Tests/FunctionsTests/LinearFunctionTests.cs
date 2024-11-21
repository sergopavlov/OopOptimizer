using FluentAssertions;
using Functions;
using Interfaces.DataStorage;
using Tests.DataGeneration;

namespace Tests.FunctionsTests;

/// <summary>
/// Tests for the linear function.
/// </summary>
public sealed class LinearFunctionTests(LinearFunction function) : IClassFixture<LinearFunction>
{
    [Theory]
    [MemberData(nameof(TestDataGenerator.GetLinearFunctionValueData), MemberType = typeof(TestDataGenerator))]
    public void Value_LinearFunction_ShouldReturnCorrectResult(IVector parameters, IVector points, double expected)
    {
        // Arrange
        var boundFunction = function.Bind(parameters);

        // Act
        var result = boundFunction.Value(points);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetLinearFunctionGradientData), MemberType = typeof(TestDataGenerator))]
    public void Gradient_LinearFunction_ShouldReturnCorrectGradient(IVector parameters, IVector points, IVector expected)
    {
        // Arrange
        var boundFunction = function.Bind(parameters);

        // Act
        var result = boundFunction.Gradient(points);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Bind_EmptyParameters_ShouldThrowArgumentException()
    {
        // Arrange
        var parameters = new Vector();

        // Act
        var action = () => function.Bind(parameters);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Value_DifferentDimensions_ShouldThrowArgumentException()
    {
        // Arrange
        var parameters = new Vector { 1.0, 2.0 };
        var points = new Vector { 1.0, 2.0 };
        var boundFunction = function.Bind(parameters);

        // Act
        var action = () => boundFunction.Value(points);

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}