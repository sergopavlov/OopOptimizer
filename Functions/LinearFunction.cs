using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Functions;

/// <summary>
/// Линейная функция в n-мерном пространстве (число параметров n+1, реализует IDifferentiableFunction)
/// </summary>
public class LinearFunction : IParametricFunction<IDifferentiableFunction>
{

   class InternalLinearFunction : IDifferentiableFunction
   {
      private IVector _parameters { get; set; }
      public InternalLinearFunction(IVector parameters)
      {
         _parameters = parameters;
      }
      public IVector Gradient(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         return (IVector)_parameters.Skip(1).ToList();
      }
      /// <summary>
      /// f(x1,x2,...,xn) = a0+a1*x1+a2*x2+...+an*xn
      /// </summary>
      /// <param name="point"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      public double Value(IVector point)
      {
         if (_parameters.Count < 1)
            throw new ArgumentException("Parameters are not bound. Please call the 'bind' method first.");
         if (_parameters.Count != point.Count + 1)
            throw new ArgumentException("The dimensionality of the space does not match the type of function.");
         return _parameters[0] + _parameters.Skip(1).Zip(point, (a, b) => a * b).Sum();
      }
   }

   /// <summary>
   /// f(x1,x2,...,xn) = a0+a1*x1+a2*x2+...+an*xn
   /// </summary>
   /// <param name="parameters">{a0, a1,a2,..., an}</param>
   /// <returns></returns>
   public IDifferentiableFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the linear function.");
      return new InternalLinearFunction(parameters);
   }


}
