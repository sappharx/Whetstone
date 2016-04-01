using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whetstone.Tests
{
    // copied (and updated from) http://codeblog.jonskeet.uk/2011/11/25/eduasync-part-17-unit-testing/
    public class TimeMachine
    {
        private readonly SortedList<int, Action> _actions = new SortedList<int, Action>();

        public int CurrentTime { get; private set; }

        public void AdvanceBy(int time)
        {
            AdvanceTo(CurrentTime + time);
        }

        public void AdvanceTo(int time)
        {
            foreach (var entry in _actions.Where(entry => entry.Key > CurrentTime && entry.Key <= time))
            {
                entry.Value();
            }
            CurrentTime = time;
        }

        public Task<T> AddSuccessTask<T>(int time, T result)
        {
            var tcs = new TaskCompletionSource<T>();
            _actions[time] = () => tcs.SetResult(result);
            return tcs.Task;
        }

        public Task<T> AddCancelTask<T>(int time)
        {
            var tcs = new TaskCompletionSource<T>();
            _actions[time] = () => tcs.SetCanceled();
            return tcs.Task;
        }

        public Task<T> AddFaultingTask<T>(int time, Exception e)
        {
            var tcs = new TaskCompletionSource<T>();
            _actions[time] = () => tcs.SetException(e);
            return tcs.Task;
        }
    }
}
