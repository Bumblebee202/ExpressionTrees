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
                ID = 1000,
                //BooleanValue = null,
                //Value = 10,
                //Date = DateTime.Now,
            };
            Test1(filter);
            Test2(filter);
            Test1(filter);
        }

        static void Test1(Filter filter)
        {
            TestBase test = new LinqTest();
            test.Test(filter);
        }

        static void Test2(Filter filter)
        {
            TestBase test = new ExpressionTreeTest();
            test.Test(filter);
        }
    }
}
