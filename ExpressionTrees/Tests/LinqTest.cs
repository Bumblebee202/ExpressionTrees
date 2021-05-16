using ExpressionTrees.Database;
using ExpressionTrees.Database.Models;
using System.Linq;

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

            if (filter.ID.HasValue)
            {
                entities = entities.Where(e => e.ID <= filter.ID);
            }

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

            //Show(entities.ToList());
            entities.ToList();

            Stop();
        }
    }
}
