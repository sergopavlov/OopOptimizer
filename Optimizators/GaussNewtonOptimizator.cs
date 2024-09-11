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
/// Алгоритм Гаусса-Ньютона
/// </summary>
public class GaussNewtonOptimizator : IOptimizator<ILeastSquaresFunctional, IDifferentiableFunction>
{
    public int Maxiter { get; set; } = 10000;
    public double TargetEps { get; set; } = 1e-12;
    public IVector Minimize(ILeastSquaresFunctional objective, IParametricFunction<IDifferentiableFunction> function, IVector initialParameters, IVector? minimumParameters = null, IVector? maximumParameters = null)
    {
        int n = initialParameters.Count;
        var currentParameters = new Vector();
        for (var i = 0; i < n; i++)
        {
            currentParameters.Add(initialParameters[i]);
        }
        int k = 0;
        var fun = function.Bind(currentParameters);
        var currentValue = objective.Value(fun);
        while (k < Maxiter && currentValue > TargetEps)
        {
            var jacobi = objective.Jacobian(fun);
            var jacobiT = Transpose(jacobi);
            var mat = MatMat(jacobiT, jacobi);
            var dparam = objective.Residual(fun);
            dparam = MatVec(jacobiT, dparam);
            dparam = SolveSLAE(mat, dparam);
            for (int i = 0; i < n; i++)
            {
                currentParameters[i] -= dparam[i];
            }
            fun = function.Bind(currentParameters);
            currentValue = objective.Value(fun);
            k++;
        }
        return currentParameters;
    }

    private IMatrix Transpose(IMatrix m)
    {
        throw new NotImplementedException();
    }
    private IMatrix MatMat(IMatrix m1, IMatrix m2)
    {

        throw new NotImplementedException();
    }
    private IVector MatVec(IMatrix m, IVector v)
    {
        throw new NotImplementedException();
    }
    private IVector SolveSLAE(IMatrix m, IVector rhs)
    {
        throw new NotImplementedException();
    }
}
