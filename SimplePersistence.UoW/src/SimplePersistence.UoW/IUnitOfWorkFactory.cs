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
namespace SimplePersistence.UoW
{
    using System;

    /// <summary>
    /// Interface to a factory of <see cref="IUnitOfWork"/> objects.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Gets a given <see cref="IUnitOfWork"/> of the specified type
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <returns>An <see cref="IUnitOfWork"/> instance</returns>
        TUoW Get<TUoW>() where TUoW : IUnitOfWork;

        /// <summary>
        /// Gets a given <see cref="IUnitOfWork"/> of the specified type
        /// </summary>
        /// <param name="uowType">The <see cref="IUnitOfWork"/> type</param>
        /// <returns>An <see cref="IUnitOfWork"/> instance</returns>
        IUnitOfWork Get(Type uowType);

        /// <summary>
        /// Releases a given <see cref="IUnitOfWork"/> 
        /// </summary>
        /// <param name="unitOfWork">The instance to be released</param>
        void Release(IUnitOfWork unitOfWork);
    }
}
