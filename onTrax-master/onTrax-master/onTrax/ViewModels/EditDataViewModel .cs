// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-09-2018
//
// Last Modified On : 04-20-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.Models;
using PagedList;
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
    /// Class EditDataViewModel.
    /// Type: ViewModel
    /// For: Admin/Data/Edit
    /// 
    /// Contains paginated list of all production runs
    /// Contains lists of all searchable models
    /// Contains individual model IDs as search criteria
    /// 
    /// </summary>
    public class EditDataViewModel
    {
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
        /// Gets or sets the production list.
        /// </summary>
        /// <value>The production list.</value>
        [Display(Name ="Productions")]
        public List<Production> ProductionList { get; set; }

        // Setup a Paged List of ProductionList
        /// <summary>
        /// Gets the paged production list.
        /// </summary>
        /// <value>The paged production list.</value>
        [Display(Name = "Paginatd Productions")]
        public IPagedList<Production> PagedProductionList
        {
            get
            {
                return GetPagedProductions();
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        [Display(Name = "Page Size")]
        public Int32 PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        [Display(Name = "Page Number")]
        public Int32 PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        [Display(Name = "Employee")]
        public Int32 EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        [Display(Name = "Product")]
        public Int32 ProductID { get; set; }

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        /// <value>The process identifier.</value>
        [Display(Name = "Process")]
        public Int32 ProcessID { get; set; }

        /// <summary>
        /// Gets or sets the batch identifier.
        /// </summary>
        /// <value>The batch identifier.</value>
        [Display(Name = "Batch")]
        public Int32 BatchID { get; set; }

        /// <summary>
        /// Gets or sets the issue identifier.
        /// </summary>
        /// <value>The issue identifier.</value>
        [Display(Name = "Issue")]
        public Int32 IssueID { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [Display(Name = "Date")]
        public String Date { get; set; }



        /// <summary>
        /// Gets the paged productions.
        /// </summary>
        /// <returns>IPagedList&lt;Production&gt;.</returns>
        public IPagedList<Production> GetPagedProductions()
        {
            Int32 pageNumber = 1;
            Int32 pageSize = 2;

            // If PageNumber is set, set pageNumber to it
            if (PageNumber > 0) pageNumber = PageNumber;

            // If PageSize is set, set pageSize to it
            if (PageSize > 0) pageSize = PageSize;

            // Generated a paged list with appropriate page size and number
            if(ProductionList != null && ProductionList.Where(x => x !=null).Count() > 0)
            {
                var Return = ProductionList.ToPagedList(pageNumber, pageSize);
                return Return;
            }
            return null;            
        }
    }
}