// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-05-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Models;
using onTrax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// The ViewModels namespace.
/// </summary>
namespace onTrax.ViewModels
{

    /// <summary>
    /// Class RunProductionViewModel.
    /// Type: ViewModel
    /// For: /Production/Run
    /// 
    /// Contains checked-in Employee
    /// Contains lists of all selectable models
    /// Contains dictionary of process to issue combinations
    /// 
    /// </summary>
    public class RunProductionViewModel
    {
        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        [Display(Name ="Employee")]
        public Employee Employee { get; set; }

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

        /// <summary>
        /// Gets or sets the process issue dictionary.
        /// </summary>
        /// <value>The process issue dictionary.</value>
        public Dictionary<Int32, List<Issue>> ProcessIssueDict { get; set; }

        /// <summary>
        /// Generates the process issue dictionary.
        /// </summary>
        /// <returns>Dictionary&lt;Int32, List&lt;Issue&gt;&gt;.</returns>
        public Dictionary<Int32, List<Issue>> GenerateProcessIssueDict() {
			Dictionary<Int32, List<Issue>> toReturn = new Dictionary<Int32, List<Issue>>();
			var Data = new Data();
			AppDbContext db = new AppDbContext();
			Issue quarantine = db.Issues.Where(x => x.IssueName == "Quarantine").FirstOrDefault();
			List<Process> processes = Data.GetActiveProcesses();
			foreach (Process process in processes) {
				Int32 key = process.ProcessID;
				List<Issue> issues = process.Issues.ToList();
				issues.Add(quarantine);
				toReturn.Add(key, issues);
			}
			return toReturn;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="RunProductionViewModel"/> class.
        /// </summary>
        public RunProductionViewModel() {
			this.ProcessIssueDict = GenerateProcessIssueDict();
		}
	}
}