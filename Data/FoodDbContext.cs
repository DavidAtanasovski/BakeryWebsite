using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext (DbContextOptions<FoodDbContext> options) : base (options)
        {

        }

        public DbSet<Food> Foods { get; set; }
    }
}
