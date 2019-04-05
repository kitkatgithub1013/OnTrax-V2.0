// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-10-2018
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
    /// Class UpdateDataViewModel.
    /// Type: ViewModel
    /// For: /Admin/Data/Update
    /// 
    /// Contains Production to update
    /// Contains lists of all selectable models
    /// Contains Selected Issues from form
    /// 
    /// </summary>
    public class UpdateDataViewModel
    {
        /// <summary>
        /// Gets or sets the production.
        /// </summary>
        /// <value>The production.</value>
        public Production Production { get; set; }

        /// <summary>
        /// Gets or sets the employee list.
        /// </summary>
        /// <value>The employee list.</value>
        [Display(Name ="Employees")]
        public List<Employee> EmployeeList { get; set; }

        /// <summary>
        /// Gets or sets the process list.
        /// </summary>
        /// <value>The process list.</value>
        [Display(Name = "Processes")]
        public List<Process> ProcessList { get; set; }

        /// <summary>
        /// Gets or sets the product list.
        /// </summary>
        /// <value>The product list.</value>
        [Display(Name = "Products")]
        public List<Product> ProductList { get; set; }

        /// <summary>
        /// Gets or sets the batch list.
        /// </summary>
        /// <value>The batch list.</value>
        [Display(Name = "Batches")]
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
    }
}