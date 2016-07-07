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
    /// Contains extension methods for <see cref="IUnitOfWorkFactory"/> instances.
    /// </summary>
    public static partial class UnitOfWorkFactoryExtensions
    {
        #region GetAndRelease

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T GetAndRelease<TFactory, TUoW, T>(TFactory factory, Func<TUoW, T> toExecute)
#else
        public static T GetAndRelease<TFactory, TUoW, T>(this TFactory factory, Func<TUoW, T> toExecute)
#endif
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                return toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T GetAndRelease<TUoW, T>(IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
#else
        public static T GetAndRelease<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            return GetAndRelease<IUnitOfWorkFactory, TUoW, T>(factory, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static void GetAndRelease<TFactory, TUoW>(TFactory factory, Action<TUoW> toExecute)
#else
        public static void GetAndRelease<TFactory, TUoW>(this TFactory factory, Action<TUoW> toExecute)
#endif
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static void GetAndRelease<TUoW>(IUnitOfWorkFactory factory, Action<TUoW> toExecute)
#else
        public static void GetAndRelease<TUoW>(this IUnitOfWorkFactory factory, Action<TUoW> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            GetAndRelease<IUnitOfWorkFactory, TUoW>(factory, toExecute);
        }

        #endregion

        #endregion

        #region GetAndReleaseAfterExecuteAndCommit

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET20
        public static T GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW, T>(
            TFactory factory, Func<TUoW, T> toExecute)
#else
        public static T GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, T> toExecute)
#endif
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return GetAndRelease<TFactory, TUoW, T>(
                // ReSharper disable once InvokeAsExtensionMethod
                factory, uow => UnitOfWorkExtensions.ExecuteAndCommit(uow, toExecute));
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET20
        public static T GetAndReleaseAfterExecuteAndCommit<TUoW, T>(IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
#else
        public static T GetAndReleaseAfterExecuteAndCommit<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            return GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, TUoW, T>(factory, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET20
        public static void GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW>(
            TFactory factory, Action<TUoW> toExecute)
#else
        public static void GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW>(
            this TFactory factory, Action<TUoW> toExecute)
#endif
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            GetAndRelease<TFactory, TUoW>(
                // ReSharper disable once InvokeAsExtensionMethod
                factory, uow => UnitOfWorkExtensions.ExecuteAndCommit(uow, toExecute));
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET20
        public static void GetAndReleaseAfterExecuteAndCommit<TUoW>(IUnitOfWorkFactory factory, Action<TUoW> toExecute)
#else
        public static void GetAndReleaseAfterExecuteAndCommit<TUoW>(this IUnitOfWorkFactory factory, Action<TUoW> toExecute)
#endif
            where TUoW : IUnitOfWork
        {
            GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, TUoW>(factory, toExecute);
        }

        #endregion

        #endregion
    }
}
