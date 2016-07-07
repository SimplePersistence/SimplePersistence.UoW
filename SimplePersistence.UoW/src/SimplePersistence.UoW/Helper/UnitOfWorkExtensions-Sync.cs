#region License
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
    using System;
    using Exceptions;

    /// <summary>
    /// Contains extension methods for <see cref="IUnitOfWork"/> instances.
    /// </summary>
    public static partial class UnitOfWorkExtensions
    {
        #region T

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET20
        public static T ExecuteAndCommit<TUoW, T>(TUoW uow, Func<TUoW, T> toExecute)
#else
        public static T ExecuteAndCommit<TUoW, T>(this TUoW uow, Func<TUoW, T> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));
            
            uow.Begin();

            var result = toExecute(uow);

            uow.Commit();

            return result;
        }

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET20
        public static T ExecuteAndCommit<TUoW, T>(TUoW uow, Func<T> toExecute)
#else
        public static T ExecuteAndCommit<TUoW, T>(this TUoW uow, Func<T> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            var result = toExecute();

            uow.Commit();

            return result;
        }

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET20
        public static T ExecuteAndCommit<T>(IUnitOfWork uow, Func<T> toExecute)
#else
        public static T ExecuteAndCommit<T>(this IUnitOfWork uow, Func<T> toExecute)
#endif
        {
            return ExecuteAndCommit<IUnitOfWork, T>(uow, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Executes the given action inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET20
        public static void ExecuteAndCommit<TUoW>(TUoW uow, Action<TUoW> toExecute)
#else
        public static void ExecuteAndCommit<TUoW>(this TUoW uow, Action<TUoW> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            toExecute(uow);

            uow.Commit();
        }

        /// <summary>
        /// Executes the given action inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET20
        public static void ExecuteAndCommit<TUoW>(TUoW uow, Action toExecute)
#else
        public static void ExecuteAndCommit<TUoW>(this TUoW uow, Action toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            toExecute();

            uow.Commit();
        }

        #endregion
    }
}
