using Interfaces.DataStorage;

namespace Optimizators.LinearAlgebra;

internal class LinearAlgebra : ILinearAlgebra
{
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
        throw new NotImplementedException();
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