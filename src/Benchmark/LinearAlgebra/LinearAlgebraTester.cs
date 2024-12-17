// <copyright file="LinearAlgebraTester.cs" company="Math.NET">
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
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.LinearAlgebra
{
    public class LinearAlgebraConfig : ManualConfig
    {
        public LinearAlgebraConfig()
        {
            AddJob(Job.Default.WithRuntime(CoreRuntime.Core80).WithPlatform(Platform.X64).WithJit(Jit.RyuJit));
        }

        
    }
    public abstract class LinearAlgebraTester<TVector, TMatrix>
    {

        [Params(8, 32, 128)]
        public int CommonSize { get; set; }


        public int VectorSize => CommonSize * 512;

        public int MatrixSize => CommonSize * 8;

        protected TVector TargetVector;
        protected TVector TargetVectorAdded;
        protected TVector TargetVectorMultiplied;
        protected TVector TargetVectorMultiplied2;
        protected TMatrix TargetMatrix;
        protected SystemRandomSource random = new SystemRandomSource();
        protected double added;

        [GlobalSetup]
        public virtual void Setup()
        {
        }



        [GlobalCleanup]
        public virtual void Cleanup()
        {
        }

        [Benchmark]
        public abstract TVector TestVectorAddition();

        //[Benchmark]
        public abstract double TestVectorDotProduct();

        //[Benchmark]
        public abstract TMatrix TestMatrixAddition();

        //[Benchmark]
        public abstract TMatrix TestLU();

        //[Benchmark]
        public abstract TVector TestMatrixMultipleVector();

        //[Benchmark]
        public abstract TMatrix TestEigenValue();
    }
}
