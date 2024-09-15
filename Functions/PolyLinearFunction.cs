using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;
/// <summary>
/// Кусочно-линейная функция (реализует IDifferentiableFunction)
/// </summary>
public class PolyLinearFunction : IParametricFunction<IDifferentiableFunction>
{
   class InternalPolyLinearFunction : IDifferentiableFunction
   {
      private IVector _parameters { get; set; }
      private IVector _mesh { get; set; }
      public InternalPolyLinearFunction(IVector parameters, IVector mesh)
      {
         if (parameters.Count != mesh.Count)
            throw new ArgumentException("Incorrect weights binded");
         _parameters = parameters;
         _mesh = mesh;
      }
      public IVector Gradient(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         if (point.Count != 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         try
         {
            int ind;
            (double x1, double x2, double y1, double y2) = FindInterval(point[0], out ind);
            Vector res = new Vector();
            res.AddRange(new double[_parameters.Count]);
            res[ind - 1] = (x2-point[0])/(x2-x1);
            res[ind]     = (point[0]-x1)/(x2-x1);
            return res;
         }
         catch
         {
            Vector res = new Vector();
            res.AddRange(new double[_parameters.Count]);
            if (point[0] < _parameters[0])
            {
               res[0] = 1;
               return res;
            }
            else //(point[^2]>_parameters[^1])
            {
               res[^1] = 1;
               return res;
            }
         };
      }
      /// <summary>
      /// f(x)=(y_{k}-y_{k-1})/(x_{k}-x_{k-1})*(x-x_{k-1}) + y_{k-1}
      /// </summary>
      /// <param name="point"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      public double Value(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         if (point.Count != 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         try
         {
            int ind;
            (double x1, double x2, double y1, double y2) = FindInterval(point[0], out ind);
            return y1 + (y2 - y1) / (x2 - x1) * (point[0] - x1);
         }
         catch
         {
            if (point[0] < _parameters[0])
               return _parameters[1];
            else //(point[^2]>_parameters[^1])
               return _parameters[^1];
         }
      }
      (double xk, double xk1, double yk, double yk1) FindInterval(double input, out int ind)
      {
         ind = Array.BinarySearch(_mesh.ToArray(), input);
         if (ind < 0)
            ind = ~ind;
         try
         {
            return (_mesh[ind - 1], _mesh[ind], _parameters[ind - 1], _parameters[ind]);
         }
         catch
         {
            throw new ArgumentException("input not inside mesh");
         }
      }
   }
   /// <summary>
   /// f(x)=(y_{k}-y_{k-1})/(x_{k}-x_{k-1})*(x-x_{k-1}) + y_{k-1}
   /// </summary>
   /// <param name="parameters">{y0, y1, ..., yn }</param>
   /// <returns></returns>
   /// <exception cref="ArgumentException"></exception>
   public IDifferentiableFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the linear function.");
      return new InternalPolyLinearFunction(parameters, _mesh);
   }
   private IVector _mesh { get; set; }
   public PolyLinearFunction(IVector mesh)
   {
      if (mesh.Count < 2)
         throw new ArgumentException("Incorrect mesh");
      _mesh = mesh;
   }
}
