using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Functions;
/// <summary>
/// Сплайн (не линейный)
/// </summary>
public class CubicInterpolationHermitSplineFunction : IParametricFunction<IFunction>
{
   class InternalCubicInterpolationHermitSplineFunction : IFunction
   {
      private IVector _parameters { get; set; }
      public InternalCubicInterpolationHermitSplineFunction(IVector parameters)
      {
         if (parameters.Count % 3 != 0 && parameters.Count<6)
            throw new ArgumentException("Incorrect mesh");
         _parameters = parameters;
      }
      double GetValueAtPoint(double point)
      {
         double x1, x2, y1, y2, q1, q2;
         try
         {
            (x1, x2, y1, y2, q1, q2) = FindIntervalHermit(_parameters, point);
         }
         catch
         {
            if (point < _parameters[0])
            {
               return _parameters[2] * point + _parameters[1] - _parameters[2] * _parameters[0];
            }
            else//point > _parameters[^3]
            {
               return _parameters[^1] * point + _parameters[^2] - _parameters[^1] * _parameters[^3];
            }
         }
         double res = 0;
         double step = x2 - x1;
         double ksi = (point - x1) / step;
         res += (1 - 3 * ksi * ksi + 2 * ksi * ksi * ksi) * y1;
         res += (ksi - 2 * ksi * ksi + ksi * ksi * ksi) * q1 * step;
         res += (3 * ksi * ksi - 2 * ksi * ksi * ksi) * y2;
         res += (-ksi * ksi + ksi * ksi * ksi) * q2 * step;
         return res;
      }
      public double Value(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         if (point.Count != 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         return GetValueAtPoint(point[0]);
      }
      (double xk, double xk1, double yk, double yk1, double qk, double qk1) FindIntervalHermit(IVector data, double input)
      {
         for (int i = 0; i < data.Count - 3; i += 3)
         {
            double xk = data[i];
            double yk = data[i + 1];
            double qk = data[i + 2];
            double xk1 = data[i + 3];
            double yk1 = data[i + 4];
            double qk1 = data[i + 5];

            if (input >= xk && input <= xk1)
            {
               return (xk, xk1, yk, yk1, qk, qk1);
            }
         }

         throw new ArgumentException("input not inside mesh");
      }
   }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="parameters">{x0,y0,y'0,x1,y1,y'1,...,xn,yn,y'n}</param>
   /// <returns></returns>
   /// <exception cref="ArgumentException"></exception>
   public IFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the linear function.");
      return new InternalCubicInterpolationHermitSplineFunction(parameters);
   }
}
