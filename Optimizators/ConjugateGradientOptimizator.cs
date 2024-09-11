using Interfaces.DataStorage;
using Interfaces.Functionals;
using Interfaces.Functions;
using Interfaces.Optimizators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizators;
/// <summary>
/// метод сопряжённых градиентов
/// </summary>
public class ConjugateGradientOptimizator : IOptimizator<IDifferentiableFunctional, IDifferentiableFunction>
{
    public int Maxiter { get; set; } = 10000;
    public double TargetEps { get; set; } = 1e-12;
    public IVector Minimize(IDifferentiableFunctional objective, IParametricFunction<IDifferentiableFunction> function, IVector initialParameters, IVector? minimumParameters = null, IVector? maximumParameters = null)
    {
        int n = initialParameters.Count;
        var currentParams = new Vector();
        for (int i = 0; i < n; i++)
        {
            currentParams.Add(initialParameters[i]);
        }
        var currentFunction = function.Bind(currentParams);
        var grad = objective.Gradient(currentFunction);
        for (int i = 0; i < n; i++)
        {
            currentParams[i] -= grad[i];
        }

        currentFunction = function.Bind(currentParams);
        var currentValue = objective.Value(currentFunction);
        double lastGradNorm = Dot(grad, grad);
        var direction = new Vector();
        for (int i = 0; i < n; i++)
        {
            direction.Add(-grad[i]);
        }
        for (int i = 0; i < Maxiter && currentValue > TargetEps; i++)
        {
            grad = objective.Gradient(currentFunction);
            var curGradNorm = Dot(grad, grad);
            double beta = curGradNorm / lastGradNorm;
            for (int j = 0; j < n; j++)
            {
                direction[j] = -grad[j] + beta * direction[j];
                currentParams[j] += direction[j];
            }
            lastGradNorm = curGradNorm;
            currentFunction = function.Bind(currentParams);
            currentValue = objective.Value(currentFunction);
        }
        return currentParams;
    }
    private double Dot(IVector left, IVector right)
    {
        double result = 0;
        for (int i = 0; i < left.Count; i++)
        {
            result += left[i] * right[i];
        }
        return result;
    }
}
