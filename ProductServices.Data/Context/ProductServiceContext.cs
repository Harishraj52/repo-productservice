//-----------------------------------------------------------------------
// <copyright file="ProductServiceContext.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
namespace ProductService.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using ProductService.Data;
    using ProductService.Data.Entities;

    /// <summary>
    ///  This class communicates with server.
    /// </summary>
    public class ProductServiceContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceContext"/> class.
        /// </summary>
        public ProductServiceContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceContext"/> class.
        /// </summary>
        /// <param name="options">instance for dbcontext.</param>
        public ProductServiceContext(DbContextOptions<ProductServiceContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets products values.
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets categories values.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// configures the database provider.
        /// </summary>
        /// <param name="optionsBuilder">instance for dbcontextoptionsbuilder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ProductDb");
            }
        }

        /// <summary>
        /// This method applies user defined conventions to entities.
        /// </summary>
        /// <param name="modelBuilder">instance for modelbuilder class.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(i => i.CategoryId);
            modelBuilder.Entity<Category>().Property(i => i.Name).IsRequired().HasMaxLength(70);


            // modelBuilder.Entity<Category>().HasData(
            // new Category { CategoryId =newGuid(), Name ="Tv"});
            modelBuilder.Entity<Product>().HasKey(i => i.ProductId);
            modelBuilder.Entity<Product>().Property(i => i.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(i => i.Description).HasMaxLength(512);
            modelBuilder.Entity<Product>().Property(i => i.Brand).IsRequired();
            modelBuilder.Entity<Product>().Property(i => i.Price).IsRequired();
            modelBuilder.Entity<Product>().HasOne(i => i.Category).WithMany(j => j.Products);
        }
    }
}