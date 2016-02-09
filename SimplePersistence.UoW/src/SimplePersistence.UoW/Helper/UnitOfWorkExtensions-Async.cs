namespace SimplePersistence.UoW.Helper
{
#if !(NET20 || NET35)

    using System;
    using System.Threading.Tasks;
    using Exceptions;
    using CtToken = System.Threading.CancellationToken;

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
            this TUoW uow, Func<TUoW, CtToken, Task<T>> toExecute, CtToken ct = default(CtToken))
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
            this TUoW uow, Func<TUoW, CtToken, Task<T>> toExecute, CtToken ct = default(CtToken))
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
            this TUoW uow, Func<Task<T>> toExecute, CtToken ct = default(CtToken))
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), ct);
        }
#else
        public static async Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<Task<T>> toExecute, CtToken ct = default(CtToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            var result = await toExecute();

            await uow.CommitAsync(ct);

            return result;
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
            this TUoW uow, Func<TUoW, CtToken, Task> toExecute, CtToken ct = default(CtToken))
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
            this TUoW uow, Func<TUoW, CtToken, Task> toExecute, CtToken ct = default(CtToken))
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
            this TUoW uow, Func<Task> toExecute, CtToken ct = default(CtToken))
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), ct);
        }
#else
        public static async Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<Task> toExecute, CtToken ct = default(CtToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            await toExecute();

            await uow.CommitAsync(ct);
        }
#endif

        #endregion
    }

#endif
}
