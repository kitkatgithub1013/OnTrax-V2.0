// ***********************************************************************
// Assembly         : onTrax
// Created          : 03-25-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The Models namespace.
/// </summary>
namespace onTrax.Models
{

    /// <summary>
    /// Class Product.
    /// Identifies Product related to Production
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public Int32 ProductID { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>The name of the product.</value>
        [Required(ErrorMessage = "Product name is required.")]
        [Display(Name = "Product")]
		// Server-side input validation 
		[RegularExpression(@"^[a-zA-Z0-9 -]*$", ErrorMessage = "Invalid characters in Product name.")]
        public String ProductName { get; set; }

        // Nav properties
        /// <summary>
        /// Gets or sets the productions.
        /// </summary>
        /// <value>The productions.</value>
        public virtual List<Production> Productions { get; set; }
        /// <summary>
        /// Gets or sets the processes.
        /// </summary>
        /// <value>The processes.</value>
        public virtual List<ProductProcess> Processes { get; set; }

        // Set to active by default when a new Product object is created 
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product() {
			this.Active = true;
		}
	}
}


        