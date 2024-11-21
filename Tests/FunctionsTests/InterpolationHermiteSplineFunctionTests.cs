using FluentAssertions;
using Functions;
using Interfaces.DataStorage;
using Tests.FunctionsTests.Common;

namespace Tests.FunctionsTests;

/// <summary>
/// Tests for the interpolation Hermite spline function.
/// </summary>
public sealed class InterpolationHermiteSplineFunctionTests(InterpolationHermiteSplineFunctionFixture fixture)
    : IClassFixture<InterpolationHermiteSplineFunctionFixture>
{
    [Fact]
    public void Value_InMiddleOfInterval_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 1.0;
        var boundFunction = fixture.Bind();

        // Act
        var value = boundFunction.Value(new Vector { 1.0 });

        // Assert
        value.Should().Be(expected);
    }

    [Fact]
    public void Value_OutsideIntervalLeft_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 0.0;
        var boundFunction = fixture.Bind();

        // Act
        var value = boundFunction.Value(new Vector { -0.5 });

        // Assert
        value.Should().Be(expected);
    }

    [Fact]
    public void Value_OutsideIntervalRight_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 4.0;
        var boundFunction = fixture.Bind();

        // Act
        var value = boundFunction.Value(new Vector { 2.5 });

        // Assert
        value.Should().Be(expected);
    }

    [Fact]
    public void Value_AtFirstPoint_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 0.0;
        var boundFunction = fixture.Bind();

        // Act
        var value = boundFunction.Value(new Vector { 0.0 });

        // Assert
        value.Should().Be(expected);
    }

    [Fact]
    public void Value_AtLastPoint_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 4.0;
        var boundFunction = fixture.Bind();

        // Act
        var value = boundFunction.Value(new Vector { 2.0 });

        // Assert
        value.Should().Be(expected);
    }

    [Fact]
    public void CallConstructor_InvalidParametersCount_ShouldThrowArgumentException()
    {
        // Arrange
        var mesh = new Vector();

        // Act
        var act = () => _ = new CubicInterpolationHermitSplineFunction(mesh);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Bind_InvalidParametersCount_ShouldThrowArgumentException()
    {
        // Arrange
        var mesh = new Vector { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0 };
        var function = new CubicInterpolationHermitSplineFunction(mesh);

        // Act
        var act = () => function.Bind(new Vector());

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Value_DifferentDimensions_ShouldThrowArgumentException()
    {
        // Arrange
        var mesh = new Vector { 0.0, 1.0, 2.0 };
        var parameters = new Vector { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
        var function = new CubicInterpolationHermitSplineFunction(mesh);
        var point = new Vector { 0.0, 1.0 };

        // Act
        var act = () => function.Bind(parameters).Value(point);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}