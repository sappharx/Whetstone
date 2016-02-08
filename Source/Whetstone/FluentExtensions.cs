namespace System.Collections.Generic
{
    /// <summary>
    /// Provides some fluent APIs to certain types
    /// </summary>
    public static class FluentExtensions
    {
        /// <summary>
        /// Creates a new <see cref="FluentCollection{T}"/> from an existing <see cref="ICollection{T}"/> object
        /// </summary>
        /// <typeparam name="T">The type of the objects held in original collection</typeparam>
        /// <param name="this">The original collection object</param>
        /// <returns>A new <see cref="FluentCollection{T}"/> object wrapping the original collection</returns>
        public static FluentCollection<T> AsFluentCollection<T>(this ICollection<T> @this)
            => new FluentCollection<T>(@this);
    }
}
