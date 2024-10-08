using Interfaces.DataStorage;

namespace Optimizators.LinearAlgebra;
internal interface ILinearAlgebra
{
    public int SolverMaxIter { get; set; }
    public double SolverEps { get; set; }
    IMatrix MatMat(IMatrix mat1, IMatrix mat2);
    IVector MatVec(IMatrix mat, IVector vec);
    IVector SolveSLAE(IMatrix m, IVector rhs);
    IMatrix Transpose(IMatrix mat);
    double VecVec(IVector vec1, IVector vec2);
}