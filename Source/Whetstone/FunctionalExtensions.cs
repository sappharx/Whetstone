using System.Collections.Generic;
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
        /// Asynchronously maps an object of type TSource to an object of type TResult by applying the supplied function to it
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TResult">The type of the result object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="transform">The function to apply to the source object</param>
        /// <returns>The result of the provided function applied to the source object</returns>
        /// <remarks>
        /// This method makes the most sense when the transform function is CPU-intensive;
        /// if it's IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// <example>
        /// Parse an integer from a string
        /// <code>
        /// int number = await "123".MapAsync(int.Parse);  // number == 123
        /// </code>
        /// Double a number
        /// <code>
        /// int number = await 12.MapAsync(x => x * 2);    // number == 24
        /// </code>
        /// </example>
        public static async Task<TResult> MapAsync<TSource, TResult>(this TSource source, Func<TSource, TResult> transform)
            => await Task.Run(() => transform(source));

        /// <summary>
        /// Performs an action on an object and returns the object (e.g. writing it to the console or a log)
        /// </summary>
        /// <typeparam name="T">The type of object to be acted upon</typeparam>
        /// <param name="source">The object to be acted upon</param>
        /// <param name="action">The action to perform on the object</param>
        /// <returns>The object that was acted upon</returns>
        /// <example>
        /// Write an object to the console
        /// <code>
        /// "Hello world".Tee(Console.WriteLine);
        /// </code>
        /// </example>
        public static T Tee<T>(this T source, Action<T> action)
        {
            action(source);
            return source;
        }

        /// <summary>
        /// Performs an asynchronous action on an object and returns the object (e.g. writing it to the console or a log)
        /// </summary>
        /// <typeparam name="T">The type of object to be acted upon</typeparam>
        /// <param name="source">The object to be acted upon</param>
        /// <param name="action">The action to perform on the object</param>
        /// <returns>The object that was acted upon</returns>
        /// <remarks>
        /// This method makes the most sense when the action is CPU-intensive;
        /// if it's IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// <example>
        /// Write an object to the console
        /// <code>
        /// await "Hello world".TeeAsync(Console.WriteLine);
        /// </code>
        /// </example>
        public static async Task<T> TeeAsync<T>(this T source, Action<T> action)
        {
            await Task.Run(() => action(source));
            return source;
        }

        /// <summary>
        /// Performs an action on each object in a list and returns the list (e.g. writing items to the console or a log)
        /// </summary>
        /// <typeparam name="T">The type of objects to be acted upon</typeparam>
        /// <param name="source">The list of objects to be acted upon</param>
        /// <param name="action">The action to perform on each object</param>
        /// <returns>The list of objects that were acted upon</returns>
        /// <example>
        /// Write a list of objects to the console
        /// <code>
        /// new[] {1, 2, 3}.TeeEach(Console.WriteLine);
        /// </code>
        /// </example>
        public static IList<T> TeeEach<T>(this IList<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);

            return source;
        }

        /// <summary>
        /// Performs an asynchronous action on each object in a list and returns the list (e.g. writing items to the console or a log)
        /// </summary>
        /// <typeparam name="T">The type of objects to be acted upon</typeparam>
        /// <param name="source">The list of objects to be acted upon</param>
        /// <param name="action">The action to perform on each object</param>
        /// <returns>The list of objects that were acted upon</returns>
        /// <remarks>
        /// This method makes the most sense when the action is CPU-intensive;
        /// if it's IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// <example>
        /// Write a list of objects to the console
        /// <code>
        /// await new[] {1, 2, 3}.TeeEachAsync(Console.WriteLine);
        /// </code>
        /// </example>
        public static async Task<IList<T>> TeeEachAsync<T>(this IList<T> source, Action<T> action)
        {
            foreach (var item in source)
                await Task.Run(() => action(item));

            return source;
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

        /// <summary>
        /// Asynchronously applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate expression to evaluate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <remarks>
        /// This method makes the most sense when the transform function is CPU-intensive;
        /// if it's IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// <example>
        /// Repeat a string if true
        /// <code>
        /// int a = await "abc".WhenAsync(true, s => s + s);   a == "abcabc"
        /// int b = await "abc".WhenAsync(false, s => s + s);  b == "abc"
        /// </code>
        /// </example>
        public static async Task<T> WhenAsync<T>(this T source, bool predicate, Func<T, T> transform)
            => predicate ? await Task.Run(() => transform(source)) : source;

        /// <summary>
        /// Asynchronously applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate function to evaulate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <remarks>
        /// Both the predicate and transform functions will be run asynchronously.
        /// This method makes the most sense when the predicate and/or transform function are CPU-intensive;
        /// if either function is IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// <example>
        /// Double a number if allowed to
        /// <code>
        /// bool isAllowed = true;
        /// int a = await 12.WhenAsync(() => isAllowed, x => x * 2);   a == 24
        /// isAllowed = false;
        /// int b = await 13.WhenAsync(() => isAllowed, x => x * 2);   b == 13
        /// </code>
        /// </example>
        public static async Task<T> WhenAsync<T>(this T source, Func<bool> predicate, Func<T, T> transform)
            => await Task.Run(predicate) ? await Task.Run(() => transform(source)) : source;

        /// <summary>
        /// Asynchronously applies a function to the source object provided the predicate evaluates to true
        /// </summary>
        /// <typeparam name="T">The type of the source object</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="predicate">A predicate function to evaulate to determine if the transformation should take place</param>
        /// <param name="transform">The transformation function to apply to the source object</param>
        /// <returns>
        /// <remarks>
        /// Both the predicate and transform functions will be run asynchronously.
        /// This method makes the most sense when the predicate and/or transform function are CPU-intensive;
        /// if either function is IO-bound, it might make more sense to pass an async function to the sync version of this method.
        /// </remarks>
        /// If the predicate evaluates to true, the result of applying the transformation function to the source object;
        /// otherwise, the source object.
        /// </returns>
        /// <example>
        /// Divide an integer in half only if it is even
        /// <code>
        /// int a = await 12.WhenAsync(x => x % 2 == 0, x => x / 2);   a == 6
        /// int b = await 13.WhenAsync(x => x % 2 == 0, x => x / 2);   b == 13
        /// </code>
        /// </example>
        public static async Task<T> WhenAsync<T>(this T source, Func<T, bool> predicate, Func<T, T> transform)
            => await Task.Run(() => predicate(source)) ? await Task.Run(() => transform(source)) : source;
    }
}
