using ExpressionTrees.Database;
using ExpressionTrees.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionTrees.Tests
{
    public class LinqTest : TestBase
    {
        public override string TestName => "Linq test";

        public override void Test(Filter filter)
        {
            using Context context = new Context();
            Start();

            IQueryable<TestEntity> entities = context.TestEntities;

            if (filter.BooleanValue.HasValue)
            { 
                entities = entities.Where(e => e.BooleanValue == filter.BooleanValue);
            }

            if (filter.Value.HasValue)
            {
                entities = entities.Where(e => e.Value <= filter.Value);
            }

            if (filter.Date.HasValue)
            {
                entities = entities.Where(e => e.Date < filter.Date);
            }

            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                entities = entities.Where(e => e.Text.Equals(filter.Text));
            }

            entities.ToList();

            Stop();
        }
    }
}
