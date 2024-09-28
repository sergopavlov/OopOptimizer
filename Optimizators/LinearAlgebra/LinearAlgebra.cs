using Interfaces.DataStorage;

namespace Optimizators.LinearAlgebra;

internal class LinearAlgebra : ILinearAlgebra
{
    public int SolverMaxIter { get; set; } = 10000;
    public double SolverEps { get; set; } = 1e-15;

    public IMatrix MatMat(IMatrix mat1, IMatrix mat2)
    {
        int n = mat1.Count;
        int m = mat1[0].Count;
        int l = mat2[0].Count;
        IMatrix res = new Matrix();
        for (int i = 0; i < n; i++)
        {
            res.Add(new List<double>());
            for (int j = 0; j < l; j++)
            {
                res[i].Add(0);
                for (int k = 0; k < m; k++)
                {
                    res[i][j] += mat1[i][k] * mat2[k][j];
                }
            }
        }
        return res;

    }
    public IVector MatVec(IMatrix mat, IVector vec)
    {
        int n = mat.Count;
        int m = vec.Count;
        IVector res = new Vector();
        for (int i = 0; i < n; i++)
        {
            res.Add(0);
            for (int j = 0; j < m; j++)
            {
                res[i] += mat[i][j] * vec[j];
            }
        }
        return res;
    }
    public IVector SolveSLAE(IMatrix m, IVector rhs)
    {
        int n = rhs.Count;

        double alpha, beta;
        double squareNorm;
        Vector q = [.. Enumerable.Repeat(0.0, n)];
        Vector z = new();
        IVector r = MatVec(m, q);
        for (int i = 0; i < n; i++)
        {
            r[i] = rhs[i] - r[i];
            z.Add(r[i]);
        }

        IVector p = MatVec(m, z);
        IVector tmp;

        squareNorm = VecVec(r, r);

        for (int index = 0; index < SolverMaxIter && squareNorm > SolverEps; index++)
        {
            alpha = VecVec(p, r) / VecVec(p, p);
            for (int i = 0; i < n; i++)
            {
                q[i] += alpha * z[i];
            }
            squareNorm = VecVec(r,r) - (alpha * alpha) * VecVec(p, p);
            for (int i = 0; i < n; i++)
            {
                r[i] -= alpha * p[i];
            }

            tmp = MatVec(m,r);

            beta = - VecVec(p , tmp) / VecVec(p , p);
            for (int i = 0; i < n; i++)
            {
                z[i] = r[i]+ beta * z[i];
                    p[i]= tmp[i]+beta * p[i];
            }
        }
        return q;
    }

    public IMatrix Transpose(IMatrix mat)
    {
        int n = mat.Count;
        int m = mat[0].Count;
        IMatrix res = new Matrix();
        for (int i = 0; i < m; i++)
        {
            res.Add(new List<double>());
            for (int j = 0; j < n; j++)
            {
                res[i].Add(mat[j][i]);
            }
        }
        return res;
    }

    public double VecVec(IVector vec1, IVector vec2)
    {
        double result = 0;
        for (int i = 0; i < vec1.Count; i++)
        {
            result += vec1[i] * vec2[i];
        }
        return result;
    }
}