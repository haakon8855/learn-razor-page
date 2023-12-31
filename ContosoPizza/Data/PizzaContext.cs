﻿using ContosoPizza.Models;
using ContosoPizza.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoPizza.Models.Pizza>? Pizzas { get; set; }
        public DbSet<ContosoPizza.Models.Sauce>? Sauces { get; set; }
        public DbSet<ContosoPizza.Models.Topping>? Toppings { get; set; }
        public DbSet<ContosoPizza.Models.Soda>? Sodas { get; set; }
        public DbSet<ContosoPizza.Models.Order>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This is where we configure the value objects to be stored as strings
            // and how to convert them back to value objects when we read from the database.
            modelBuilder.Entity<Pizza>()
                .Property(p => p.Name)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => v == null ? null : new ProductName(v)
                );
            modelBuilder.Entity<Sauce>()
                .Property(s => s.Name)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => v == null ? null : new ProductName(v)
                );
            modelBuilder.Entity<Topping>()
                .Property(t => t.Name)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => v == null ? null : new ProductName(v)
                );
            modelBuilder.Entity<Soda>()
                .Property(s => s.Name)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => v == null ? null : new ProductName(v)
                );
            modelBuilder.Entity<Order>()
                .Property(s => s.CustomerName)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => v == null ? null : new CustomerName(v)
                );
            modelBuilder.Entity<Sauce>()
                .Property(s => s.Calories)
                .HasConversion(
                    v => v == null ? 0 : v.ToDecimal(),
                    v => new Calories(v)
                );
            modelBuilder.Entity<Soda>()
                .Property(s => s.Calories)
                .HasConversion(
                    v => v == null ? 0 : v.ToDecimal(),
                    v => new Calories(v)
                );
        }
    }
}
