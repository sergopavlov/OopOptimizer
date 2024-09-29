using Interfaces.DataStorage;

namespace Tests.DataGeneration;

/// <summary>
/// Generates test data for the tests.
/// </summary>
public static class TestDataGenerator
{
    /// <summary>
    /// Contains data for testing the Value method of the LinearFunction class (parameters, points, expected).
    /// </summary>
    /// <returns></returns>
    public static TheoryData<IVector, IVector, double> GetLinearFunctionValueData() =>
        new()
        {
            { new Vector { 2.0, 3.0 }, new Vector { 4.0 }, 14.0 },
            { new Vector { 1.0, 2.0, 3.0 }, new Vector { 4.0, 5.0 }, 24.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0 }, new Vector { 4.0, 5.0, 6.0 }, 48.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0, 5.0 }, new Vector { 4.0, 5.0, 6.0, 7.0 }, 83.0 }
        };

    /// <summary>
    /// Contains data for testing the Gradient method of the LinearFunction class (parameters, points, expected).
    /// </summary>
    /// <returns></returns>
    public static TheoryData<IVector, IVector, IVector> GetLinearFunctionGradientData() =>
        new()
        {
            { new Vector { 2.0, 3.0 }, new Vector { 4.0 }, new Vector { 1.0, 4.0 } },
            { new Vector { 1.0, 2.0, 3.0 }, new Vector { 4.0, 5.0 }, new Vector { 1.0, 4.0, 5.0 } },
            { new Vector { 1.0, 2.0, 3.0, 4.0 }, new Vector { 4.0, 5.0, 6.0 }, new Vector { 1.0, 4.0, 5.0, 6.0 } },
            { new Vector { 1.0, 2.0, 3.0, 4.0, 5.0 }, new Vector { 4.0, 5.0, 6.0, 7.0 }, new Vector { 1.0, 4.0, 5.0, 6.0, 7.0 } }
        };

    /// <summary>
    /// Contains data for testing the Value method of the PolynomialFunction class (parameters, points, expected).
    /// </summary>
    /// <returns></returns>
    public static TheoryData<IVector, IVector, double> GetPolynomialFunctionValueData() =>
        new()
        {
            { new Vector { 1.0, 2.0 }, new Vector { 1.0 }, 3.0 },
            { new Vector { 1.0, 2.0, 3.0 }, new Vector { 2.0 }, 17.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0 }, new Vector { 3.0 }, 142.0 },
            { new Vector { 1.0, 2.0, 3.0, 4.0, 5.0 }, new Vector { 4.0 }, 1593.0 }
        };
}