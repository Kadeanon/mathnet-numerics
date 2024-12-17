// <copyright file="MathnetImpl.cs" company="Math.NET">
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
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.LinearAlgebra
{
    public class MathnetImpl : LinearAlgebraTester<MathNet.Numerics.LinearAlgebra.Vector<double>, MathNet.Numerics.LinearAlgebra.Matrix<double>>
    {
        public override void Setup()
        {
            TargetVector = Vector<double>.Build.Random(VectorSize);
            TargetVectorAdded = Vector<double>.Build.Random(VectorSize);
            TargetVectorMultiplied = Vector<double>.Build.Random(MatrixSize);
            TargetVectorMultiplied2 = Vector<double>.Build.Random(MatrixSize);
            TargetMatrix = Matrix<double>.Build.Random(MatrixSize, MatrixSize);
            added = random.NextDouble();
            base.Setup();
        }

        public override Matrix<double> TestEigenValue()
        {
            throw new NotImplementedException();
        }

        public override Matrix<double> TestLU()
        {
            throw new NotImplementedException();
        }

        [Benchmark]
        public override Matrix<double> TestMatrixAddition()
        {
            return TargetMatrix.Add(added);
        }

        [Benchmark]
        public override Vector<double> TestMatrixMultipleVector()
        {
            return TargetMatrix.Multiply(TargetVectorMultiplied);
        }

        [Benchmark]
        public override Vector<double> TestVectorAddition()
        {
            return TargetVector.Add(TargetVectorAdded);
        }

        [Benchmark]
        public override double TestVectorDotProduct()
        {
            return TargetVector.DotProduct(TargetVectorAdded);
        }
    }
}
