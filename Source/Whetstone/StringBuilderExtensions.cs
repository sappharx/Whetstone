using System.Collections.Generic;
using System.Linq;

namespace System.Text
{
    /// <summary>
    /// Provides extension methods for StringBuilder
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a formatted string along with a new line
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> object</param>
        /// <param name="format">The string format to fulfill</param>
        /// <param name="args">The arguments used to fill the format</param>
        /// <returns>
        /// The base <see cref="StringBuilder"/> object with the appended formatted string and new line
        /// </returns>
        public static StringBuilder AppendFormattedLine(this StringBuilder builder,
            string format,
            params object[] args)
                => builder.AppendFormat(format, args).AppendLine();

        /// <summary>
        /// Appends a sequence of objects
        /// </summary>
        /// <typeparam name="T">The type of objects being appended</typeparam>
        /// <param name="builder">The <see cref="StringBuilder"/> object</param>
        /// <param name="sequence">The sequence of objects to append</param>
        /// <param name="func">The function to apply to each object (this will typically be a call to an Append method</param>
        /// <returns>
        /// The base <see cref="StringBuilder"/> object with the sequence of objects appended to it
        /// </returns>
        /// <example>
        /// Append a list of names on new lines
        /// <code>
        /// var names = new[] {"Alice", "Bob", "Carl"};
        /// var str = new StringBuilder()
        ///     .AppendSequence(names, (builder, name) => builder.AppendFormattedLine("{0}", name))
        ///     .ToString();
        /// </code>
        /// </example>
        public static StringBuilder AppendSequence<T>(this StringBuilder builder,
            IEnumerable<T> sequence,
            Func<StringBuilder, T, StringBuilder> func)
                => sequence.Aggregate(builder, func);

        /// <summary>
        /// Removes the last character
        /// </summary>
        /// <param name="builder">The <see cref="StringBuilder"/> object</param>
        /// <returns>The base <see cref="StringBuilder"/> with the last character removed</returns>
        public static StringBuilder RemoveLastCharacter(this StringBuilder builder)
            => builder.Remove(builder.Length - 1, 1);
    }
}
