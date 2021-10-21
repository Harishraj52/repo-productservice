//-----------------------------------------------------------------------
// <copyright file="CategoryServices.cs" company="Techwave">
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
    using ProductService.Data;
    using ProductService.Data.Context;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///   This class implements the category model.
    /// </summary>
    public class CategoryServices : ICategoryServices
    {
        private readonly ProductServiceContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryServices"/> class.
        /// </summary>
        /// <param name="context">instance to communicate with context class.</param>
        /// <param name="mapper">The mapper.</param>
        public CategoryServices(ProductServiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get method gets all the categories.
        /// </summary>
        /// <returns>
        /// Returns list of categories.
        /// </returns>
        public async Task<IEnumerable<CategoryDto>> GetallCategories()
        {
            var allCategories = await this.context.Categories.ToListAsync();

            // .Include(i => i.Products)
            return this.mapper.Map<IEnumerable<CategoryDto>>(allCategories);
        }

        /// <summary>
        /// This method gets the category with an Id.
        /// </summary>
        /// <param name="categoryId">The id to get category.</param>
        /// <returns>The single category.</returns>
        public async Task<CategoryDto> GetCategory(Guid categoryId)
        {
            var category = await this.context.Categories.FindAsync(categoryId);
            return this.mapper.Map<CategoryDto>(category);
        }

        /// <summary>
        /// This method saves the new product .
        /// </summary>
        /// <param name="category">The category to save category.</param>
        /// <returns>The posted category.</returns>
        public async Task<CategoryDto> SaveCategory(CategoryDto category)
        {
            var abc = this.mapper.Map<Category>(category);
            this.context.Categories.Add(abc);
            await this.context.SaveChangesAsync();
            return this.mapper.Map<CategoryDto>(abc);
        }

        /// <summary>
        /// This method updates the existing category.
        /// </summary>
        /// <param name="categoryId">The id to save category.</param>
        /// <param name="category">The category to save category.</param>
        /// <returns>The updated category with 201 status code.</returns>
        /// <returns>The updated category.</returns>
        public async Task<CategoryDto> UpdateCategory(Guid categoryId, CategoryDto category)
        {
            var newCategory=this.mapper.Map<Category>(category);
            this.context.Entry(newCategory).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return this.mapper.Map<CategoryDto>(newCategory);
        }

        /// <summary>
        /// This method deletes the existing category.
        /// </summary>
        /// <param name="categoryId">The id to delete category.</param>
        /// <returns>The deleted.</returns>
        public async Task<Category> DeleteCategory(Guid categoryId)
        {
            var deleted = this.context.Categories.Find(categoryId);
            this.context.Categories.Remove(deleted);
            await this.context.SaveChangesAsync();
            return deleted;
        }
    }
}
