namespace SimplePersistence.UoW.Helper
{
    using System;
    using Exceptions;

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
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return GetAndRelease<TFactory, TUoW, T>(
                factory, u => UnitOfWorkExtensions.ExecuteAndCommit(u, toExecute));
        }
#else
        public static T GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW, T>(
            this TFactory factory, Func<TUoW, T> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return factory.GetAndRelease<TFactory, TUoW, T>(uow => uow.ExecuteAndCommit(toExecute));
        }
#endif

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
            TFactory factory, Func<TUoW> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            GetAndRelease<TFactory, TUoW>(
                factory, u => UnitOfWorkExtensions.ExecuteAndCommit(u, toExecute));
        }
#else
        public static void GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW>(
            this TFactory factory, Action<TUoW> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            factory.GetAndRelease<TFactory, TUoW>(uow => uow.ExecuteAndCommit(toExecute));
        }
#endif

#endregion

#endregion
    }
}
