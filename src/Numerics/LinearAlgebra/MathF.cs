// <copyright file="MathF.cs" company="Math.NET">
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


#if NET5_0_OR_GREATER
global using MathF = System.MathF;
#else
global using MathF = MathNet.Numerics.LinearAlgebra.MathF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathNet.Numerics.LinearAlgebra
{
    /// <summary>
    /// Wrapper class to System.MathF for .netfx 4.8 or .net standard 2.0.
    /// </summary>
    internal static class MathF
    {
        public const float PI = 3.14159274f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float x)
        {
            return Math.Abs(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float x)
        {
            return (float)Math.Acos(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float x)
        {
            return (float)Math.Cos(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IEEERemainder(float x, float y)
        {
            return (float)Math.IEEERemainder(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float x)
        {
            return (float)Math.Sin(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float x)
        {
            return (float)Math.Tan(x);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        public static float Exp(float x)
        {
            return (float)Math.Exp(x);
        }

        public static float Log(float x)
        {
            return (float)Math.Log(x);
        }
    }
}
#endif
