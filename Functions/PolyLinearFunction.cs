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
      public InternalPolyLinearFunction(IVector parameters) 
      {
         if (parameters.Count % 2 != 0 && parameters.Count < 4)
            throw new ArgumentException("Incorrect mesh");
         _parameters = parameters;
      }
      public IVector Gradient(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         if (point.Count != 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         try
         {
            (double x1, double x2, double y1, double y2) = FindInterval(_parameters, point[0]);
            return new Vector() {(y2-y1) /(x2-x1)};
         }
         catch
         {
            if (point[0] < _parameters[0])
               return new Vector() { _parameters[1]};
            else //(point[^2]>_parameters[^1])
               return new Vector() { _parameters[^1]};
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
         if ( point.Count != 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         try 
         {
            (double x1, double x2, double y1, double y2) = FindInterval(_parameters, point[0]);
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
      (double xk, double xk1, double yk, double yk1) FindInterval(IVector data, double input)
      {
         for (int i = 0; i < data.Count - 2; i += 2)
         {
            double xk = data[i];
            double yk = data[i + 1];
            double xk1 = data[i + 2];
            double yk1 = data[i + 3];

            if (input >= xk && input <= xk1)
            {
               return (xk, xk1, yk, yk1);
            }
         }

         throw new ArgumentException("input not inside mesh");
      }
   }
   /// <summary>
   /// f(x)=(y_{k}-y_{k-1})/(x_{k}-x_{k-1})*(x-x_{k-1}) + y_{k-1}
   /// </summary>
   /// <param name="parameters">{x0, y0, x1, y1, ..., xn, yn }</param>
   /// <returns></returns>
   /// <exception cref="ArgumentException"></exception>
   public IDifferentiableFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the linear function.");
      return new InternalPolyLinearFunction(parameters);
   }
}
