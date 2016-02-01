using System.Collections.Generic;
using System.Linq;
using Whetstone.FunctionalExtensions;

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
        /// <param name="source">The source object</param>
        /// <param name="transform">The function to apply to the source object</param>
        /// <returns>The result of the provided function applied to the source object</returns>
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
        public static TResult Map<TSource, TResult>(this TSource source, Func<TSource, TResult> transform)
            => transform(source);

        /// <summary>
        /// Maps an <see cref="IEnumerable{TSource}"/> to an<see cref="IEnumerable{TResult}"/> 
        /// by applying the supplied function to each object in the source enumerable object
        /// </summary>
        /// <typeparam name="TSource">The type of the objects in the source enumerable object</typeparam>
        /// <typeparam name="TResult">The type of the objects in the result enumerable object</typeparam>
        /// <param name="source">The source enumerable object</param>
        /// <param name="transform">The function to apply to each object in the source enumerable object</param>
        /// <returns>A new enumerable object whose objects have had the provided function applied to them</returns>
        /// <example>
        /// Convert an array of integers to an array of strings
        /// <code>
        /// var strings = new[] {1, 2, 3}.Map&lt;int, string&gt;(x => x.ToString());
        /// </code>
        /// </example>
        /// <remarks>
        /// The type parameters must be explicit for this overload of the Map() function, because 
        /// the C# compiler's type inference will give precendence to the other Map() overload, i.e.
        /// <code>
        /// // applies ToString() to the int array, instead of each int separately
        /// new[] {1, 2, 3}.Map(x => x.ToString());
        /// </code>
        /// </remarks>
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> transform)
            => source.Aggregate(
                new List<TResult>().AsFluentCollection(),
                (output, input) => output.Add(input.Map(transform))
            );

        /// <summary>
        /// Performs an action on an object and returns the object (e.g. writing it to the console or a log
        /// </summary>
        /// <typeparam name="T">The type of object to be acted upon</typeparam>
        /// <param name="this">The object to be acted upon</param>
        /// <param name="action">The action to perform on the object</param>
        /// <returns>The object that was acted upon</returns>
        /// <example>
        /// Write an object to the console
        /// <code>
        /// "Hello world".Tee(Console.WriteLine);
        /// </code>
        /// </example>
        public static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        /// <summary>
        /// Applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate expression to evaluate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <example>
        /// Repeat a string if true
        /// <code>
        /// int a = "abc".When(true, s => s + s);   a == "abcabc"
        /// int b = "abc".When(false, s => s + s);  b == "abc"
        /// </code>
        /// </example>
        public static T When<T>(this T source, bool predicate, Func<T, T> transform)
            => predicate ? transform(source) : source;

        /// <summary>
        /// Applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate function to evaulate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <example>
        /// Double a number if allowed to
        /// <code>
        /// bool isAllowed = true;
        /// int a = 12.When(() => isAllowed, x => x * 2);   a == 24
        /// isAllowed = false;
        /// int b = 13.When(() => isAllowed, x => x * 2);   b == 13
        /// </code>
        /// </example>
        public static T When<T>(this T source, Func<bool> predicate, Func<T, T> transform)
            => predicate() ? transform(source) : source;

        /// <summary>
        /// Applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate function to evaulate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <example>
        /// Divide an integer in half only if it is even
        /// <code>
        /// int a = 12.When(x => x % 2 == 0, x => x / 2);   a == 6
        /// int b = 13.When(x => x % 2 == 0, x => x / 2);   b == 13
        /// </code>
        /// </example>
        public static T When<T>(this T source, Func<T, bool> predicate, Func<T, T> transform)
            => predicate(source) ? transform(source) : source;
    }
}
