// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-17-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace onTrax.ViewModels
{

    /// <summary>
    /// Class LogDataViewModel.
    /// Type: ViewModel
    /// For: Admin/Data/Log
    /// 
    /// Contains StartTime, Duration and Quantity for Production
    /// Contains lists of all selectable models
    /// Contains individual model IDs as selected attributes
    /// 
    /// </summary>
    public class LogDataViewModel
    {
        /// <summary>
        /// Gets the date recorded.
        /// </summary>
        /// <value>The date recorded.</value>
        public DateTime DateRecorded
        {
            get { return DateTime.Now; }

        }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>


        [Display(Name = "Date")]
        [StringLength(10, MinimumLength = 9)]
        public String StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        [Range(0.10, Double.MaxValue, ErrorMessage = "Please enter a value of at least {1}")]
        [Display(Name = "Hours")]
        //[RegularExpression(@"^[0-9 .]*$", ErrorMessage = "Duration must be a vaild decimal.")]
        public Decimal Duration { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value of at least {1}")]
        [Display(Name = "Quantity")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Quantity must be a valid integer.")]
        public Int32 Quantity { get; set; }

        /// <summary>
        /// Gets or sets the employee list.
        /// </summary>
        /// <value>The employee list.</value>
        [Display(Name ="Employee")]
        public List<Employee> EmployeeList { get; set; }

        /// <summary>
        /// Gets or sets the process list.
        /// </summary>
        /// <value>The process list.</value>
        [Display(Name = "Process")]
        public List<Process> ProcessList { get; set; }

        /// <summary>
        /// Gets or sets the product list.
        /// </summary>
        /// <value>The product list.</value>
        [Display(Name = "Product")]
        public List<Product> ProductList { get; set; }

        /// <summary>
        /// Gets or sets the batch list.
        /// </summary>
        /// <value>The batch list.</value>
        [Display(Name = "Batch")]
        public List<Batch> BatchList { get; set; }

        /// <summary>
        /// Gets or sets the issue list.
        /// </summary>
        /// <value>The issue list.</value>
        [Display(Name = "Issues")]
        public List<Issue> IssueList { get; set; }

        /// <summary>
        /// Gets or sets the selected issues.
        /// </summary>
        /// <value>The selected issues.</value>
        public String[] SelectedIssues { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        public Int32 EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public Int32 ProductID { get; set; }

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>The process identifier.</value>
        public Int32 ProcessID { get; set; }

        /// <summary>
        /// Gets or sets the batch identifier.
        /// </summary>
        /// <value>The batch identifier.</value>
        public Int32 BatchID { get; set; }

    }
}