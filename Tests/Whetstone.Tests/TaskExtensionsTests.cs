using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public void InCompletionOrder_ReturnsSuccessfulTasksInOrder()
        {
            var timeMachine = new TimeMachine();

            var task1 = timeMachine.AddSuccessTask(3, 'a');
            var task2 = timeMachine.AddSuccessTask(1, 'b');
            var task3 = timeMachine.AddSuccessTask(2, 'c');
            var tasks = new List<Task<char>>
            {
                task1,
                task2,
                task3
            };

            foreach (var task in tasks)
            {
                task.IsCompleted.Should().BeFalse("the task hasn't completed yet");
            }

            var inorder = tasks.InCompletionOrder();

            // step 1
            timeMachine.AdvanceTo(1);
            task2.IsCompleted.Should().BeTrue("because the task has completed");

            // step 2
            timeMachine.AdvanceTo(2);
            task3.IsCompleted.Should().BeTrue("because the task has completed");

            // step 3
            timeMachine.AdvanceTo(3);
            task1.IsCompleted.Should().BeTrue("because the task has completed");

            // check order
            using (var enumerator = inorder.GetEnumerator())
            {
                enumerator.MoveNext();
                enumerator.Current.Result.Should().Be('b', "because task2 completed first");
                enumerator.MoveNext();
                enumerator.Current.Result.Should().Be('c', "because task3 completed second");
                enumerator.MoveNext();
                enumerator.Current.Result.Should().Be('a', "because task1 completed last");
            }
        }

        [Test]
        public void InCompletionOrder_HandlesCanceledTasks()
        {
            var timeMachine = new TimeMachine();

            var task1 = timeMachine.AddCancelTask<int>(2);
            var task2 = timeMachine.AddCancelTask<int>(1);
            var tasks = new List<Task<int>>
            {
                task1,
                task2
            };

            foreach (var task in tasks.InCompletionOrder())
            {
                task.IsCompleted.Should().BeFalse("the task hasn't completed yet");
            }

            // step 1
            timeMachine.AdvanceTo(1);
            task2.IsCompleted.Should().BeTrue("because the task has completed");
            task2.IsCanceled.Should().BeTrue("because the task was canceled");

            // step 2
            timeMachine.AdvanceTo(2);
            task1.IsCompleted.Should().BeTrue("because the task has completed");
            task1.IsCanceled.Should().BeTrue("because the task was canceled");
        }

        [Test]
        public void InCompletionOrder_HandlesFaultedTasks()
        {
            var timeMachine = new TimeMachine();

            var task1 = timeMachine.AddFaultingTask<int>(2, new Exception("oops"));
            var task2 = timeMachine.AddFaultingTask<int>(1, new Exception("whoa"));
            var tasks = new List<Task<int>>
            {
                task1,
                task2
            };

            foreach (var task in tasks.InCompletionOrder())
            {
                task.IsCompleted.Should().BeFalse("the task hasn't completed yet");
            }

            // step 1
            timeMachine.AdvanceBy(1);
            task2.IsCompleted.Should().BeTrue("because the task has completed");
            task2.IsFaulted.Should().BeTrue("because the task was canceled");

            // step 2
            timeMachine.AdvanceBy(2);
            task1.IsCompleted.Should().BeTrue("because the task has completed");
            task1.IsFaulted.Should().BeTrue("because the task was canceled");
        }
    }
}
