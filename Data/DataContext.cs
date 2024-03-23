﻿using Microsoft.EntityFrameworkCore;
using PaintStockStatusAPI.Models;

namespace PaintStockStatusAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PaintInventory> PaintInventory { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Define primary key for PaintInventory table
            modelBuilder.Entity<PaintInventory>()
                .HasKey(p => p.Colour);

            // Define primary key for User table
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            // Define primary key for UserRole table
            modelBuilder.Entity<UserRole>()
                .HasKey(r => r.RoleId);

            // Define foreign key relationship between User and UserRole
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            // Define primary key for Order table
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            // Define primary key for OrderItem table
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);

            // Define foreign key relationship between OrderItem and Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);
        }
    }


}
