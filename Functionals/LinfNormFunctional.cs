using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;

namespace Functionals;

/// <summary>
/// linf норма разности с требуемыми значениями в наборе точек (не реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
/// </summary>
public class LinfNormFunctional : IFunctional<IFunction>
{
   private readonly IList<IVector> _points;
   private readonly IVector _values;

   public LinfNormFunctional(IList<IVector> points, IVector targetValues)
   {
      DataValidator.ValidateDimensions(points, targetValues);

      _points = points;
      _values = targetValues;
   }

    public double Value(IFunction function) 
       => _points
          .Select(function.Value)
          .Select((functionValue, i) => Math.Abs(functionValue - _values[i]))
          .Max();
}