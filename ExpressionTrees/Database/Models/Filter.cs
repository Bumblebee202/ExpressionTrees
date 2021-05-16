using System;

namespace ExpressionTrees.Database.Models
{
    public class Filter
    {
        public int? ID { get; set; }
        public bool? BooleanValue { get; set; }
        public int? Value { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
    }
}
