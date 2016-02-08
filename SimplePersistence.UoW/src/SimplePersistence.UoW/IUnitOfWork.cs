namespace SimplePersistence.UoW
{
#if !(NET20 || NET35)
    using System.Threading;
    using System.Threading.Tasks;
#endif

    /// <summary>
    /// Interface representing an unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        void Begin();

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback every work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        void Rollback();

#if !(NET20 || NET35)

        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task BeginAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Asynchronously commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task CommitAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Asynchronously rollback every work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task RollbackAsync(CancellationToken ct = default(CancellationToken));

#endif
    }
}
