﻿#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 SimplePersistence
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimplePersistence.UoW.Helper
{
#if NET20
    /// <summary>
    /// Encapsulates a method that has no parameters and does not return a value.
    /// </summary>
    public delegate void Action();

    /// <summary>
    /// Encapsulates a method that has one parameter and does not return a value.
    /// </summary>
    /// <typeparam name="T">The parameter type</typeparam>
    /// <param name="t">The parameter value</param>
    public delegate void Action<in T>(T t);

    /// <summary>
    /// Encapsulates a method that has no parameters and returns a value.
    /// </summary>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <returns>The result value</returns>
    public delegate TResult Func<out TResult>();

    /// <summary>
    /// Encapsulates a method that has a single parameter and returns a value.
    /// </summary>
    /// <typeparam name="T">The parameter type</typeparam>
    /// <typeparam name="TResult">The result type</typeparam>
    /// <param name="t">The parameter value</param>
    /// <returns>The result value</returns>
    public delegate TResult Func<in T, out TResult>(T t);
#endif
}
