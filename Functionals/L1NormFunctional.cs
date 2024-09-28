using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;

namespace Functionals;

/// <summary>
/// l1 норма разности с требуемыми значениями в наборе точек (реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
/// </summary>
public class L1NormFunctional : IDifferentiableFunctional
{
   private readonly IList<IVector> _points;
   private readonly IVector _values;

   public L1NormFunctional(IList<IVector> points, IVector targetValues)
   {
      DataValidator.ValidateDimensions(points, targetValues);

      _points = points;
      _values = targetValues;
   }

   public double Value(IDifferentiableFunction function)
      => _points
         .Select(function.Value)
         .Select((value, i) => Math.Abs(value - _values[i]))
         .Sum();

   public IVector Gradient(IDifferentiableFunction function)
   {
      int gradDim = function.Gradient(_points[0]).Count;
      var gradient = new Vector();
      gradient.AddRange(new double[gradDim]);

      for (int i = 0; i < _points.Count; i++)
      {
         double value = function.Value(_points[i]);
         double diff = value - _values[i];
         var funcGradient = function.Gradient(_points[i]);

         for (int j = 0; j < gradDim; j++)
         {
            gradient[j] += Math.Sign(diff) * funcGradient[j];
         }
      }

      return gradient;
   }
}