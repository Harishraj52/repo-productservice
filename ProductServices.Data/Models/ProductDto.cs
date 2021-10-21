//-----------------------------------------------------------------------
// <copyright file="ProductDto.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
using FluentValidation;
using ProductService.Data;
using ProductService.Data.Models;

namespace ProductService.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProductService.Data.Entities;

    /// <summary>
    ///  Dto class is used to expose the data to user.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        ///  Gets or sets productid.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        ///  Gets or sets categoryid.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        ///  Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///  Gets or sets price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///  Gets or sets category.
        /// </summary>
        public virtual Category Category { get; set; }
    }
}


