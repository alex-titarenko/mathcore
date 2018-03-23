// <copyright file="Options.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2014 Math.NET
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

namespace MathNet.Numerics.LinearAlgebra
{
    public enum Symmetricity
    {
        /// <summary>
        /// It is not known yet whether a matrix is symmetric or not.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A matrix is symmetric
        /// </summary>
        Symmetric = 1,

        /// <summary>
        /// A matrix is hermitian (conjugate symmetric).
        /// </summary>
        Hermitian = 2,

        [Obsolete("Use Hermitian instead. Will be removed in v4.")]
        ConjugateSymmetric = 2,

        /// <summary>
        /// A matrix is not symmetric
        /// </summary>
        Asymmetric = 3
    }
}
