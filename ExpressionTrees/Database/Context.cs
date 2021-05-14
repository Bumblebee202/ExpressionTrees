using ExpressionTrees.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ExpressionTrees.Database
{
    public class Context : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public Context()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

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

                bool booleanValue = true;
                if (i % 3 == 0)
                {
                    booleanValue = false;
                }

                int id = i + 1;

                TestEntity entity = new TestEntity
                {
                    ID = id,
                    BooleanValue = booleanValue,
                    Value = value,
                    Date = date,
                    Text = $"{id} {value} {date.ToShortDateString()} {booleanValue}"
                };

                entities.Add(entity);
            }

            modelBuilder.Entity<TestEntity>().HasData(entities);
        }
    }
}
