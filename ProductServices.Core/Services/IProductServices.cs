//-----------------------------------------------------------------------
// <copyright file="IProductServices.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------

namespace ProductService.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///  definition for product service.
    /// </summary>
    public interface IProductServices
    {
        /// <summary>
        ///  gets all the products.
        /// </summary>
        /// <returns><IEnumerable>list of products<see cref="Task"/> representing the asynchronous operation.</IEnumerable></returns>
        public Task<IEnumerable<ProductDto>> GetallProducts();

        /// <summary>
        /// gets the product with id.
        /// </summary>
        /// <param name="productId">gets the product.</param>
        /// <returns>product<see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductDto> GetProduct(Guid productId);

        /// <summary>
        /// saves the product.
        /// </summary>
        /// <param name="product">stores the new product.</param>
        /// <returns>saved product <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductDto> SaveProduct(ProductDto product);

        /// <summary>
        /// updates the existing product.
        /// </summary>
        /// <param name="productId">stores the product id.</param>
        /// <param name="product">stores the updated product.</param>
        /// <returns>updated product <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<ProductDto> UpdateProduct(Guid productId, ProductDto product);

        /// <summary>
        /// deletes the product with id.
        /// </summary>
        /// <param name="productId">id to delete item.</param>
        /// <returns>product <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Product> DeleteProduct(Guid productId);
    }
}
