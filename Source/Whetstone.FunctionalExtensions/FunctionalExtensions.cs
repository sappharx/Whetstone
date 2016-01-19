using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Provides some functional capabilities to all types
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Maps an object of type TSource to an object of type TResult by applying the supplied function to it
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TResult">The type of the result object</typeparam>
        /// <param name="this">The source object</param>
        /// <param name="func">The function to apply to the source object</param>
        /// <returns>The result of the provided function applied to the source object</returns>
        /// <remarks>
        /// This really comes in handy when you want to create fluent code by chaining methods together
        /// </remarks>
        /// <example>
        /// Parse an integer from a string
        /// <code>
        /// int number = "123".Map(int.Parse);  // number == 123
        /// </code>
        /// Double a number
        /// <code>
        /// int number = 12.Map(x => x * 2);    // number == 24
        /// </code>
        /// </example>
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func)
            => func(@this);
    }
}
