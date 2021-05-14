using ExpressionTrees.Database.Models;
using ExpressionTrees.Tests;
using System;

namespace ExpressionTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            Filter filter = new Filter
            {
                Value = 5000,
                Date = DateTime.Now,
            };

            Test1(filter);
        }

        static void Test1(Filter filter)
        {
            TestBase test = new LinqTest();
            test.Test(filter);
        }
    }
}
