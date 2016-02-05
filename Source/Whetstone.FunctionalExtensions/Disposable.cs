namespace System
{
    /// <summary>
    /// Provides static methods for working with <see cref="IDisposable"/> objects
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// Abstracts a using statement when you just need to obtain a value from an <see cref="IDisposable"/> object
        /// </summary>
        /// <typeparam name="TDisposable">The type of the <see cref="IDisposable"/> object</typeparam>
        /// <typeparam name="TResult">The type of the object to return</typeparam>
        /// <param name="factory">The function that creates the disposable object</param>
        /// <param name="func">The function to apply to the disposable object to obtain the result</param>
        /// <returns>
        /// The value obtained by applying the supplied function to the created disposable object
        /// </returns>
        /// <example>
        /// Create a string array by reading a file and splitting it into lines
        /// <code>
        /// // `GetFileStream` is a method that returns a StreamReader for the specific file, i.e. it takes no arguments
        /// var lines = Disposable.Using(GetFileStream, reader => reader.ReadToEnd().Split(System.Environment.Newline));
        /// </code>
        /// as opposed to
        /// <code>
        /// string[] lines;
        /// using (var reader = GetFileStream())
        /// {
        ///     lines = reader.ReadToEnd().Split(System.Environment.Newline);
        /// }
        /// </code>
        /// </example>
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> func)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return func(disposable);
            }
        }
    }
}
