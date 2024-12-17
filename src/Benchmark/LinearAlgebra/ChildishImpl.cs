﻿// <copyright file="ChildishImpl.cs" company="Math.NET">
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
using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.LinearAlgebra
{
    public class ChildishImpl : LinearAlgebraTester<double[], double[,]>
    {
        public override void Setup()
        {
            TargetVector = new double[VectorSize];
            TargetVectorAdded = new double[VectorSize];
            TargetVectorMultiplied = new double[MatrixSize];
            TargetVectorMultiplied2 = new double[MatrixSize];
            TargetMatrix = new double[MatrixSize, MatrixSize];
            foreach (var i in Enumerable.Range(0, VectorSize))
            {
                TargetVector[i] = random.NextDouble();
                TargetVectorAdded[i] = random.NextDouble();
            }
            foreach (var i in Enumerable.Range(0, MatrixSize))
            {
                TargetVectorMultiplied[i] = random.NextDouble();
                foreach (var j in Enumerable.Range(0, MatrixSize))
                {
                    TargetMatrix[i, j] = random.NextDouble();
                }
            }
            added = random.NextDouble();
            base.Setup();
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        public override double[,] TestEigenValue()
        {
            throw new NotImplementedException();
        }

        public override double[,] TestLU()
        {
            throw new NotImplementedException();
        }

        [Benchmark]
        public override double[,] TestMatrixAddition()
        {
            foreach (var i in Enumerable.Range(0, MatrixSize))
            {
                foreach (var j in Enumerable.Range(0, MatrixSize))
                {
                    TargetMatrix[i, j] += added;
                }
            }
            return TargetMatrix;
        }

        [Benchmark]
        public override double[] TestMatrixMultipleVector()
        {
            foreach (var i in Enumerable.Range(0, MatrixSize))
            {
                double sum = 0;
                foreach (var j in Enumerable.Range(0, MatrixSize))
                {
                    sum += TargetMatrix[i, j] * TargetVectorMultiplied[j];
                }
                TargetVectorMultiplied2[i] = sum;
            }
            return TargetVectorMultiplied2;
        }

        [Benchmark]
        public override double[] TestVectorAddition()
        {
            for (int i = 0; i < VectorSize; i++)
            {
                TargetVector[i] += added;
            }
            return TargetVector;
        }

        [Benchmark]
        public override double TestVectorDotProduct()
        {
            double sum = 0;
            for (int i = 0; i < VectorSize; i++)
            {
                sum += TargetVector[i] * TargetVectorAdded[i];
            }
            return sum;
        }
    }
}
