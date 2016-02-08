namespace System
{
    /// <summary>
    /// Provides extension methods for integers
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the value subject to the provided minimum and maximum constraints
        /// </summary>
        /// <param name="value">The input value to bound</param>
        /// <param name="min">The minimum value allowed</param>
        /// <param name="max">The maximum value allowed</param>
        /// <returns>The value subjected to the minimum and maximum values allowed</returns>
        public static int GetBoundedValue(this int value, int min, int max)
            => Math.Min(Math.Max(value, min), max);

        /// <summary>
        /// Returns the value (if it's null, use the provided default) subject to the provided minimum constraint
        /// </summary>
        /// <param name="value">The input value to bound</param>
        /// <param name="defaultValue">The value to use if the input value is null</param>
        /// <param name="min">The minimum value allowed</param>
        /// <returns>The value (or default) subjected to the minimum values allowed</returns>
        public static int GetBoundedValue(this int? value, int defaultValue, int min)
            => Math.Max(value ?? defaultValue, min);

        /// <summary>
        /// Returns the value (if it's null, use the provided default) subject to the provided minimum and maximum constraints
        /// </summary>
        /// <param name="value">The input value to bound</param>
        /// <param name="defaultValue">The value to use if the input value is null</param>
        /// <param name="min">The minimum value allowed</param>
        /// <param name="max">The maximum value allowed</param>
        /// <returns>The value (or default) subjected to the minimum and maximum values allowed</returns>
        public static int GetBoundedValue(this int? value, int defaultValue, int min, int max)
            => GetBoundedValue(value ?? defaultValue, min, max);
    }
}
