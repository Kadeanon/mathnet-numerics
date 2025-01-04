// <copyright file="Complex32.INumberBase.cs" company="Math.NET">
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MathNet.Numerics
{
#if NET7_0_OR_GREATER
    public readonly partial struct Complex32 : INumberBase<Complex32>
    {


        /// <inheritdoc cref="INumberBase{TSelf}.Radix" />
        public static int Radix => 2;

        /// <inheritdoc cref="IAdditiveIdentity{TSelf, TResult}.AdditiveIdentity"/>
        public static Complex32 AdditiveIdentity => new Complex32(0.0f, 0.0f);

        /// <inheritdoc cref="IMultiplicativeIdentity{TSelf, TResult}.MultiplicativeIdentity"/>
        public static Complex32 MultiplicativeIdentity => new Complex32(1.0f, 0.0f);

        /// <inheritdoc cref="INumberBase{TSelf}.One" />
        static Complex32 INumberBase<Complex32>.One => new Complex32(1.0f, 0.0f);

        /// <inheritdoc cref="INumberBase{TSelf}.Zero" />
        static Complex32 INumberBase<Complex32>.Zero => new Complex32(0.0f, 0.0f);

        /// <inheritdoc cref="INumberBase{TSelf}.IsCanonical(TSelf)" />
        public static bool IsCanonical(Complex32 value) => true;

        /// <inheritdoc cref="INumberBase{TSelf}.IsComplexNumber(TSelf)" />
        public static bool IsComplexNumber(Complex32 value) => (value._imag != 0.0f) && (value._imag != 0.0f);

        /// <inheritdoc cref="INumberBase{TSelf}.IsEvenInteger(TSelf)" />
        public static bool IsEvenInteger(Complex32 value) =>
            value._imag == 0.0f && float.IsEvenInteger(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsFinite(TSelf)" />
        public static bool IsFinite(Complex32 value) =>
            float.IsFinite(value._real) && float.IsFinite(value._imag);

        /// <inheritdoc cref="INumberBase{TSelf}.IsImaginaryNumber(TSelf)" />
        public static bool IsImaginaryNumber(Complex32 value) => value._real == 0.0f && float.IsRealNumber(value._imag);

        /// <inheritdoc cref="INumberBase{TSelf}.IsInfinity(TSelf)" />
        public static bool IsInfinity(Complex32 value) =>
            float.IsInfinity(value._real) || float.IsInfinity(value._imag);

        /// <inheritdoc cref="INumberBase{TSelf}.IsInteger(TSelf)" />
        public static bool IsInteger(Complex32 value) =>
            value._imag == 0.0f && float.IsInteger(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsNaN(TSelf)" />
        public static bool IsNaN(Complex32 value) =>
            !IsInfinity(value) && !IsFinite(value);

        /// <inheritdoc cref="INumberBase{TSelf}.IsNegative(TSelf)" />
        public static bool IsNegative(Complex32 value) =>
            (value._imag == 0.0f) &&
            float.IsNegative(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsNegativeInfinity(TSelf)" />
        public static bool IsNegativeInfinity(Complex32 value) => (value._imag == 0.0f) &&
            float.IsNegativeInfinity(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsNormal(TSelf)" />
        public static bool IsNormal(Complex32 value)
            => float.IsNormal(value._real) &&
            ((value._imag == 0.0f) || float.IsNormal(value._imag));

        /// <inheritdoc cref="INumberBase{TSelf}.IsOddInteger(TSelf)" />
        public static bool IsOddInteger(Complex32 value) => (value._imag == 0.0f) && float.IsOddInteger(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsPositive(TSelf)" />
        public static bool IsPositive(Complex32 value) => (value._imag == 0.0f) && float.IsPositive(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsPositiveInfinity(TSelf)" />
        public static bool IsPositiveInfinity(Complex32 value) =>
            (value._imag == 0.0f) &&
            float.IsPositiveInfinity(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsRealNumber(TSelf)" />
        public static bool IsRealNumber(Complex32 value) =>
            value._imag == 0.0f &&float.IsRealNumber(value._real);

        /// <inheritdoc cref="INumberBase{TSelf}.IsSubnormal(TSelf)" />
        public static bool IsSubnormal(Complex32 value) => float.IsSubnormal(value._real) ||
            float.IsSubnormal(value._imag);

        /// <inheritdoc cref="INumberBase{TSelf}.IsZero(TSelf)" />
        public static bool IsZero(Complex32 value) => value._real == 0.0f && value._imag == 0.0f;

        /// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitude(TSelf, TSelf)" />
        public static Complex32 MaxMagnitude(Complex32 x, Complex32 y)
        {
            // complex numbers are not normally comparable, however every complex
            // number has a real magnitude (absolute value) and so we can provide
            // an implementation for MaxMagnitude

            // This matches the IEEE 754:2019 `maximumMagnitude` function
            //
            // It propagates NaN inputs back to the caller and
            // otherwise returns the input with a larger magnitude.
            // It treats +0 as larger than -0 as per the specification.

            double ax = Abs(x);
            double ay = Abs(y);

            if ((ax > ay) || double.IsNaN(ax))
            {
                return x;
            }

            if (ax == ay)
            {
                // We have two equal magnitudes which means we have two of the following
                //   `+a + ib`
                //   `-a + ib`
                //   `+a - ib`
                //   `-a - ib`
                //
                // We want to treat `+a + ib` as greater than everything and `-a - ib` as
                // lesser. For `-a + ib` and `+a - ib` its "ambiguous" which should be preferred
                // so we will just preference `+a - ib` since that's the most correct choice
                // in the face of something like `+a - i0.0` vs `-a + i0.0`. This is the "most
                // correct" choice because both represent real numbers and `+a` is preferred
                // over `-a`.

                if (float.IsNegative(y._real))
                {
                    if (float.IsNegative(y._imag))
                    {
                        // when `y` is `-a - ib` we always prefer `x` (its either the same as
                        // `x` or some part of `x` is positive).

                        return x;
                    }
                    else
                    {
                        if (float.IsNegative(x._real))
                        {
                            // when `y` is `-a + ib` and `x` is `-a + ib` or `-a - ib` then
                            // we either have same value or both parts of `x` are negative
                            // and we want to prefer `y`.

                            return y;
                        }
                        else
                        {
                            // when `y` is `-a + ib` and `x` is `+a + ib` or `+a - ib` then
                            // we want to prefer `x` because either both parts are positive
                            // or we want to prefer `+a - ib` due to how it handles when `x`
                            // represents a real number.

                            return x;
                        }
                    }
                }
                else if (float.IsNegative(y._imag))
                {
                    if (float.IsNegative(x._real))
                    {
                        // when `y` is `+a - ib` and `x` is `-a + ib` or `-a - ib` then
                        // we either both parts of `x` are negative or we want to prefer
                        // `+a - ib` due to how it handles when `y` represents a real number.

                        return y;
                    }
                    else
                    {
                        // when `y` is `+a - ib` and `x` is `+a + ib` or `+a - ib` then
                        // we want to prefer `x` because either both parts are positive
                        // or they represent the same value.

                        return x;
                    }
                }
            }

            return y;
        }

        /// <inheritdoc cref="INumberBase{TSelf}.MaxMagnitudeNumber(TSelf, TSelf)" />
        public static Complex32 MaxMagnitudeNumber(Complex32 x, Complex32 y)
        {
            // complex numbers are not normally comparable, however every complex
            // number has a real magnitude (absolute value) and so we can provide
            // an implementation for MaxMagnitudeNumber

            // This matches the IEEE 754:2019 `maximumMagnitudeNumber` function
            //
            // It does not propagate NaN inputs back to the caller and
            // otherwise returns the input with a larger magnitude.
            // It treats +0 as larger than -0 as per the specification.

            double ax = Abs(x);
            double ay = Abs(y);

            if ((ax > ay) || double.IsNaN(ay))
            {
                return x;
            }

            if (ax == ay)
            {
                // We have two equal magnitudes which means we have two of the following
                //   `+a + ib`
                //   `-a + ib`
                //   `+a - ib`
                //   `-a - ib`
                //
                // We want to treat `+a + ib` as greater than everything and `-a - ib` as
                // lesser. For `-a + ib` and `+a - ib` its "ambiguous" which should be preferred
                // so we will just preference `+a - ib` since that's the most correct choice
                // in the face of something like `+a - i0.0` vs `-a + i0.0`. This is the "most
                // correct" choice because both represent real numbers and `+a` is preferred
                // over `-a`.

                if (float.IsNegative(y._real))
                {
                    if (float.IsNegative(y._imag))
                    {
                        // when `y` is `-a - ib` we always prefer `x` (its either the same as
                        // `x` or some part of `x` is positive).

                        return x;
                    }
                    else
                    {
                        if (float.IsNegative(x._real))
                        {
                            // when `y` is `-a + ib` and `x` is `-a + ib` or `-a - ib` then
                            // we either have same value or both parts of `x` are negative
                            // and we want to prefer `y`.

                            return y;
                        }
                        else
                        {
                            // when `y` is `-a + ib` and `x` is `+a + ib` or `+a - ib` then
                            // we want to prefer `x` because either both parts are positive
                            // or we want to prefer `+a - ib` due to how it handles when `x`
                            // represents a real number.

                            return x;
                        }
                    }
                }
                else if (float.IsNegative(y._imag))
                {
                    if (float.IsNegative(x._real))
                    {
                        // when `y` is `+a - ib` and `x` is `-a + ib` or `-a - ib` then
                        // we either both parts of `x` are negative or we want to prefer
                        // `+a - ib` due to how it handles when `y` represents a real number.

                        return y;
                    }
                    else
                    {
                        // when `y` is `+a - ib` and `x` is `+a + ib` or `+a - ib` then
                        // we want to prefer `x` because either both parts are positive
                        // or they represent the same value.

                        return x;
                    }
                }
            }

            return y;
        }

        /// <inheritdoc cref="INumberBase{TSelf}.MinMagnitude(TSelf, TSelf)" />
        public static Complex32 MinMagnitude(Complex32 x, Complex32 y)
        {
            // complex numbers are not normally comparable, however every complex
            // number has a real magnitude (absolute value) and so we can provide
            // an implementation for MaxMagnitude

            // This matches the IEEE 754:2019 `minimumMagnitude` function
            //
            // It propagates NaN inputs back to the caller and
            // otherwise returns the input with a smaller magnitude.
            // It treats -0 as smaller than +0 as per the specification.

            double ax = Abs(x);
            double ay = Abs(y);

            if ((ax < ay) || double.IsNaN(ax))
            {
                return x;
            }

            if (ax == ay)
            {
                // We have two equal magnitudes which means we have two of the following
                //   `+a + ib`
                //   `-a + ib`
                //   `+a - ib`
                //   `-a - ib`
                //
                // We want to treat `+a + ib` as greater than everything and `-a - ib` as
                // lesser. For `-a + ib` and `+a - ib` its "ambiguous" which should be preferred
                // so we will just preference `-a + ib` since that's the most correct choice
                // in the face of something like `+a - i0.0` vs `-a + i0.0`. This is the "most
                // correct" choice because both represent real numbers and `-a` is preferred
                // over `+a`.

                if (float.IsNegative(y._real))
                {
                    if (float.IsNegative(y._imag))
                    {
                        // when `y` is `-a - ib` we always prefer `y` as both parts are negative
                        return y;
                    }
                    else
                    {
                        if (float.IsNegative(x._real))
                        {
                            // when `y` is `-a + ib` and `x` is `-a + ib` or `-a - ib` then
                            // we either have same value or both parts of `x` are negative
                            // and we want to prefer it.

                            return x;
                        }
                        else
                        {
                            // when `y` is `-a + ib` and `x` is `+a + ib` or `+a - ib` then
                            // we want to prefer `y` because either both parts of 'x' are positive
                            // or we want to prefer `-a - ib` due to how it handles when `y`
                            // represents a real number.

                            return y;
                        }
                    }
                }
                else if (float.IsNegative(y._imag))
                {
                    if (float.IsNegative(x._real))
                    {
                        // when `y` is `+a - ib` and `x` is `-a + ib` or `-a - ib` then
                        // either both parts of `x` are negative or we want to prefer
                        // `-a - ib` due to how it handles when `x` represents a real number.

                        return x;
                    }
                    else
                    {
                        // when `y` is `+a - ib` and `x` is `+a + ib` or `+a - ib` then
                        // we want to prefer `y` because either both parts of x are positive
                        // or they represent the same value.

                        return y;
                    }
                }
                else
                {
                    return x;
                }
            }

            return y;
        }

        /// <inheritdoc cref="INumberBase{TSelf}.MinMagnitudeNumber(TSelf, TSelf)" />
        public static Complex32 MinMagnitudeNumber(Complex32 x, Complex32 y)
        {
            // complex numbers are not normally comparable, however every complex
            // number has a real magnitude (absolute value) and so we can provide
            // an implementation for MinMagnitudeNumber

            // This matches the IEEE 754:2019 `minimumMagnitudeNumber` function
            //
            // It does not propagate NaN inputs back to the caller and
            // otherwise returns the input with a smaller magnitude.
            // It treats -0 as smaller than +0 as per the specification.

            double ax = Abs(x);
            double ay = Abs(y);

            if ((ax < ay) || double.IsNaN(ay))
            {
                return x;
            }

            if (ax == ay)
            {
                // We have two equal magnitudes which means we have two of the following
                //   `+a + ib`
                //   `-a + ib`
                //   `+a - ib`
                //   `-a - ib`
                //
                // We want to treat `+a + ib` as greater than everything and `-a - ib` as
                // lesser. For `-a + ib` and `+a - ib` its "ambiguous" which should be preferred
                // so we will just preference `-a + ib` since that's the most correct choice
                // in the face of something like `+a - i0.0` vs `-a + i0.0`. This is the "most
                // correct" choice because both represent real numbers and `-a` is preferred
                // over `+a`.

                if (float.IsNegative(y._real))
                {
                    if (float.IsNegative(y._imag))
                    {
                        // when `y` is `-a - ib` we always prefer `y` as both parts are negative
                        return y;
                    }
                    else
                    {
                        if (float.IsNegative(x._real))
                        {
                            // when `y` is `-a + ib` and `x` is `-a + ib` or `-a - ib` then
                            // we either have same value or both parts of `x` are negative
                            // and we want to prefer it.

                            return x;
                        }
                        else
                        {
                            // when `y` is `-a + ib` and `x` is `+a + ib` or `+a - ib` then
                            // we want to prefer `y` because either both parts of 'x' are positive
                            // or we want to prefer `-a - ib` due to how it handles when `y`
                            // represents a real number.

                            return y;
                        }
                    }
                }
                else if (float.IsNegative(y._imag))
                {
                    if (float.IsNegative(x._real))
                    {
                        // when `y` is `+a - ib` and `x` is `-a + ib` or `-a - ib` then
                        // either both parts of `x` are negative or we want to prefer
                        // `-a - ib` due to how it handles when `x` represents a real number.

                        return x;
                    }
                    else
                    {
                        // when `y` is `+a - ib` and `x` is `+a + ib` or `+a - ib` then
                        // we want to prefer `y` because either both parts of x are positive
                        // or they represent the same value.

                        return y;
                    }
                }
                else
                {
                    return x;
                }
            }

            return y;
        }

        /// <inheritdoc cref="INumberBase{TSelf}.Abs(TSelf)" />
        static Complex32 INumberBase<Complex32>.Abs(Complex32 value) =>
            ((Complex32)Abs(value));

        private static bool TryConvertFrom<TOther>(TOther value, out Complex32 result)
        {
            // We don't want to defer to `double.Create*(value)` because some type might have its own
            // `TOther.ConvertTo*(value, out Complex result)` handling that would end up bypassed.

            if (typeof(TOther) == typeof(byte))
            {
                byte actualValue = (byte)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                char actualValue = (char)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                decimal actualValue = (decimal)(object)value;
                result = (Complex32)actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualValue = (double)(object)value;
                result = (Complex32)actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualValue = (Half)(object)value;
                result = (float)actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                short actualValue = (short)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                int actualValue = (int)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                long actualValue = (long)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                Int128 actualValue = (Int128)(object)value;
                result = (Complex32)(long)actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                nint actualValue = (nint)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                sbyte actualValue = (sbyte)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualValue = (float)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                ushort actualValue = (ushort)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                uint actualValue = (uint)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                ulong actualValue = (ulong)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                UInt128 actualValue = (UInt128)(object)value;
                result = (Complex32)(ulong)actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                nuint actualValue = (nuint)(object)value;
                result = actualValue;
                return true;
            }
            else if (typeof(TOther) == typeof(Complex))
            {
                Complex actualValue = (Complex)(object)value;
                result = (Complex32)actualValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertFromChecked{TOther}(TOther, out TSelf)" />
        static bool INumberBase<Complex32>.TryConvertFromChecked<TOther>(TOther value, out Complex32 result)
        {
            return TryConvertFrom<TOther>(value, out result);
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertFromSaturating{TOther}(TOther, out TSelf)" />
        static bool INumberBase<Complex32>.TryConvertFromSaturating<TOther>(TOther value, out Complex32 result)
        {
            return TryConvertFrom<TOther>(value, out result);
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertFromTruncating{TOther}(TOther, out TSelf)" />
        static bool INumberBase<Complex32>.TryConvertFromTruncating<TOther>(TOther value, out Complex32 result)
        {
            return TryConvertFrom<TOther>(value, out result);
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertToChecked{TOther}(TSelf, out TOther)" />
        static bool INumberBase<Complex32>.TryConvertToChecked<TOther>(Complex32 value, out TOther result)
        {
            // Complex numbers with an imag part can't be represented as a "real number"
            // so we'll throw an OverflowException for this scenario for integer types and
            // for decimal. However, we will convert it to NaN for the floating-point types,
            // since that's what Sqrt(-1) (which is `new Complex(0, 1)`) results in.

            if (typeof(TOther) == typeof(byte))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                byte actualResult = checked((byte)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                char actualResult = checked((char)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                decimal actualResult = checked((decimal)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualResult = (value._imag != 0) ? double.NaN : value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualResult = (value._imag != 0) ? Half.NaN : (Half)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                short actualResult = checked((short)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                int actualResult = checked((int)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                long actualResult = checked((long)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                Int128 actualResult = checked((Int128)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                nint actualResult = checked((nint)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(BigInteger))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                BigInteger actualResult = checked((BigInteger)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                sbyte actualResult = checked((sbyte)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualResult = (value._imag != 0) ? float.NaN : (float)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                ushort actualResult = checked((ushort)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                uint actualResult = checked((uint)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                ulong actualResult = checked((ulong)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                UInt128 actualResult = checked((UInt128)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                if (value._imag != 0)
                {
                    throw new OverflowException();
                }

                nuint actualResult = checked((nuint)value._real);
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Complex))
            {
                Complex actualResult = checked(value.ToComplex());
                result = (TOther)(object)actualResult;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertToSaturating{TOther}(TSelf, out TOther)" />
        static bool INumberBase<Complex32>.TryConvertToSaturating<TOther>(Complex32 value, out TOther result)
        {
            // Complex numbers with an imaginary part can't be represented as a "real number"
            // and there isn't really a well-defined way to "saturate" to just a real value.
            //
            // The two potential options are that we either treat complex numbers with a non-
            // zero imaginary part as NaN and then convert that to 0 -or- we ignore the imaginary
            // part and only consider the real part.
            //
            // We use the latter below since that is "more useful" given an unknown number type.
            // Users who want 0 instead can always check `IsComplexNumber` and special-case the
            // handling.

            if (typeof(TOther) == typeof(byte))
            {
                byte actualResult = (value._real >= byte.MaxValue) ? byte.MaxValue :
                                    (value._real <= byte.MinValue) ? byte.MinValue : (byte)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                char actualResult = (value._real >= char.MaxValue) ? char.MaxValue :
                                    (value._real <= char.MinValue) ? char.MinValue : (char)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                decimal actualResult = (value._real >= (double)decimal.MaxValue) ? decimal.MaxValue :
                                       (value._real <= (double)decimal.MinValue) ? decimal.MinValue : (decimal)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualResult = value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualResult = (Half)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                short actualResult = (value._real >= short.MaxValue) ? short.MaxValue :
                                     (value._real <= short.MinValue) ? short.MinValue : (short)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                int actualResult = (value._real >= int.MaxValue) ? int.MaxValue :
                                   (value._real <= int.MinValue) ? int.MinValue : (int)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                long actualResult = (value._real >= long.MaxValue) ? long.MaxValue :
                                    (value._real <= long.MinValue) ? long.MinValue : (long)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                Int128 actualResult = (value._real >= +170141183460469231731687303715884105727.0) ? Int128.MaxValue :
                                      (value._real <= -170141183460469231731687303715884105728.0) ? Int128.MinValue : (Int128)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                nint actualResult = (value._real >= nint.MaxValue) ? nint.MaxValue :
                                    (value._real <= nint.MinValue) ? nint.MinValue : (nint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(BigInteger))
            {
                BigInteger actualResult = (BigInteger)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                sbyte actualResult = (value._real >= sbyte.MaxValue) ? sbyte.MaxValue :
                                     (value._real <= sbyte.MinValue) ? sbyte.MinValue : (sbyte)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualResult = (float)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                ushort actualResult = (value._real >= ushort.MaxValue) ? ushort.MaxValue :
                                      (value._real <= ushort.MinValue) ? ushort.MinValue : (ushort)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                uint actualResult = (value._real >= uint.MaxValue) ? uint.MaxValue :
                                    (value._real <= uint.MinValue) ? uint.MinValue : (uint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                ulong actualResult = (value._real >= ulong.MaxValue) ? ulong.MaxValue :
                                     (value._real <= ulong.MinValue) ? ulong.MinValue : (ulong)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                UInt128 actualResult = (value._real >= 340282366920938463463374607431768211455.0) ? UInt128.MaxValue :
                                       (value._real <= 0.0) ? UInt128.MinValue : (UInt128)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                nuint actualResult = (value._real >= nuint.MaxValue) ? nuint.MaxValue :
                                     (value._real <= nuint.MinValue) ? nuint.MinValue : (nuint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Complex))
            {
                result = (TOther)(object)value.ToComplex();
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryConvertToTruncating{TOther}(TSelf, out TOther)" />
        static bool INumberBase<Complex32>.TryConvertToTruncating<TOther>(Complex32 value, out TOther result)
        {
            // Complex numbers with an imaginary part can't be represented as a "real number"
            // so we'll only consider the real part for the purposes of truncation.

            if (typeof(TOther) == typeof(byte))
            {
                byte actualResult = (value._real >= byte.MaxValue) ? byte.MaxValue :
                                    (value._real <= byte.MinValue) ? byte.MinValue : (byte)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                char actualResult = (value._real >= char.MaxValue) ? char.MaxValue :
                                    (value._real <= char.MinValue) ? char.MinValue : (char)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                decimal actualResult = (value._real >= (double)decimal.MaxValue) ? decimal.MaxValue :
                                       (value._real <= (double)decimal.MinValue) ? decimal.MinValue : (decimal)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                double actualResult = value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Half))
            {
                Half actualResult = (Half)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                short actualResult = (value._real >= short.MaxValue) ? short.MaxValue :
                                     (value._real <= short.MinValue) ? short.MinValue : (short)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                int actualResult = (value._real >= int.MaxValue) ? int.MaxValue :
                                   (value._real <= int.MinValue) ? int.MinValue : (int)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                long actualResult = (value._real >= long.MaxValue) ? long.MaxValue :
                                    (value._real <= long.MinValue) ? long.MinValue : (long)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Int128))
            {
                Int128 actualResult = (value._real >= +170141183460469231731687303715884105727.0) ? Int128.MaxValue :
                                      (value._real <= -170141183460469231731687303715884105728.0) ? Int128.MinValue : (Int128)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                nint actualResult = (value._real >= nint.MaxValue) ? nint.MaxValue :
                                    (value._real <= nint.MinValue) ? nint.MinValue : (nint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(BigInteger))
            {
                BigInteger actualResult = (BigInteger)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                sbyte actualResult = (value._real >= sbyte.MaxValue) ? sbyte.MaxValue :
                                     (value._real <= sbyte.MinValue) ? sbyte.MinValue : (sbyte)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                float actualResult = (float)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                ushort actualResult = (value._real >= ushort.MaxValue) ? ushort.MaxValue :
                                      (value._real <= ushort.MinValue) ? ushort.MinValue : (ushort)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                uint actualResult = (value._real >= uint.MaxValue) ? uint.MaxValue :
                                    (value._real <= uint.MinValue) ? uint.MinValue : (uint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                ulong actualResult = (value._real >= ulong.MaxValue) ? ulong.MaxValue :
                                     (value._real <= ulong.MinValue) ? ulong.MinValue : (ulong)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(UInt128))
            {
                UInt128 actualResult = (value._real >= 340282366920938463463374607431768211455.0) ? UInt128.MaxValue :
                                       (value._real <= 0.0) ? UInt128.MinValue : (UInt128)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                nuint actualResult = (value._real >= nuint.MaxValue) ? nuint.MaxValue :
                                     (value._real <= nuint.MinValue) ? nuint.MinValue : (nuint)value._real;
                result = (TOther)(object)actualResult;
                return true;
            }
            else if (typeof(TOther) == typeof(Complex))
            {
                Complex actualResult = value.ToComplex();
                result = (TOther)(object)actualResult;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
        /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat(Span{byte}, out int, ReadOnlySpan{char}, IFormatProvider?)" />
        public bool TryFormat(Span<char> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider = null) =>
            TryFormatCore(utf8Destination, out bytesWritten, format, provider);

        //TODO: Reimplement it with the Complex32 Format require
        private bool TryFormatCore<TChar>(Span<TChar> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) where TChar : unmanaged, IBinaryInteger<TChar>
        {
            Debug.Assert(typeof(TChar) == typeof(char) || typeof(TChar) == typeof(byte));

            // We have at least 6 more characters for: <0; 0>
            if (destination.Length >= 6)
            {
                int realChars;

                if (typeof(TChar) == typeof(char) ?
                    _real.TryFormat(CastCharSpan<TChar>(destination.Slice(1)), out realChars, format, provider) :
                    _real.TryFormat(CastByteSpan<TChar>(destination.Slice(1)), out realChars, format, provider))
                {
                    destination[0] = TChar.CreateTruncating('(');
                    destination = destination.Slice(1 + realChars); // + 1 for <

                    // We have at least 4 more characters for: ; 0>
                    if (destination.Length >= 4)
                    {
                        int imaginaryChars;
                        if (typeof(TChar) == typeof(char) ?
                            _imag.TryFormat(CastCharSpan<TChar>(destination.Slice(2)), out imaginaryChars, format, provider) :
                            _imag.TryFormat(CastByteSpan<TChar>(destination.Slice(2)), out imaginaryChars, format, provider))
                        {
                            // We have 1 more character for: >
                            if ((uint)(2 + imaginaryChars) < (uint)destination.Length)
                            {
                                destination[0] = TChar.CreateTruncating(',');
                                destination[1] = TChar.CreateTruncating(' ');
                                destination[2 + imaginaryChars] = TChar.CreateTruncating(')');

                                charsWritten = realChars + imaginaryChars + 4;
                                return true;
                            }
                        }
                    }
                }
            }

            charsWritten = 0;
            return false;
        }

        private static Span<char> CastCharSpan<TChar>(Span<TChar> span) where TChar : unmanaged, IBinaryInteger<TChar>
        {
            ref TChar r0 = ref MemoryMarshal.GetReference(span);
            return MemoryMarshal.Cast<TChar, char>(span);
        }

        private static Span<byte> CastByteSpan<TChar>(Span<TChar> span) where TChar : unmanaged, IBinaryInteger<TChar>
        {
            ref TChar r0 = ref MemoryMarshal.GetReference(span);
            return MemoryMarshal.Cast<TChar, byte>(span);
        }

        public static Complex32 operator ++(Complex32 value) => value + One;

        public static Complex32 operator --(Complex32 value) => value - One;

    }
#endif
}
