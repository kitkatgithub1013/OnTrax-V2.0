// ***********************************************************************
// Assembly         : onTrax
// Created          : 03-26-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The Models namespace.
/// </summary>
namespace onTrax.Models
{

    /// <summary>
    /// Class ProductProcess.
    /// Associates Products to Processes
    /// Enables the assignment of Standard Durations for a specific Product and Process combination
    /// </summary>
    public class ProductProcess {

        /// <summary>
        /// Gets or sets the product process identifier.
        /// </summary>
        /// <value>The product process identifier.</value>
        public Int32 ProductProcessID { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public Int32 Quantity { get; set; }

        /// <summary>
        /// Gets or sets the duration of the standard.
        /// </summary>
        /// <value>The duration of the standard.</value>
        public Decimal StandardDuration { get; set; }

        // Nav properties 
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public Product Product { get; set; }
        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public Process Process { get; set; }

        // Set default value when a new ProductProcess object is created 
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductProcess"/> class.
        /// </summary>
        public ProductProcess()
        {
			// Quantity is not currently in use
			// but we set it to zero so that if it is implemented in the future there are not null values for old objects
			this.Quantity = 0;
			// Set standard duration to 0 so that a null value is not displayed if an Admin has not specified a value yet 
            this.StandardDuration = 0;
        }
    }
}