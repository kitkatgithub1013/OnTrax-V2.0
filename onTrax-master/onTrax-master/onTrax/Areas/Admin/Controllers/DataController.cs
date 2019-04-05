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
using onTrax.ViewModels;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


/// <summary>
/// The Controllers namespace.
/// </summary>
namespace onTrax.Areas.Admin.Controllers
{
    /// <summary>
    /// Class DataController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class DataController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private DAL.AppDbContext db = new AppDbContext();


        /// <summary>
        /// GET: Admin/Data
        /// Data Management Index
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// GET: /Admin/Data/Edit
        /// Search functionality based on user criteria
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="Date">The date.</param>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <param name="ProcessID">The process identifier.</param>
        /// <param name="ProductID">The product identifier.</param>
        /// <param name="BatchID">The batch identifier.</param>
        /// <param name="IssueID">The issue identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Edit(int? page, String Date = null, Int32 EmployeeID = 0, Int32 ProcessID = 0, Int32 ProductID = 0, Int32 BatchID = 0, Int32 IssueID = 0)
        {
            // We will pass through an instance of ManageViewModel
            var model = new EditDataViewModel();

            // Create an instance of the Data utility
            var Data = new Data();

            // Populate model with appropriate Data Utility for each property
            model.ProductionList = Data.GetProductions();
            model.EmployeeList = Data.GetActiveEmployees();
            model.ProductList = Data.GetActiveProducts();
            model.ProcessList = Data.GetActiveProcesses();
            model.IssueList = Data.GetActiveIssues();
            model.BatchList = Data.GetActiveBatches();

            // Filter model based on earch criteria
            if(model.ProductionList != null && model.ProductionList.Where(x => x != null).Count() > 0)
            {
				// Search by date from date picker UI element
                if (Date != null && Date != "")
                {
                    var date = Convert.ToDateTime(Date);
                    model.ProductionList = model.ProductionList.Where(x => x.StartTime.Date == date.Date).ToList();
                    model.Date = date.ToString("yyyy-MM-dd");
                }
                // Search by Employee
                if (EmployeeID != 0)
                {
                    model.ProductionList = model.ProductionList.Where(x => x.Employee.EmployeeID == EmployeeID).ToList();
                    model.EmployeeID = EmployeeID;
                }

                // Search by Process
                if (ProcessID != 0)
                {
                    model.ProductionList = model.ProductionList.Where(x => x.Process.ProcessID == ProcessID).ToList();
                    model.ProcessID = ProcessID;
                }


                // Search by Product
                if (ProductID != 0)
                {
                    model.ProductionList = model.ProductionList.Where(x => x.Product.ProductID == ProductID).ToList();
                    model.ProductID = ProductID;
                }

                // Search by Batch
                if (BatchID != 0)
                {
                    model.ProductionList = model.ProductionList.Where(x => x.Batch.BatchID == BatchID).ToList();
                    model.BatchID = BatchID;
                }


                // Search by Issue
                if (IssueID != 0)
                {
                    model.ProductionList = model.ProductionList.Where(x => x.Issues.Any(i => i.IssueID == IssueID)).ToList();
                    model.IssueID = IssueID;
                }
				
				// Set pagination settings 
                model.PageSize = 25;
                model.PageNumber = (page ?? 1);
            }
            // Return model to view
            return View(model);
        }

        /// <summary>
        /// GET: Admin/Data/Log
        /// Log manual Production
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Log()
        {
			// Create an instance of the Data utility to get active objects and add them to view mdoel
            var data = new Data();
			// We will pass through an instance of the view model
            var model = new LogDataViewModel();
			
			// Add active objects to view model 
            model.EmployeeList = data.GetActiveEmployees();
            model.IssueList = data.GetActiveIssues();
            model.ProductList = data.GetActiveProducts();
            model.ProcessList = data.GetActiveProcesses();
            model.BatchList = data.GetActiveBatches();

            return View(model);
        }

