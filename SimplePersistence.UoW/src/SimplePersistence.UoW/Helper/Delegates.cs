namespace SimplePersistence.UoW.Helper
{
#if NET20
        public delegate void Action();
        public delegate void Action<in T>(T t);

        public delegate TResult Func<out TResult>();
        public delegate TResult Func<in T, out TResult>(T t);
#endif
}
