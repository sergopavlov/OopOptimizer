using FluentAssertions;
using Functionals;
using Functions;
using Interfaces.DataStorage;

namespace Tests.FunctionalsTests;

/// <summary>
/// Tests for Linf norm functional.
/// </summary>
/// <param name="polynomialFunction">Polynomial function.</param>
public sealed class LinfNormFunctionalTests(PolynomialFunction polynomialFunction) : IClassFixture<PolynomialFunction>
{
    [Fact]
    public void Value_QuadraticFunctionWithNotExactValues_ShouldReturnCorrectNorm()
    {
        // Arrange
        const double expectedNorm = 1.0;
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 2.0, 5.0, 10.0 };
        var function = polynomialFunction.Bind(new Vector { 0.0, 0.0, 1.0 }); // f(x) = x^2
        var functional = new LinfNormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // Differences: |1 - 2| = 1, |4 - 5| = 1, |9 - 10| = 1
        // Maximum difference: 1
        expectedNorm.Should().Be(result);
    }

    [Fact]
    public void Value_QuadraticFunctionWithExactValues_ShouldReturnZero()
    {
        // Arrange
        const double expectedNorm = 0.0;
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var targetValues = new Vector { 1.0, 4.0, 9.0 };
        var function = polynomialFunction.Bind(new Vector { 0.0, 0.0, 1.0 }); // f(x) = x^2
        var functional = new LinfNormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // Differences: |1 - 1| = 0, |4 - 4| = 0, |9 - 9| = 0
        // Maximum difference: 0
        expectedNorm.Should().Be(result);
    }

    [Fact]
    public void ConstructorCall_EmptyPoints_ShouldThrowArgumentException()
    {
        // Arrange
        var points = new List<IVector>();
        var targetValues = new Vector { 2.0, 4.0, 6.0 };

        // Act
        Action act = () => _ = new LinfNormFunctional(points, targetValues);

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
        Action act = () => _ = new LinfNormFunctional(points, targetValues);

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
        Action act = () => _ = new LinfNormFunctional(points, targetValues);

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
        Action act = () => _ = new LinfNormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}