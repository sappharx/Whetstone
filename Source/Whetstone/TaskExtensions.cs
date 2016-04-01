using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides extension methods for <see cref="Task"/>
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Returns a sequence of <see cref="Task{T}"/> in the order in which they complete
        /// </summary>
        /// <typeparam name="T">The type of results produces by the tasks</typeparam>
        /// <param name="source">The original tasks</param>
        /// <returns>
        /// The tasks in the order in which they complete
        /// </returns>
        public static IEnumerable<Task<T>> InCompletionOrder<T>(this IEnumerable<Task<T>> source)
        {
            var inputs = source.ToList();
            var boxes = inputs.Select(x => new TaskCompletionSource<T>()).ToList();

            var currentIndex = -1;
            foreach (var task in inputs)
            {
                task.ContinueWith(completed =>
                {
                    var nextBox = boxes[Interlocked.Increment(ref currentIndex)];
                    PropogateResult(completed, nextBox);
                }, TaskContinuationOptions.ExecuteSynchronously);
            }

            return boxes.Select(box => box.Task);
        }

        private static void PropogateResult<T>(Task<T> completedTask, TaskCompletionSource<T> completionSource)
        {
            switch (completedTask.Status)
            {
                case TaskStatus.Canceled:
                    completionSource.TrySetCanceled();
                    break;
                case TaskStatus.Faulted:
                    Debug.Assert(completedTask.Exception != null, "completedTask.Exception != null");
                    completionSource.TrySetException(completedTask.Exception.InnerExceptions);
                    break;
                case TaskStatus.RanToCompletion:
                    completionSource.TrySetResult(completedTask.Result);
                    break;
                default:
                    // don't currently know how to force this in a test, thus it isn't covered in any tests
                    throw new ArgumentException("Task was not completed", nameof(completedTask));
            }
        }
    }
}
