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
    /// Class Process.
    /// Identifies Process for Production run
    /// </summary>
    public class Process
    {
        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>The process identifier.</value>
        public Int32 ProcessID { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        /// <value>The name of the process.</value>
        [Required(ErrorMessage = "Process name is required.")]
        [Display(Name = "Process")]
		// Server-side input validation 
		[RegularExpression(@"^[a-zA-Z0-9 -]*$", ErrorMessage = "Invalid characters in Process name.")]
		public String ProcessName { get; set; }

        // Nav properties
        /// <summary>
        /// Gets or sets the productions.
        /// </summary>
        /// <value>The productions.</value>
        public virtual List<Production> Productions { get; set; }
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public virtual List<ProductProcess> Products { get; set; }
        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        public virtual List<Issue> Issues { get; set; }

        // Set to active by default when a new Process object is created 
        /// <summary>
        /// Initializes a new instance of the <see cref="Process"/> class.
        /// </summary>
        public Process() {
			this.Active = true;
		}
    }
}