using FluentAssertions;
using Functionals;
using Functions;
using Interfaces.DataStorage;

namespace Tests.FunctionalsTests;

/// <summary>
/// Tests for L2 norm functional.
/// </summary>
/// <param name="linearFunction">Linear function.</param>
public sealed class L2NormFunctionalTests(LinearFunction linearFunction) : IClassFixture<LinearFunction>
{
    [Fact]
    public void Value_LinearFunctionWithNotExactValues_ShouldReturnCorrectNorm()
    {
        // Arrange
        const double expectedNorm = 4.0;
        var targetValues = new Vector { 2.0, 1.0, 4.0 };
        List<IVector> points =
        [
            new Vector { 1.0 },
            new Vector { 2.0 },
            new Vector { 3.0 }
        ];
        var function = linearFunction.Bind(new Vector { 1.0, 1.0 }); // f(x) = 1 + x
        var functional = new L2NormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // For the first point: (f(1) - 2)^2 = (2 - 2)^2 = 0
        // For the second point: (f(2) - 1)^2 = (3 - 1)^2 = 4
        // For the third point: (f(3) - 4)^2 = (4 - 4)^2 = 0
        expectedNorm.Should().Be(result);
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
        var functional = new L2NormFunctional(points, targetValues);

        // Act
        var result = functional.Value(function);

        // Assert
        // For the first point: (f(1) - 2)^2 = (2 - 2)^2 = 0
        // For the second point: (f(2) - 3)^2 = (3 - 3)^2 = 0
        // For the third point: (f(3) - 4)^2 = (4 - 4)^2 = 0
        expectedNorm.Should().Be(result);
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
        var functional = new L2NormFunctional(points, targetValues);

        // Act
        var gradient = functional.Gradient(function);

        // Assert
        // For each point the difference (f(x) - y) will be 0, so the gradient will be 0
        gradient.Should().BeEquivalentTo(expectedGradient);
    }

    [Fact]
    public void ConstructorCall_EmptyPoints_ShouldThrowArgumentException()
    {
        // Arrange
        var points = new List<IVector>();
        var targetValues = new Vector { 2.0, 4.0, 6.0 };

        // Act
        Action act = () => _ = new L2NormFunctional(points, targetValues);

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
        Action act = () => _ = new L2NormFunctional(points, targetValues);

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
        Action act = () => _ = new L2NormFunctional(points, targetValues);

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
        Action act = () => _ = new L2NormFunctional(points, targetValues);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}