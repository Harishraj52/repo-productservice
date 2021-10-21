//-----------------------------------------------------------------------
// <copyright file="CategoryDto.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------
using FluentValidation;
using ProductService.Data;

namespace ProductService.Data.Models
{
    using ProductService.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///  Dto class is used to expose the data to user.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        ///  Gets or sets categoryid.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        ///  Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Gets or sets collection of products.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }

  
}
