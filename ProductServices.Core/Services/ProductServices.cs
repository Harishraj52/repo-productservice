//-----------------------------------------------------------------------
// <copyright file="ProductServices.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
namespace ProductService.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using ProductService.Data.Context;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///   This class implements the product model.
    /// </summary>
    public class ProductServices : IProductServices
    {
        private readonly ProductServiceContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServices"/> class.
        /// </summary>
        /// <param name="context">instance to communicate with context class.</param>
        /// <param name="mapper">The mapper.</param>
        public ProductServices(ProductServiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// This method gets the category with an Id.
        /// </summary>
        /// <param name="productid">The id to delete product.</param>
        /// <returns>The deleted product.</returns>
        public async Task<Product> DeleteProduct(Guid productid)
        {
            var deleted = this.context.Products.Find(productid);
            this.context.Products.Remove(deleted);
            await this.context.SaveChangesAsync();
            return deleted;
        }

        /// <summary>
        /// This method gets all the products.
        /// </summary>
        /// <returns>All products.</returns>
        public async Task<IEnumerable<ProductDto>> GetallProducts()
        {
            var products = await this.context.Products.ToListAsync();
            return this.mapper.Map<IEnumerable<ProductDto>>(products);
        }

        /// <summary>
        /// This method gets the product.
        /// </summary>
        /// <param name="productid">The id to get product.</param>
        /// <returns>single product.</returns>
        public async Task<ProductDto> GetProduct(Guid productid)
        {
            var product = await this.context.Products.FindAsync(productid);
            return this.mapper.Map<ProductDto>(product);
        }

        /// <summary>
        /// This method saves the product.
        /// </summary>
        /// <param name="product">Saves the new product.</param>
        /// <returns>single saved product.</returns>
        public async Task<ProductDto> SaveProduct(ProductDto product)
        {
            var abc = this.mapper.Map<Product>(product);

           // product.ProductId = Guid.NewGuid();
            this.context.Products.Add(abc);
            await this.context.SaveChangesAsync();
            return this.mapper.Map<ProductDto>(abc);
        }

        /// <summary>
        /// This method updates the product.
        /// </summary>
        /// <param name="productid">takes the productid.</param>
        /// <param name="product">updates the existing product.</param>
        /// <returns>updated product.</returns>
        public async Task<ProductDto> UpdateProduct(Guid productid, ProductDto product)
        {
            var newProduct = this.mapper.Map<Product>(product);
            this.context.Entry(newProduct).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return this.mapper.Map<ProductDto>(newProduct);
        }
    }
}
