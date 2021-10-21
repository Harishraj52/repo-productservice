//-----------------------------------------------------------------------
// <copyright file="ProductsController.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ProductServices.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using ProductService.Core;
    using ProductService.Core.Services;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///  This class has products controller for all crud operations.
    /// </summary>
    [Route("api/[controller]")]

    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices service;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name=service">The service repository.</param>
        public ProductsController(IProductServices service, IConfiguration configuration)
        {
            this.service = service;
            this._configuration = configuration;
        }

        // GET: api/<ProductsController>

        /// <summary>
        /// Get method gets all the products.
        /// </summary>
        /// <returns>
        /// Returns list of products.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var allProducts = await this.service.GetallProducts();
            return allProducts;
        }

        /// <summary>
        /// This method gets the product with an Id.
        /// </summary>
        /// <param name="id">The id to get product.</param>
        /// <returns>The Notfound.</returns>
        /// <returns>The single product.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await this.service.GetProduct(id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.Ok(product);
        }

        /// <summary>
        /// This method saves the new product .
        /// </summary>
        /// <param name="product">The product to save product.</param>
        /// <returns>The posted product with 201 status code.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
           // product.ProductId = Guid.NewGuid();
            var postedProduct = await this.service.SaveProduct(product);
            ITopicClient topicClient = new TopicClient(_configuration["TopicConnectionString"], _configuration["TopicName"]);
            var productJSON = JsonConvert.SerializeObject(postedProduct);
            var productMessage = new Message(Encoding.UTF8.GetBytes(productJSON))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };
            await topicClient.SendAsync(productMessage).ConfigureAwait(false);
            return this.StatusCode(201, postedProduct);
        }

        /// <summary>
        /// This method updates the existing product.
        /// </summary>
        /// <param name="id">The id to save product.</param>
        /// <param name="product">The product to save product.</param>
        /// <returns>The updated product with 201 status code.</returns>
        /// <returns>The updated product.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductDto product)
        {
            var updated = await this.service.UpdateProduct(id, product);
            return this.StatusCode(201, updated);
        }

        /// <summary>
        /// This method deletes the existing product.
        /// </summary>
        /// <param name="id">The id to delete product.</param>
        /// <returns>The NoContent .</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await this.service.DeleteProduct(id);

            return this.NoContent();
        }
    }
}