        /// <summary>
        /// POST: Admin/Data/Log
        /// Log manual Production
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Log(LogDataViewModel Model)
        {
			// Create an instance of the data utility 
            var d = new Data();

            
            if (Model.StartTime == null || Model.StartTime == "" || Model.StartTime.Count() != 10 || Model.StartTime.Length != 10)
            {

                TempData["Error"] = "Please enter a valid Date.";
                Model.EmployeeList = d.GetActiveEmployees();
                Model.ProductList = d.GetActiveProducts();
                Model.ProcessList = d.GetActiveProcesses();
                Model.BatchList = d.GetActiveBatches();
                Model.IssueList = d.GetActiveIssues();

                Model.SelectedIssues = Model.SelectedIssues;



                return View(Model);
            }
            

            if (!ModelState.IsValid)
            {
                // Production log entry was invalid
                // Kick them back to the view and try again
				// But we have to rebuild the view model first 
                Model.EmployeeList = d.GetActiveEmployees();
                Model.ProductList = d.GetActiveProducts();
                Model.ProcessList = d.GetActiveProcesses();
                Model.BatchList = d.GetActiveBatches();
                Model.IssueList = d.GetActiveIssues();

                Model.SelectedIssues = Model.SelectedIssues;



                return View(Model);
            }

            // If we find that production indeed exists
            if (Model != null)
            {
				// Initialize a new Production object 
                var production = new Production();

                // Grab each property based on the selected value and add it to the new Production object
                production.Employee = db.Employees.Find(Model.EmployeeID);
                production.Product = db.Products.Find(Model.ProductID);
                production.Process = db.Processes.Find(Model.ProcessID);
                production.Batch = db.Batches.Find(Model.BatchID);

                production.Quantity = Model.Quantity;

                // Duration from manual logging is in HOURS
                // Measures are calcualted as MINUTES
                production.Duration = Model.Duration * 60;

				// Convert user-entered times to DateTimes for model and DB 
                production.StartTime = Convert.ToDateTime(Model.StartTime);
				// DateRecorded is only not null if an Admin enters the production manually 
                production.DateRecorded = Model.DateRecorded;
                production.EndTime = Convert.ToDateTime(Model.StartTime);

                // If any issue is selected
                if (Model.SelectedIssues != null && Model.SelectedIssues.Count() > 0)
                {
                    // Insert a list of Issues as the production Issues where the ID is found within the Issues array
                    production.Issues = db.Issues.Where(x => Model.SelectedIssues.Contains(x.IssueID.ToString())).ToList();
                }

                System.Diagnostics.Debug.WriteLine("**********************************");
                System.Diagnostics.Debug.WriteLine(Model.StartTime + " " + Model.StartTime.Length + " " + Model.StartTime.Count());
                System.Diagnostics.Debug.WriteLine("**********************************");

                db.Productions.Add(production);
                db.SaveChanges();
                var ReturnModel = new LogDataViewModel();
				// Inform the user that their data was successfully entered 
                TempData["Success"] = "Production Logged";

                // Rebuild the view model and send them back to the update view 
                ReturnModel.EmployeeList = d.GetActiveEmployees();
                ReturnModel.ProductList = d.GetActiveProducts();
                ReturnModel.ProcessList = d.GetActiveProcesses();
                ReturnModel.BatchList = d.GetActiveBatches();
                ReturnModel.IssueList = d.GetActiveIssues();

                return View(ReturnModel);
            }

            // Fillout the lists if we need to return to the update view
            Model.EmployeeList = d.GetActiveEmployees();
            Model.ProductList = d.GetActiveProducts();
            Model.ProcessList = d.GetActiveProcesses();
            Model.BatchList = d.GetActiveBatches();
            Model.IssueList = d.GetActiveIssues();

            Model.SelectedIssues = Model.SelectedIssues;
			// Inform the user that the data was not entered successfully 
            TempData["Error"] = "Production not updated";
            return View(Model);
        }

        /// <summary>
        /// Admin/Data/Delete/5
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delete(int id)
        {           
            Production productionToDelete = db.Productions.Find(id);
            db.Productions.Remove(productionToDelete);
            Console.Write(productionToDelete);
            db.SaveChanges();
            TempData["Success"] = "Production record successfully deleted.";
            return RedirectToAction("Edit", "Data", new { area = "Admin" });
        }

        /// <summary>
        /// GET: Admin/Data/Update/1
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			// Find the production instance the user selected and populate the view model 
            Production production = db.Productions.Find(id);
            var data = new Data();
            var model = new UpdateDataViewModel();
            model.Production = production;
            model.EmployeeList = data.GetActiveEmployees();
            model.IssueList = data.GetActiveIssues();
            model.ProductList = data.GetActiveProducts();
            model.ProcessList = data.GetActiveProcesses();
            model.BatchList = data.GetActiveBatches();

            return View(model);
        }

        /// <summary>
        /// POST: Admin/Data/Update/1
        /// Updates the specified model.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateDataViewModel Model)
       {
            // Look up existing productions based on the ProductionID that was passed through as a hidden field
            var production = db.Productions.Find(Model.Production.ProductionID);
            String[] issues = production.Issues.Select(x => x.IssueID.ToString()).ToArray();

			// Create an instance of the Data utility 
            var d = new Data();

            if (!ModelState.IsValid)
            {
               //Production log entry was invalid
               // Kick them back to the view and try again
                Model.EmployeeList = d.GetActiveEmployees();
                Model.ProductList = d.GetActiveProducts();
                Model.ProcessList = d.GetActiveProcesses();
                Model.BatchList = d.GetActiveBatches();                
                Model.IssueList = d.GetActiveIssues();
                Model.Production = production;
                Model.SelectedIssues = issues;

                return View(Model);
            }

            // If we find that production indeed exists
            if (production != null)
            {
                // Grab each property based on the selected value
                production.Employee = db.Employees.Find(Model.Production.Employee.EmployeeID);
                production.Product = db.Products.Find(Model.Production.Product.ProductID);
                production.Process = db.Processes.Find(Model.Production.Process.ProcessID);
                production.Batch = db.Batches.Find(Model.Production.Batch.BatchID);
                production.Quantity = Model.Production.Quantity;
                production.Duration = Model.Production.Duration;

                // Clear existing issues from the production so that we may associate any selected issue
                production.Issues.Clear();

                // If any issue is selected
                if (Model.SelectedIssues != null && Model.SelectedIssues.Count() > 0)
                {                    
                    // Insert a list of Issues as the production Issues where the ID is found within the Issues array
                    production.Issues = db.Issues.Where(x => Model.SelectedIssues.Contains(x.IssueID.ToString())).ToList();
                }
                db.Entry(production).State = EntityState.Modified;
                db.SaveChanges();
				// Inform the user that the production was updated successfully 
                TempData["Success"] = "Production updated";
                return RedirectToAction("Edit", "Data");
            }
            
            // Fillout the lists if we need to return to the update view
            Model.EmployeeList = d.GetActiveEmployees();         
            Model.ProductList = d.GetActiveProducts();
            Model.ProcessList = d.GetActiveProcesses();
            Model.BatchList = d.GetActiveBatches();            
            Model.IssueList = d.GetActiveIssues();
            Model.Production = production;
            Model.SelectedIssues = issues;

			// Inform the user that the production was not updated successfully 
            TempData["Error"] = "Production not updated";
            return View(Model);
        }
    }
}