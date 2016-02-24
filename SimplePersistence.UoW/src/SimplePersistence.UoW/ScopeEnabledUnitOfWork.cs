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
namespace SimplePersistence.UoW
{
    using System;
    using System.Threading;
    using Exceptions;
#if !(NET20 || NET35)
    using System.Linq;
    using System.Threading.Tasks;
#endif

    /// <summary>
    /// Represents a scope enabled <see cref="IUnitOfWork"/> that guarantees any given
    /// begin or commit logic is only invoqued once for any given scope. Every scope information is thread safe.
    /// </summary>
#if !PORTABLE
    [System.Diagnostics.DebuggerDisplay("Id = {_privateId}, CurrentScope = {_currentScope}")]
#endif
    public abstract class ScopeEnabledUnitOfWork : IUnitOfWork
    {
        private int _currentScope;
        private readonly Guid _privateId = Guid.NewGuid();

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        public void Begin()
        {
            var s = IncrementScope();
            if(s == 1)
                OnBegin();
        }

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        public void Commit()
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException(s);

            if(s != 0) return;

            try
            {
                OnCommit();
            }
            catch (UnitOfWorkException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CommitException(e);
            }
        }

#if !(NET20 || NET35)

        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
#if NET40 || PORTABLE40
        public Task BeginAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = IncrementScope();
            if (s == 1)
                return OnBeginAsync(ct);

            var cts = new TaskCompletionSource<bool>();
            if (ct.IsCancellationRequested)
                cts.SetCanceled();
            else
                cts.SetResult(true);
            return cts.Task;
        }
#else
        public async Task BeginAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = IncrementScope();
            if (s == 1)
                await OnBeginAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
#if NET40 || PORTABLE40
        public Task CommitAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException(s);

            if (s == 0)
                return OnCommitAsync(ct).ContinueWith(t =>
                {
                    if(!t.IsFaulted) return;

                    if(t.Exception == null) // it should never happen if faulted
                        throw new CommitException();

                    t.Exception.Flatten().Handle(ex =>
                    {
                        if (ex is UnitOfWorkException)
                            throw ex;
                        throw new CommitException(ex);
                    });
                }, ct);

            var cts = new TaskCompletionSource<bool>();
            if (ct.IsCancellationRequested)
                cts.SetCanceled();
            else
                cts.SetResult(true);
            return cts.Task;
        }
#else
        public async Task CommitAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException(s);

            if (s != 0) return;

            try
            {
                await OnCommitAsync(ct);
            }
            catch (UnitOfWorkException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CommitException(e);
            }
        }
#endif

        /// <summary>
        /// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
        /// </summary>
        /// <typeparam name="T">The query item type</typeparam>
        /// <param name="queryable">The query to wrap</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        public abstract IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable);

#endif

        #endregion

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        protected abstract void OnBegin();

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
		/// made by this instance
        /// </summary>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected abstract void OnCommit();

#if !(NET20 || NET35)

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected abstract Task OnBeginAsync(CancellationToken ct);

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
		/// made by this instance
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected abstract Task OnCommitAsync(CancellationToken ct);

#endif

        #region Helpers

        private int DecrementScope()
        {
            return Interlocked.Decrement(ref _currentScope);
        }

        private int IncrementScope()
        {
            return Interlocked.Increment(ref _currentScope);
        }

        #endregion
    }
}
