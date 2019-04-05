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
    /// Class Employee.
    /// Identifies specific Employee creating Production
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        public Int32 EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
        public Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>The name of the employee.</value>
        [Required(ErrorMessage = "Employee name is required.")]
        [Display(Name = "Employee")]
		// Server-side input validation 
        [RegularExpression(@"^[a-zA-Z '-]*$", ErrorMessage = "Please enter only letters or spaces in the technician name.")]
        public String EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets the pin.
        /// </summary>
        /// <value>The pin.</value>
        [Required(ErrorMessage = "Employee PIN is required.")]
        [Display(Name = "PIN")]
        public Int32 PIN { get; set; }

        // Nav properties
        /// <summary>
        /// Gets or sets the productions.
        /// </summary>
        /// <value>The productions.</value>
        public virtual List<Production> Productions{ get; set; }

        // Set to active by default when a new Employee object is created
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        public Employee() {
			this.Active = true;
		}
	}
}