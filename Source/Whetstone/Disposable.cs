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

        /// <summary>
        /// Abstracts a using statement when you just need to obtain a value from 2 <see cref="IDisposable"/> objects
        /// </summary>
        /// <typeparam name="TDisposable1">The type of the first <see cref="IDisposable"/> object</typeparam>
        /// <typeparam name="TDisposable2">The type of the second <see cref="IDisposable"/> object</typeparam>
        /// <typeparam name="TResult">The type of the object to return</typeparam>
        /// <param name="factory1">The function that creates the first disposable object</param>
        /// <param name="factory2">The function that creates the second disposable object</param>
        /// <param name="func">The function to apply to the disposable objects to obtain the result</param>
        /// <returns>
        /// The value obtained by applying the supplied function to the created disposable objects
        /// </returns>
        /// <example>
        /// Create a string array by reading 2 files, splitting them into lines, and concatenating the 2 arrays
        /// <code>
        /// // `GetFileStream1` and `GetFileStream1` are methods that return `StreamReader`s for specific files
        /// var lines = Disposable.Using(GetFileStream1, GetFileStream2, 
        ///     (reader1, reader2) => reader1.ReadToEnd().Split(System.Environment.Newline)
        ///         .Concat(reader2.ReadToEnd().Split(System.Environment.Newline)));
        /// </code>
        /// as opposed to
        /// <code>
        /// string[] lines;
        /// using (var reader1 = GetFileStream1())
        /// using (var reader2 = GetFileStream2())
        /// {
        ///     lines = reader1.ReadToEnd().Split(System.Environment.Newline);
        ///     lines = lines.Concat(reader2.ReadToEnd().Split(System.Environment.Newline));
        /// }
        /// </code>
        /// </example>
        public static TResult Using<TDisposable1, TDisposable2, TResult>(
            Func<TDisposable1> factory1,
            Func<TDisposable2> factory2,
            Func<TDisposable1, TDisposable2, TResult> func)
                where TDisposable1 : IDisposable
                where TDisposable2 : IDisposable
        {
            using (var disposable1 = factory1())
            using (var disposable2 = factory2())
            {
                return func(disposable1, disposable2);
            }
        }
    }
}
