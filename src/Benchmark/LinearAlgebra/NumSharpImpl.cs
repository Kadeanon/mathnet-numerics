// <copyright file="NDSharp.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// https://numerics.mathdotnet.com
// https://github.com/mathnet/mathnet-numerics
//
// Copyright (c) 2009-$CURRENT_YEAR$ Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

using BenchmarkDotNet.Attributes;
using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.LinearAlgebra
{
    public class NumSharpImpl:LinearAlgebraTester<NDArray, NDArray>
    {
        public override void Setup()
        {
            TargetVector = np.random.rand(VectorSize);
            TargetVectorAdded = np.random.rand(VectorSize);
            TargetVectorMultiplied = np.random.rand(MatrixSize);
            TargetVectorMultiplied2 = np.random.rand(MatrixSize);
            TargetMatrix = np.random.rand(MatrixSize, MatrixSize);
            added = random.NextDouble();
            base.Setup();
        }

        public override NDArray TestEigenValue()
        {
            throw new NotImplementedException();
        }

        public override NDArray TestLU()
        {
            throw new NotImplementedException();
        }

        [Benchmark]
        public override NDArray TestMatrixAddition()
        {
            return TargetMatrix + added;
        }

        [Benchmark]
        public override NDArray TestMatrixMultipleVector()
        {
            return np.matmul(TargetMatrix, TargetVectorMultiplied);
        }

        [Benchmark]
        public override NDArray TestVectorAddition()
        {
            return TargetVector + TargetVectorAdded;
        }

        [Benchmark]
        public override double TestVectorDotProduct()
        {
            // it seem not to work
            return np.multiply(TargetVector, TargetVectorAdded);
        }
    }
}
