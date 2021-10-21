//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------

namespace ProductService.Data.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///  Dto class is used to expose the data to user.
    /// </summary>
    public class Category
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
