namespace System
{
    /// <summary>
    /// Provides extension methods for Uri objects
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the portion of the uri before the query delimiter
        /// </summary>
        /// <param name="original">The full uri to get the base uri from</param>
        /// <returns>The uri without the query parameters and query delimiter</returns>
        public static Uri GetBaseUri(this Uri original)
            => original
                .Map(uri => uri.AbsoluteUri.IndexOf("?", StringComparison.Ordinal))
                .Map(index => index < 0 ? original : new Uri(original.AbsoluteUri.Substring(0, index)));

        /// <summary>
        /// Gets the query string portion of a uri without the leading query delimiter ("?")
        /// </summary>
        /// <param name="uri">The full uri to get the query string from</param>
        /// <returns>The query string portion of the uri without the query delimiter</returns>
        public static string QueryStringWithoutQueryDelimiter(this Uri uri)
            => uri.Query.Length > 1 ? uri.Query.Substring(1) : string.Empty;

    }
}
