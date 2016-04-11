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
#if !(NET20 || NET35)
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;

    public static partial class UnitOfWorkFactoryExtensions
    {
        #region GetAndReleaseAsync

        #region Task<T>

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute,
            CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();

            var tcs = new TaskCompletionSource<T>();
            toExecute(uow, ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    tcs.SetResult(t01.Result);
                    factory.Release(uow);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }
#else
        public static async Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default (CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                return await toExecute(uow, ct);
            }
            finally
            {
                factory.Release(uow);
            }
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }
#else
        public static async Task<T> GetAndReleaseAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, Task<T>> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<TFactory, TUoW, T>((uow, ct) => toExecute(uow));
        }
#else
        public static async Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, Task<T>> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<TFactory, TUoW, T>(async (uow, ct) => await toExecute(uow));
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAsync<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }
#else
        public static async Task<T> GetAndReleaseAsync<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }
#endif

        #endregion

        #region Task

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute,
            CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();

            var tcs = new TaskCompletionSource<bool>();
            toExecute(uow, ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    tcs.SetResult(true);
                    factory.Release(uow);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }
#else
        public static async Task GetAndReleaseAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                await toExecute(uow, ct);
            }
            finally
            {
                factory.Release(uow);
            }
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }
#else
        public static async Task GetAndReleaseAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, Task> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<TFactory, TUoW>((uow, ct) => toExecute(uow));
        }
#else
        public static async Task GetAndReleaseAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, Task> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<TFactory, TUoW>(async (uow, ct) =>
            {
                await toExecute(uow);
            });
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }
#else
        public static async Task GetAndReleaseAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }
#endif

        #endregion

        #endregion

        #region GetAndReleaseAfterExecuteAndCommitAsync

        #region Task<T>

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute,
            CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return factory.GetAndReleaseAsync<TFactory, TUoW, T>(
                (uow01, ct01) => uow01.ExecuteAndCommitAsync(toExecute, ct01), ct);
        }
#else
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return await factory.GetAndReleaseAsync<TFactory, TUoW, T>(async (uow01, ct01) =>
            {
                return await uow01.ExecuteAndCommitAsync(async (uow02, ct02) => await toExecute(uow02, ct02), ct01);
            }, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }
#else
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, Task<T>> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>((uow, ct) => toExecute(uow));
        }
#else
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, Task<T>> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
                async (uow, ct) => await toExecute(uow));
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }
#else
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
            where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }
#endif

        #endregion

        #region Task

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute,
            CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return factory.GetAndReleaseAsync<TFactory, TUoW>(
                (uow01, ct01) => uow01.ExecuteAndCommitAsync(toExecute, ct01), ct);
        }
#else
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await factory.GetAndReleaseAsync<TFactory, TUoW>(async (uow01, ct01) =>
            {
                await uow01.ExecuteAndCommitAsync(async (uow02, ct02) => await toExecute(uow02, ct02), ct01);
            }, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }
#else
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, Task> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>((uow, ct) => toExecute(uow));
        }
#else
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
            this TFactory factory, Func<TUoW, Task> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
                async (uow, ct) => await toExecute(uow));
        }
#endif

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
#if NET40 || PORTABLE40
        public static Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
            where TUoW : IUnitOfWork
        {
            return factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }
#else
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
            this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
            where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }
#endif

        #endregion

        #endregion
    }

#endif
}
