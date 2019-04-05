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
    /// Class Issue.
    /// Identifies one or more optional quality Issues found in Production
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the issue identifier.
        /// </summary>
        /// <value>The issue identifier.</value>
        public Int32 IssueID { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the name of the issue.
        /// </summary>
        /// <value>The name of the issue.</value>
        [Required(ErrorMessage = "Issue name is required.")]
        [Display(Name = "Issue")]
		// Server-side input validation 
        [RegularExpression(@"^[a-zA-Z0-9 -]*$", ErrorMessage = "Invalid characters in Issue name.")]
        public String IssueName { get; set; }

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
        public virtual List<Process> Processes { get; set; }

        // Set to active by default when a new Issue object is created 
        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> class.
        /// </summary>
        public Issue() {
			this.Active = true;
		}
	}
}