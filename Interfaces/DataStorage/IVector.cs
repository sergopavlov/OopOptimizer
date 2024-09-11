using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataStorage;
public interface IVector : IList<double> { }
public class Vector : List<double>, IVector { }
