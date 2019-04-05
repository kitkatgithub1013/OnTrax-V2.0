// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-18-2018
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
    /// Class EditProcessViewModel.
    /// Type: ViewModel
    /// For: Admin/Process/Edit/1
    /// 
    /// Contains individual Process
    /// Contains lists of all Issues
    /// Contains String array of selected Issues for Process
    /// 
    /// </summary>
    public class EditProcessViewModel
    {
        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public Process Process { get; set; }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        [Display(Name = "Issues")]
        public List<Issue> Issues { get; set; }

        /// <summary>
        /// Gets or sets the selected issues.
        /// </summary>
        /// <value>The selected issues.</value>
        public String[] SelectedIssues { get; set; }
    }
}