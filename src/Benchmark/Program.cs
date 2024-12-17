using System;
using System.Linq;
using System.Reflection;
using Benchmark.LinearAlgebra;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using MathNet.Numerics;

namespace Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Control.Describe());

#if DEBUG
            var impl = new NumSharpImpl();
            impl.CommonSize = 8;
            impl.Setup();
            impl.TestVectorDotProduct();
#else
            var switcher = new BenchmarkSwitcher(
                new[]
                    {
                        typeof(ChildishImpl),
                        typeof(SpanImpl),
                        typeof(MathnetImpl),
                        typeof(NumSharpImpl),
                    });

            switcher.RunAll();
#endif
        }

        public static void CommonTest(string[] args)
        {
            var switcher = new BenchmarkSwitcher(
                new[]
                    {
                        typeof(Transforms.FFT),
                        typeof(LinearAlgebra.DenseMatrixProduct),
                        typeof(LinearAlgebra.DenseVector),
                    });

            switcher.Run(args);
        }
    }
}
