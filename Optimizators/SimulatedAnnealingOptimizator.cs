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
/// алгоритм имитации отжига
/// </summary>
public class SimulatedAnnealingOptimizator : IOptimizator<IFunctional<IFunction>, IFunction>
{
    private static readonly Random random = new Random(Environment.TickCount);
    int maxiter;
    Func<int, double> temperature;
    Func<double, double, double, double> acceptanceProbability;

    public SimulatedAnnealingOptimizator(int maxiter, Func<int, double> temperature, Func<double, double, double, double> acceptanceProbability)
    {
        this.maxiter = maxiter;
        this.temperature = temperature;
        this.acceptanceProbability = acceptanceProbability;
    }

    public SimulatedAnnealingOptimizator(int maxiter)
    {
        this.maxiter = maxiter;
        this.temperature = (i)=> 1.0-(1.0+i)/maxiter;
        this.acceptanceProbability = (c,r,t)=>Math.Exp((c-r)/t);
    }

    public IVector Minimize(IFunctional<IFunction> objective,
                            IParametricFunction<IFunction> function,
                            IVector initialParameters,
                            IVector? minimumParameters = null,
                            IVector? maximumParameters = null)
    {
        IVector curparameters = new Vector();
        foreach (var item in initialParameters)
        {
            curparameters.Add(item);
        }
        double curenergy = objective.Value(function.Bind(curparameters));
        for (int i = 0; i < maxiter; i++)
        {
            double t = temperature(i);
            IVector randparameters = GetRandomVector(minimumParameters, maximumParameters, initialParameters.Count);
            double randenergy = objective.Value(function.Bind(randparameters));
            if (acceptanceProbability(curenergy, randenergy, t) > random.NextDouble())
            {
                curparameters = randparameters;
                curenergy = randenergy;
            }
        }
        return curparameters;
    }
    private IVector GetRandomVector(IVector? minimumParameters,
                            IVector? maximumParameters, int n)
    {
        if (minimumParameters == null || maximumParameters == null)
        {
            IVector res = new Vector();
            for (int i = 0; i < n; i++)
            {
                res.Add(random.NextDouble());
            }
            return res;
        }
        else
        {
            IVector res = new Vector();
            for (int i = 0; i < n; i++)
            {
                res.Add(minimumParameters[i] + random.NextDouble() * (maximumParameters[i] - minimumParameters[i]));
            }
            return res;
        }
    }
}
