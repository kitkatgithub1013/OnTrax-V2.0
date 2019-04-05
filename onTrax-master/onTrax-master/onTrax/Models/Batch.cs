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
    /// Class Batch.
    /// Identifies specific manufacturer Batch for Production
    /// </summary>
    public class Batch
    {
        /// <summary>
        /// Gets or sets the batch identifier.
        /// </summary>
        /// <value>The batch identifier.</value>
        public Int32 BatchID { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>The name of the batch.</value>
        [Required(ErrorMessage = "Batch name is required.")]
        [Display(Name = "Batch")]
		// Server-side input validation
        [RegularExpression(@"^[a-zA-Z0-9 -]*$", ErrorMessage = "Invalid characters in Batch name.")]
        public String BatchName { get; set; }

        // Nav properties
        /// <summary>
        /// Gets or sets the productions.
        /// </summary>
        /// <value>The productions.</value>
        public virtual List<Production> Productions { get; set; }

        // Set to active by default when a new Batch object is created
        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        public Batch() {
			this.Active = true;
		}
	}
}