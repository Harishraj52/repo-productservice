//-----------------------------------------------------------------------
// <copyright file="CategoriesController.cs" company="Techwave">
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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using ProductService.Core;
    using ProductService.Core.Services;
    using ProductService.Data;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///  This class has Categories controller for all crud operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices service;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name=service">The service repository.</param>
        public CategoriesController(ICategoryServices service, ILogger<CategoriesController> logger, IConfiguration configuration)
        {
            this.service = service;
            this._logger = logger;
            this._configuration = configuration;
        }

        /// <summary>
        /// Get method gets all the categories.
        /// </summary>
        /// <returns>
        /// Returns list of categories.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get()
        {
             var iteration = 1;
            _logger.LogDebug($"Debug {iteration},Get controller is called");
            _logger.LogInformation($"Information {iteration},Get controller is called");
            _logger.LogWarning($"Warning {iteration}");
            _logger.LogError($"Error {iteration}");
            _logger.LogCritical($"Critical {iteration},Get controller is called");
             var allCategories = await this.service.GetallCategories();
             return allCategories;
        }

        /// <summary>
        /// This method gets the category with an Id.
        /// </summary>
        /// <param name="id">The id to get category.</param>
        /// <returns>The Notfound.</returns>
        /// <returns>The single category.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
             var category = await this.service.GetCategory(id);
             if (category == null)
            {
                return this.NotFound();
            }

             return this.Ok(category);
        }

        /// <summary>
        /// This method saves the new product .
        /// </summary>
        /// <param name="category">The category to save category.</param>
        /// <returns>The posted category with 201 status code.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto category)
        {
            // category.CategoryId = Guid.NewGuid();
                var savedCategory = await this.service.SaveCategory(category);
                ITopicClient topicClient = new TopicClient(_configuration["TopicConnectionString"], _configuration["TopicName"]);
                var categoryJSON = JsonConvert.SerializeObject(savedCategory);
                var categoryMessage = new Message(Encoding.UTF8.GetBytes(categoryJSON))
                {
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = "application/json"
                };
                await topicClient.SendAsync(categoryMessage).ConfigureAwait(false);
                return this.StatusCode(201, savedCategory);
               
         }
           

        /// <summary>
        /// This method updates the existing category.
        /// </summary>
        /// <param name="id">The id to save category.</param>
        /// <param name="category">The category to save category.</param>
        /// <returns>The updated category with 201 status code.</returns>
        /// <returns>The updated category.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoryDto category)
        {
           var updated = await this.service.UpdateCategory(id, category);
           return this.StatusCode(201, updated);
        }

        /// <summary>
        /// This method deletes the existing category.
        /// </summary>
        /// <param name="id">The id to delete category.</param>
        /// <returns>The NoContent.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.service.DeleteCategory(id);
            return this.NoContent();
        }
    }
}
