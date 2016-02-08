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
