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
   private IVector _mesh { get; set; }
   class InternalCubicInterpolationHermitSplineFunction : IFunction
   {
      private IVector _mesh { get; set; }
      private IVector _parameters { get; set; }
      public InternalCubicInterpolationHermitSplineFunction(IVector parameters, IVector mesh)
      {
         if (parameters.Count != mesh.Count * 2)
            throw new ArgumentException("Incorrect weights binded");
         _parameters = parameters;
         _mesh = mesh;
      }
      double GetValueAtPoint(double point)
      {
         double x1, x2, y1, y2, q1, q2;
         try
         {
            (x1, x2, y1, y2, q1, q2) = FindIntervalHermit(point);
         }
         catch
         {
            if (point <= _mesh[0])
            {
               return _parameters[1] * (point - _mesh[0]) + _parameters[0];
            }
            else//point > _mesh[^1]
            {
               return _parameters[^1] * (point - _mesh[^1]) + _parameters[^2];
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
      (double xk, double xk1, double yk, double yk1, double qk, double qk1) FindIntervalHermit(double input)
      {
         int ind = Array.BinarySearch(_mesh.ToArray(), input);
         if (ind < 0)
            ind = ~ind;
         try
         {
            return (_mesh[ind - 1], _mesh[ind], _parameters[2*ind-2], _parameters[2*ind], _parameters[2*ind-1], _parameters[2*ind+1]);
         }
         catch
         {
            throw new ArgumentException("input not inside mesh");
         }

         throw new ArgumentException("input not inside mesh");
      }
   }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="parameters">{y0,y'0,y1,y'1,...,yn,y'n}</param>
   /// <returns></returns>
   /// <exception cref="ArgumentException"></exception>
   public IFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the linear function.");
      return new InternalCubicInterpolationHermitSplineFunction(parameters, _mesh);
   }
   public CubicInterpolationHermitSplineFunction(IVector mesh)
   {
      if (mesh.Count < 2)
         throw new ArgumentException("Incorrect mesh");
      _mesh = mesh;
   }
}
