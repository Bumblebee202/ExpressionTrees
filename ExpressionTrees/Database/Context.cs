using ExpressionTrees.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTrees.Database
{
    public class Context : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public Context() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExpressionTrees;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int size = 10000;
            List<TestEntity> entities = new List<TestEntity>(size);

            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                int? value = random.Next(-1, size + 1);
                value = value != -1 ? value : null;

                int day = random.Next(1, 29);
                int month = random.Next(1, 13);

                DateTime date = new DateTime(2021, month, day);

                bool booleanValue = random.Next(0, 11) < 5;

                TestEntity entity = new TestEntity
                {
                    ID = i + 1,
                    BooleanValue = booleanValue,
                    Value = value,
                    Date = date,
                    Text = $"{value} {date}"
                };

                entities.Add(entity);
            }

            modelBuilder.Entity<TestEntity>().HasData(entities);
        }
    }
}
