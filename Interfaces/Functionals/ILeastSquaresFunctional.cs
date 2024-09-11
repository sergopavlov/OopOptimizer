﻿using Interfaces.DataStorage;
using Interfaces.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Functionals;
public interface ILeastSquaresFunctional : IFunctional<IDifferentiableFunction>
{
    IVector Residual(IDifferentiableFunction function);
    IMatrix Jacobian(IDifferentiableFunction function);
}