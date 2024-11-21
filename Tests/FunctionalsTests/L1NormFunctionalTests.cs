using FluentAssertions;
using Functionals;
using Functions;
using Interfaces.DataStorage;

namespace Tests.FunctionalsTests;

/// <summary>
/// Tests for L1 norm functional.
/// </summary>
/// <param name="linearFunction">Linear function.</param>
public sealed class L1NormFunctionalTests(LinearFunction linearFunction) : IClassFixture<LinearFunction>
{
    [Fact]
    public void Value_LinearFunctionWithNotExactValues_ShouldReturnCorrectNorm()
    {
        // Arrange
        const double expectedNorm = 2.0;
        var targetValues = new Vector { 2.0, 1.0, 4.0 };
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var function = linearFunction.Bind(new Vector { 1.0, 1.0 }); // f(x) = 1 + x
        var functional = new L1NormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // For the first point: |f(1) - 2| = |2 - 2| = 0
        // For the second point: |f(2) - 1| = |3 - 1| = 2
        // For the third point: |f(3) - 4| = |4 - 4| = 0
        result.Should().Be(expectedNorm);
    }

    [Fact]
    public void Value_LinearFunctionWithExactValues_ShouldReturnZero()
    {
        // Arrange
        const double expectedNorm = 0.0;
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 2.0, 3.0, 4.0 };
        var function = linearFunction.Bind(new Vector { 1.0, 1.0 }); // f(x) = 1 + x
        var functional = new L1NormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // For the first point: |f(1) - 2| = |2 - 2| = 0
        // For the second point: |f(2) - 3| = |3 - 3| = 0
        // For the third point: |f(3) - 4| = |4 - 4| = 0
        result.Should().Be(expectedNorm);
    }

    [Fact]
    public void Gradient_LinearFunctionWithExactValues_ShouldReturnZeroGradient()
    {
        // Arrange
        var expectedGradient = new Vector { 0.0, 0.0 };
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 2.0, 4.0, 6.0 };
        var function = linearFunction.Bind(new Vector { 0.0, 2.0 }); // f(x) = 2x
        var functional = new L1NormFunctional(points, targetValues);

        // Act
        var gradient = functional.Gradient(function);

        // Assert
        // For the first point: sign(f(1) - 2) * grad(f) = sign(2 - 2) * 2 = 0
        // For the second point: sign(f(2) - 4) * grad(f) = sign(4 - 4) * 2 = 0
        // For the third point: sign(f(3) - 6) * grad(f) = sign(6 - 6) * 2 = 0
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void ConstructorCall_EmptyPoints_ShouldThrowArgumentException()
    {
        // Arrange
        var points = new List<IVector>();
        var targetValues = new Vector { 2.0, 4.0, 6.0 };

        // Act
        Action act = () => _ = new L1NormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ConstructorCall_EmptyPoint_ShouldThrowArgumentException()
    {
        // Arrange
        List<IVector> points =
        [
            new Vector(),
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 2.0, 4.0, 6.0 };

        // Act
        Action act = () => _ = new L1NormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ConstructorCall_EmptyTargetValues_ShouldThrowArgumentException()
    {
        // Arrange
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector();

        // Act
        Action act = () => _ = new L1NormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ConstructorCall_DifferentDimensions_ShouldThrowArgumentException()
    {
        // Arrange
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0, 3.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 2.0, 4.0, 6.0 };

        // Act
        Action act = () => _ = new L1NormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}