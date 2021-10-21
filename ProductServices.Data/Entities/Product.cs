//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="Techwave">
// Copyright (c) Techwave. All rights reserved.
// </copyright>
// <author>Harishraj Biruduraju</author>
//-----------------------------------------------------------------------

namespace ProductService.Data.Entities
{
    using System;

    /// <summary>
    ///   This class defines the product model.
    /// </summary>
    public class Product
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
