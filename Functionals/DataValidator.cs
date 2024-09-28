using Interfaces.DataStorage;

namespace Functionals;

internal static class DataValidator
{
   public static void ValidateDimensions(IList<IVector> points, IVector values)
   {
      if (points.Count == 0)
      {
         throw new ArgumentException("The array must contain at least one point");
      }

      if (points.Count != values.Count)
      {
         throw new ArgumentException($"Dimension mismatch in {nameof(points)} and {nameof(values)}");
      }

      var firstPoint = points[0];

      if (firstPoint.Count == 0)
      {
         throw new ArgumentException("The point must contain at least one coordinate");
      }

      if (points.Any(p => p.Count != firstPoint.Count))
      {
         throw new ArgumentException("The points have different dimensions");
      }
   }
}