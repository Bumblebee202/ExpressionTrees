using ExpressionTrees.Database;
using ExpressionTrees.Database.Models;
using System;
using System.Linq;

namespace ExpressionTrees.Tests
{
    public class ExpressionTreeTest : TestBase
    {
        public override string TestName => "ExpressionTreeTest";

        public override void Test(Filter filter)
        {
            //using Context context = new Context();
            Start();

            //IQueryable<TestEntity> entities = context.TestEntities;

            //entities.ToList();

            Stop();
        }
    }
}
