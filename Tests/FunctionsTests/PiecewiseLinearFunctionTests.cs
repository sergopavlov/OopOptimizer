using FluentAssertions;
using Functions;
using Interfaces.DataStorage;
using Tests.FunctionsTests.Common;

namespace Tests.FunctionsTests;

/// <summary>
///  Tests for the piecewise linear function.
/// </summary>
public sealed class PiecewiseLinearFunctionTests(PiecewiseLinearFunctionFixture fixture)
    : IClassFixture<PiecewiseLinearFunctionFixture>
{
    [Fact]
    public void Value_InsideInterval_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 1.0;
        var boundFunction = fixture.Bind();

        // Act
        var result = boundFunction.Value(new Vector { 0.5 });

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Value_AtMeshBoundary_ShouldReturnCorrectValue()
    {
        // Arrange
        const double expected = 4.0;
        var boundFunction = fixture.Bind();

        // Act
        var result = boundFunction.Value(new Vector { 2.0 });

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Value_PointIsLeftOfMesh_ShouldReturnFirstParameter()
    {
        // Arrange
        const double expected = 0.0;
        var boundFunction = fixture.Bind();

        // Act
        var result = boundFunction.Value(new Vector { -1.0 });

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Value_PointIsRightOfMesh_ShouldReturnLastParameter()
    {
        // Arrange
        const double expected = 4.0;
        var boundFunction = fixture.Bind();

        // Act
        var result = boundFunction.Value(new Vector { 3.5 });

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Gradient_InsideInterval_ShouldReturnCorrectGradient()
    {
        // Arrange
        var expectedGradient = new Vector { 0.5, 0.5, 0.0 };
        var boundFunction = fixture.Bind();

        // Act
        var gradient = boundFunction.Gradient(new Vector { 0.5 });

        // Assert
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void Gradient_AtMeshBoundary_ShouldReturnCorrectGradient()
    {
        // Arrange
        var expectedGradient = new Vector { 0.0, 0.0, 1.0 };
        var boundFunction = fixture.Bind();

        // Act
        var gradient = boundFunction.Gradient(new Vector { 1.0 });

        // Assert
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void Gradient_OutsideIntervalLeft_ShouldReturnCorrectGradient()
    {
        // Arrange
        var expectedGradient = new Vector { 1.0, 0.0, 0.0 };
        var boundFunction = fixture.Bind();

        // Act
        var gradient = boundFunction.Gradient(new Vector { -0.5 });

        // Assert
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void Gradient_OutsideIntervalRight_ShouldReturnCorrectGradient()
    {
        // Arrange
        var expectedGradient = new Vector { 0.0, 0.0, 1.0 };
        var boundFunction = fixture.Bind();

        // Act
        var gradient = boundFunction.Gradient(new Vector { 2.5 });

        // Assert
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void Bind_EmptyParameters_ShouldThrowArgumentException()
    {
        // Arrange
        var mesh = new Vector { 0.0, 1.0, 2.0, 3.0, 4.0 };
        var parameters = new Vector();

        // Act
        var act = () => new PolyLinearFunction(mesh).Bind(parameters);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Value_InvalidDimension_ShouldThrowArgumentException()
    {
        // Arrange
        var boundFunction = fixture.Bind();

        // Act
        var act = () => boundFunction.Value(new Vector { 0.5, 0.5 });

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Gradient_InvalidDimension_ShouldThrowArgumentException()
    {
        // Arrange
        var boundFunction = fixture.Bind();

        // Act
        Action act = () => boundFunction.Gradient(new Vector { 0.5, 0.5 });

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}