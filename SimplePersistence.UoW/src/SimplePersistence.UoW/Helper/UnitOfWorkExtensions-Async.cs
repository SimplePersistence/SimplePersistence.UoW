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

    public static partial class UnitOfWorkExtensions
    {
        #region Task<T>

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET40 || PORTABLE40
        public static Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var tcs = new TaskCompletionSource<T>();
            uow.BeginAsync(ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    toExecute(uow, ct).ContinueWith(t02 =>
                    {
                        if (t02.IsFaulted)
                        {
                            tcs.SetExceptionFromTask(t02);
                        }
                        else if (t02.IsCompleted)
                        {
                            uow.CommitAsync(ct).ContinueWith(t03 =>
                            {
                                if (t03.IsFaulted)
                                {
                                    tcs.SetExceptionFromTask(t03);
                                }
                                else if (t03.IsCompleted)
                                {
                                    tcs.SetResult(t02.Result);
                                }
                                else
                                {
                                    tcs.SetCanceled();
                                }
                            }, ct);
                        }
                        else
                        {
                            tcs.SetCanceled();
                        }
                    }, ct);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }
#else
        public static async Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            var result = await toExecute(uow, ct);

            await uow.CommitAsync(ct);

            return result;
        }
#endif

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET40 || PORTABLE40
        public static Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), ct);
        }
#else
        public static async Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return await uow.ExecuteAndCommitAsync(async (u, c) => await toExecute(), ct);
        }
#endif

        #endregion

        #region Task

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET40 || PORTABLE40
        public static Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var tcs = new TaskCompletionSource<bool>();
            uow.BeginAsync(ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    toExecute(uow, ct).ContinueWith(t02 =>
                    {
                        if (t02.IsFaulted)
                        {
                            tcs.SetExceptionFromTask(t02);
                        }
                        else if (t02.IsCompleted)
                        {
                            uow.CommitAsync(ct).ContinueWith(t03 =>
                            {
                                if (t03.IsFaulted)
                                {
                                    tcs.SetExceptionFromTask(t03);
                                }
                                else if (t03.IsCompleted)
                                {
                                    tcs.SetResult(true);
                                }
                                else
                                {
                                    tcs.SetCanceled();
                                }
                            }, ct);
                        }
                        else
                        {
                            tcs.SetCanceled();
                        }
                    }, ct);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }
#else
        public static async Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            await toExecute(uow, ct);

            await uow.CommitAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
#if NET40 || PORTABLE40
        public static Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), ct);
        }
#else
        public static async Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            await uow.ExecuteAndCommitAsync(async (u, c) =>
            {
                await toExecute();
            }, ct);
        }
#endif

        #endregion
    }

#endif
}
