namespace System.Collections.Generic
{
    /// <summary>
    /// Wraps a <see cref="ICollection{T}"/> object and overloads certain manipulation methods to provide a fluent interface
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class FluentCollection<T> : ICollection<T>
    {
        private ICollection<T> _collection;

        /// <summary>
        /// Initializes a new <see cref="FluentCollection{T}"/> object
        /// </summary>
        /// <param name="collection">The <see cref="ICollection{T}"/> object to wrap</param>
        public FluentCollection(ICollection<T> collection)
        {
            _collection = collection;
        }

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item">The object to add to the collection</param>
        /// <returns>The collection object itself</returns>
        public FluentCollection<T> Add(T item)
        {
            _collection.Add(item);
            return this;
        }

        /// <summary>
        /// Removes all items from the collection
        /// </summary>
        /// <returns>The collection object itself</returns>
        public FluentCollection<T> Clear()
        {
            _collection.Clear();
            return this;
        }

        #region ICollection<T> members

        /// <summary>
        /// Gets the number of elements contained in the collection
        /// </summary>
        public int Count
        {
            get { return _collection.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only
        /// </summary>
        public bool IsReadOnly
        {
            get { return _collection.IsReadOnly; }
        }

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item">The object to add to the collection</param>
        void ICollection<T>.Add(T item)
        {
            _collection.Add(item);
        }

        /// <summary>
        /// Removes all items from the collection
        /// </summary>
        void ICollection<T>.Clear()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains a specific value
        /// </summary>
        /// <param name="item">The object to locate in the object</param>
        /// <returns>True if item is found in the collection; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to a <see cref="System.Array"/> starting at a particular index
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the collection.
        /// The array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurence of a specific object from the collection
        /// </summary>
        /// <param name="item">The object to remove from the collection</param>
        /// <returns>
        /// True if item was successfully removed from the collection; otherwise, false.
        /// This method also returns false if item is not found in the original collection.
        /// </returns>
        public bool Remove(T item)
        {
            return _collection.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator"/> that can be used to iterate through the collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator() as IEnumerator;
        } 

        #endregion
    }
}
