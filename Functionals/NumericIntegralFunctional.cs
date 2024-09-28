using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;

namespace Functionals;

/// <summary>
/// Интеграл по некоторой области (численно)
/// </summary>
public class NumericIntegralFunctional : IFunctional<IFunction>
{
   private readonly IVector _lower;
   private readonly IVector _upper;
   private readonly double[] _gaussNodes = [0.0, Math.Sqrt(3.0 / 5.0), -Math.Sqrt(3.0 / 5.0)];
   private readonly double[] _gaussWeights = [8.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0];

   public NumericIntegralFunctional(IVector lower, IVector upper)
   {
      if (lower.Count != upper.Count)
      {
         throw new ArgumentException($"Dimension mismatch in {nameof(lower)} and {nameof(upper)}");
      }

      _lower = lower;
      _upper = upper;
   }

    public double Value(IFunction function)
    {
       double result = 0.0;
       int dim = _lower.Count;

       var stack = new Stack<(int depth, double currentProduct, double[] points)>();
       var points = new double[dim];
       stack.Push((0, 1.0, points));

       while (stack.Count > 0)
       {
          var (depth, currentProduct, currentPoints) = stack.Pop();

          if (depth == dim)
          {
             var p = new Vector();
             p.AddRange(currentPoints);
             result += currentProduct * function.Value(p);
             continue;
          }

          for (int i = 0; i < 3; i++)
          {
             double node = _gaussNodes[i];
             double weight = _gaussWeights[i];
             double point = 0.5 * ((_upper[depth] - _lower[depth]) * node + _upper[depth] + _lower[depth]);

             currentPoints[depth] = point;
             double newProduct = currentProduct * weight;
             stack.Push((depth + 1, newProduct, (double[])currentPoints.Clone()));
          }
       }

       double volume = 1.0;

       for (int i = 0; i < dim; i++)
       {
          volume *= Math.Abs(_upper[i] - _lower[i]);
       }

       return result * volume / Math.Pow(2, dim);
    }
}