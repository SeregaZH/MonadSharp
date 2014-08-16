namespace MonadSharp.Maybe
{
    /// <summary>
    /// Maybe monoid abstraction
    /// </summary>
    /// <typeparam name="T">Monoid type</typeparam>
    public interface IMaybeMonoid<out T>
    {
        /// <summary>
        /// Monoid value
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Flag to identify that valid value setted
        /// </summary>
        bool HasValue { get; }
    }
}
