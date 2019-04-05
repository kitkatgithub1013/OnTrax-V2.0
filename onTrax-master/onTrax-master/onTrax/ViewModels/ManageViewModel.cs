// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-09-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace onTrax.ViewModels
{
    /// <summary>
    /// Class ManageViewModel.
    /// Type: ViewModel
    /// For: /Admin/Manage/
    /// 
    /// Contains lists of all editable models
    /// 
    /// </summary>
    public class ManageViewModel
    {
        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        [Display(Name = "Employees")]
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the processes.
        /// </summary>
        /// <value>The processes.</value>
        [Display(Name = "Processes")]
        public List<Process> Processes { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        [Display(Name = "Products")]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the batches.
        /// </summary>
        /// <value>The batches.</value>
        [Display(Name = "Batches")]
        public List<Batch> Batches { get; set; }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        [Display(Name = "Issues")]
        public List<Issue> Issues { get; set; }

        /// <summary>
        /// Gets or sets the product processes.
        /// </summary>
        /// <value>The product processes.</value>
        public List<ProductProcess> ProductProcesses { get; set; }
    }
}