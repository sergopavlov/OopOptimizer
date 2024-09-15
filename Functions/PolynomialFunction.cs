using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions;
/// <summary>
/// Полином n-й степени в одномерном пространстве (число параметров n+1, не реализует IDifferentiableFunction)
/// </summary>
public class PolynomialFunction : IParametricFunction<IFunction>
{
   class InternalPolynomialFunction:IFunction
   {
      private IVector _parameters { get; set; }
      public InternalPolynomialFunction(IVector parameters)
      {
         _parameters = parameters;
      }
      /// <summary>
      /// f(x)=a0+a1*x+a2*x^2+...+an*x^n
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
         return _parameters.Select((coeff, index) => coeff * Math.Pow(point[0], index)).Sum();
      }
   }
   /// <summary>
   /// f(x)=a0+a1*x+a2*x^2+...+an*x^n
   /// </summary>
   /// <param name="parameters">a</param>
   /// <returns></returns>
   public IFunction Bind(IVector parameters)
   {
      if (parameters.Count < 1)
         throw new ArgumentException("Empty list of coefficients. Please provide coefficients for the polynomial function.");

      return new InternalPolynomialFunction(parameters);
   }
}
