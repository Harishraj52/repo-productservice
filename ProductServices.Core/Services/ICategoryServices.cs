//-----------------------------------------------------------------------
// <copyright file="ICategoryServices.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
namespace ProductService.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProductService.Data;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///  definition for category service.
    /// </summary>
    public interface ICategoryServices
    {
        /// <summary>
        ///  gets all the categories.
        /// </summary>
        /// <returns><IEnumerable>list of categories<see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public Task<IEnumerable<CategoryDto>> GetallCategories();

        /// <summary>
        /// gets the category with id.
        /// </summary>
        /// <param name="categoryId">gets the category.</param>
        /// <returns>category<see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<CategoryDto> GetCategory(Guid categoryId);

        /// <summary>
        /// saves the category.
        /// </summary>
        /// <param name="category">stores the new category.</param>
        /// <returns>saved category <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<CategoryDto> SaveCategory(CategoryDto category);

        /// <summary>
        /// updates the existing category.
        /// </summary>
        /// <param name="categoryId">stores the category id.</param>
        /// <param name="category">stores the updated category.</param>
        /// <returns>updated category <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<CategoryDto> UpdateCategory(Guid categoryId, CategoryDto category);

        /// <summary>
        /// deletes the category with id.
        /// </summary>
        /// <param name="categoryid">id to delete item.</param>
        /// <returns>category <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Category> DeleteCategory(Guid categoryid);
    }
}
