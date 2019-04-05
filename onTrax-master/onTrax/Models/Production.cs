// ***********************************************************************
// Assembly         : onTrax
// Created          : 03-25-2018
//
// Last Modified On : 04-20-2018
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
    /// Class Production.
    /// One Production run
    /// </summary>
    public class Production
    {
        /// <summary>
        /// Gets or sets the production identifier.
        /// </summary>
        /// <value>The production identifier.</value>
        public Int32 ProductionID { get; set; }

        /// <summary>
        /// Gets or sets the date recorded.
        /// </summary>
        /// <value>The date recorded.</value>
        public DateTime? DateRecorded { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; set; }

        // Server-side input validation 
        // Disabled for testing
        //[Range(1, Double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public Decimal Duration { get; set; }

        // Server-side input validation 
        // Disabled for testing
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public Int32 Quantity { get; set; }

        // Nav properties
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        public virtual Product Product { get; set; }
        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public virtual Process Process { get; set; }
        /// <summary>
        /// Gets or sets the batch.
        /// </summary>
        /// <value>The batch.</value>
        public virtual Batch Batch { get; set; }
        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        public virtual Employee Employee { get; set; }
        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        public virtual List<Issue> Issues { get; set; }
    }
}

