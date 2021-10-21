//-----------------------------------------------------------------------
// <copyright file="AutoMapperProfile.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
namespace ProductService.Core.Mappers
{
    using AutoMapper;
    using ProductService.Data.Entities;
    using ProductService.Data.Models;

    /// <summary>
    ///  Profile class is used to create the mapping between entities.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            this.CreateMap<Category, CategoryDto>();
            this.CreateMap<Product, ProductDto>();
            this.CreateMap<CategoryDto, Category>();
            this.CreateMap<ProductDto, Product>();
        }
    }
}
