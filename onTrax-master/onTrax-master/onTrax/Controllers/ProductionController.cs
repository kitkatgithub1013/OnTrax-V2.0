// ***********************************************************************
// Assembly         : onTrax
// Created          : 03-25-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Models;
using onTrax.Utilities;
using onTrax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace onTrax.Controllers
{
    /// <summary>
    /// Class ProductionController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProductionController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private DAL.AppDbContext db = new AppDbContext();
        // GET: Tech
        /// <summary>
        /// Index: /
        /// Check In Page for Employees
        /// </summary>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(Int32 EmployeeID = 0)
        {
            // Return a list of all active employees
            var Data = new Data();
            var model = Data.GetActiveEmployees();

            if (model == null || model.Count() < 1)
            {
                TempData["Error"] = "<b>No users found</b> Please add or activate users in Edit onTrax to proceed.";
            }
            return View(model);
        }

        /// <summary>
        /// Run a production cycle
        /// Verifies EmployeeID and PIN combination
        /// </summary>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <param name="PIN">The pin.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Run(Int32 EmployeeID = 0, Int32 PIN = 0)
        {
            // Create an instance of the Data utility
            var Data = new Data();

            var pin = db.Employees.Where(x => x.EmployeeID == EmployeeID).Select(x => x.PIN).FirstOrDefault();
			
            // We cannot proceed w/o employee ID
            if (EmployeeID == 0 || PIN != pin )
            {
                TempData["Error"] = "Please provide the correct Employee and PIN";

                List<Employee> employeeList = new List<Employee>();
                
                employeeList = Data.GetActiveEmployees();

                return View("Index", employeeList);
            }

			// Validate employee
			RunProductionViewModel model = new RunProductionViewModel() {
				Employee = db.Employees.Find(EmployeeID),
				Processes = Data.GetActiveProcesses(),
				Products = Data.GetActiveProducts(),
				Batches = Data.GetActiveBatches(),
				Issues = Data.GetActiveIssues(),
				ProductProcesses = db.ProductProcesses.ToList()
            };
            return View(model);
        }
        
        //POST: CreateProduction
        /// <summary>
        /// Creates the production.
        /// </summary>
        /// <param name="productionToAdd">The production to add.</param>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <param name="ProcessID">The process identifier.</param>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="BatchID">The batch identifier.</param>
        /// <param name="Duration">The duration.</param>
        /// <param name="Quantity">The quantity.</param>
        /// <param name="StartTime">The start time.</param>
        /// <param name="EndTime">The end time.</param>
        /// <param name="Issues">The issues.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]       
        public ActionResult CreateProduction([Bind(Include = "ProductionID")] Production productionToAdd, Int32 EmployeeID, Int32 ProcessID, Int32 ProductID, Int32 BatchID, Decimal Duration, Int32 Quantity, String StartTime, String EndTime, String[] Issues)
        {
            if (ModelState.IsValid)
            {
                // Convert StartTime and EndTime strings into DateTime objects
                DateTime STime = DateTime.ParseExact(StartTime, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime ETime = DateTime.ParseExact(EndTime, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                // Temp dates until Start and Stop time are working w/ js
                productionToAdd.StartTime = STime;
                productionToAdd.EndTime = ETime;

                productionToAdd.Duration = Duration;
                productionToAdd.Quantity = Quantity;

                //Add Employee
                Employee employeeToAdd = db.Employees.Find(EmployeeID);
                productionToAdd.Employee = employeeToAdd;

                //Add Product
                Product productToAdd = db.Products.Find(ProductID);
                productionToAdd.Product = productToAdd;

                //Add Process
                Process processToAdd = db.Processes.Find(ProcessID);
                productionToAdd.Process = processToAdd;

                //Add Batch
                Batch batchToAdd = db.Batches.Find(BatchID);
                productionToAdd.Batch = batchToAdd;

				//Add Issues
				if (Issues != null && Issues.Count() > 0)
                {
                    // Insert a list of Issues as the production Issues where the ID is found within the Issues array
                    productionToAdd.Issues = db.Issues.Where(o => Issues.Contains(o.IssueID.ToString())).ToList();
				}
                db.Productions.Add(productionToAdd);
                db.SaveChanges();
            }
            return null;
        }
    }
}