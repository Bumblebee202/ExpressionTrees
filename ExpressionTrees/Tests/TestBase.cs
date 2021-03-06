using ExpressionTrees.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExpressionTrees.Tests
{
    public abstract class TestBase
    {
        Stopwatch _stopwatch;

        public TestBase() => _stopwatch = new Stopwatch();

        public abstract string TestName { get; }

        protected void Start() => _stopwatch.Start();

        public abstract void Test(Filter filter);

        protected void Stop()
        {
            _stopwatch.Stop();

            TimeSpan timeSpan = _stopwatch.Elapsed;

            string elapsedTime = string.Format("RunTime {0:00}:{1:00}:{2:00}.{3:00}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
            Console.WriteLine(TestName);
            Console.WriteLine(elapsedTime);
            Console.WriteLine($"TotalMilliseconds {timeSpan.TotalMilliseconds}\n\n");
        }

        protected void Show(IEnumerable<TestEntity> source)
        {
            foreach (var item in source)
            {
                Console.WriteLine($"ID: {item.ID}");
            }
        }
    }
}
