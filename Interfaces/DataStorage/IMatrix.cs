using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataStorage;
public interface IMatrix : IList<IList<double>> { }
public class Matrix : List<IList<double>>, IMatrix { }
