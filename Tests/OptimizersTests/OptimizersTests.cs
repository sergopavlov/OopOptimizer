using FluentAssertions;
using FluentAssertions.Equivalency;
using Functionals;
using Functions;
using Interfaces.DataStorage;
using Optimizators;

namespace Tests.OptimizersTests;

public sealed class OptimizerTests(LinearFunction linearFunction) : IClassFixture<LinearFunction>
{
    private static EquivalencyOptions<double> ConfigureEquivalencyOptions(EquivalencyOptions<double> options,
        double eps) =>
        options.Using<double>(ctx =>
            ctx.Subject.Should().BeApproximately(ctx.Expectation, eps)).WhenTypeIs<double>();

    [Fact]
    public void Minimize_GaussNewtonOptimizer_ShouldReturnCorrectResult()
    {
        // Arrange
        var expectedParameters = new Vector { 1.0, 1.0 };
        var objective = new L2NormFunctional(
            points: new List<IVector> { new Vector { 5.0 }, new Vector { 6.0 } },
            targetValues: new Vector { 6.0, 7.0 });
        var functionParameters = new Vector { 2.0, 3.0 };
        var optimizer = new GaussNewtonOptimizator();

        // Act
        var result = optimizer.Minimize(objective, linearFunction, functionParameters);

        // Assert
        result
            .Should()
            .BeEquivalentTo(expectedParameters, options => ConfigureEquivalencyOptions(options, optimizer.TargetEps));
    }

    [Fact]
    public void Minimize_ConjugateGradientOptimizer_ShouldReturnCorrectResult()
    {
        // Arrange
        const double eps = 1E-05;
        var expectedParameters = new Vector { 1.0, 2.0 };
        var objective = new L2NormFunctional(
            points: new List<IVector> { new Vector { 3.0 }, new Vector { 4.0 } },
            targetValues: new Vector { 7.0, 9.0 });
        var functionParameters = new Vector { 2.0, 3.0 };
        var optimizer = new ConjugateGradientOptimizator();

        // Act
        var result = optimizer.Minimize(objective, linearFunction, functionParameters);

        // Assert
        result
            .Should()
            .BeEquivalentTo(expectedParameters, options => ConfigureEquivalencyOptions(options, eps));
    }

    // [Fact] // test no need because not deterministic
    // public void Minimize_SimulatedAnnealingOptimizer_ShouldReturnCorrectResult()
    // {
    //     // Arrange
    //     const double eps = 0.1;
    //     const int maxIter = 100000;
    //     var expectedParameters = new Vector { 0.0, 1.0 };
    //     var objective = new LinfNormFunctional(
    //         points: new List<IVector> { new Vector { 5.0 }, new Vector { 6.0 } },
    //         targetValues: new Vector { 5.0, 6.0 });
    //     var functionParameters = new Vector { 0.0, 1.0 };
    //     var optimizer = new SimulatedAnnealingOptimizator(maxIter);
    //
    //     // Act
    //     var result = optimizer.Minimize(objective, linearFunction, functionParameters);
    //
    //     // Assert
    //     result
    //         .Should()
    //         .BeEquivalentTo(expectedParameters,
    //             options => ConfigureEquivalencyOptions(options, eps));
    // }
}