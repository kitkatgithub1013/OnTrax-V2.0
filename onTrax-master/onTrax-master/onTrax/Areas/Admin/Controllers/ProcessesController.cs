// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-09-2018
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
    /// Class ProcessesController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProcessesController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        /// <summary>
        /// GET: Admin/Processes
        /// Index of ARCHIVED Processes
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // Create an instance of the Data utility
            var Data = new Data();

            // Get a list of all archived employees, pass to view
            var model = Data.GetArchivedProcesses();

            // Proceed to show archived list if it is not empty or null
            if (model != null && model.Count() > 0)
            {
                return View(model);
            }

            // Otherwise, redirect to /Manage
            return RedirectToAction("Index", "Manage", new { area = "Admin" }); ;
        }

        /// <summary>
        /// GET: Admin/Processes/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {

            var data = new Data();
            var model = new EditProcessViewModel();

            // Do not include Quarantine as an issue since it is automatically appended to the end
            model.Issues = data.GetActiveIssues().Where(x=>x.IssueID != 5).ToList();

            return View(model);
        }

        /// <summary>
        /// POST: Admin/Processes/Create
        /// Creates the specified model.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditProcessViewModel Model)
        {
			Data data = new Data();
            if (ModelState.IsValid && db.Processes.SingleOrDefault(p => p.ProcessName == Model.Process.ProcessName) == null)
            {
                Model.Process.Active = true;
                db.Processes.Add(Model.Process);

                // If any issue is selected
                if (Model.SelectedIssues != null && Model.SelectedIssues.Count() > 0)
                {
                    // Insert a list of Issues as the process Issues where the ID is found within the Issues array
                    Model.Process.Issues = db.Issues.Where(x => Model.SelectedIssues.Contains(x.IssueID.ToString())).ToList();
                }

                // Generate standards for the new product
                // As well as any missing product:process combination
				db.SaveChanges();
				data.GenerateDefaultStandards();

				TempData["Success"] = Model.Process.ProcessName + " has been added";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
			}
            else if (db.Processes.SingleOrDefault(p => p.ProcessName == Model.Process.ProcessName) != null)
            {
                TempData["Error"] = Model.Process.ProcessName + " already exists. Please reactivate the process or give the new process a unique name.";
            }
			else 
			{
				TempData["Error"] = "Please use only valid characters in the name field.";
			}
			Model.Issues = data.GetActiveIssues();
            return View(Model);
        }

        /// <summary>
        /// GET: Admin/Processes/Edit
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Process process = db.Processes.Find(id);

            var data = new Data();
            var model = new EditProcessViewModel();

            model.Process = process;
            // Do not include Quarantine as an issue since it is automatically appended to the end
            model.Issues = data.GetActiveIssues().Where(x => x.IssueID != 5).ToList();

            return View(model);
        }

        /// <summary>
        /// POST: Admin/Process/Edit
        /// Edits the specified model.
        /// </summary>
        /// <param name="Model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProcessViewModel Model)
        {
            // Look up existing processes based on the ProcessID that was passed through as a hidden field
            var process = db.Processes.Find(Model.Process.ProcessID);
            String[] issues = process.Issues.Select(x => x.IssueID.ToString()).ToArray();
            var d = new Data();

            if (!ModelState.IsValid)
            {
                Model.Process = process;
                Model.Issues = d.GetActiveIssues();
                Model.SelectedIssues = issues;
                return View(Model);
            }

            // If we find that process indeed exists
            if (process != null)
            {
                bool duplicateProcessFound = false;
                foreach (Process p in d.GetProcesses())
                {
                    if (p.ProcessID == process.ProcessID)
                    {
                        continue;
                    }
                    else
                    {
                        if (p.ProcessName == Model.Process.ProcessName)
                        {
                            duplicateProcessFound = true;
                        }                        
                    }
                }
                // Check to see if the ProcessName has been changed
                if (!duplicateProcessFound)
                {
                    // Name has changed and doesn't exist. 
                    // Set the existing process' name to the new name
                    process.ProcessName = Model.Process.ProcessName;
                    TempData["Sucess"] = "Here";

                }
                else
                {
                    TempData["Error"] = "Process name already exists. Please provide a unique Process name.";
                                              
                    Model.Process = process;
                    Model.Issues = d.GetActiveIssues();                        

                    Model.SelectedIssues = issues;
                    return View(Model);
                }
                
                // Clear existing issues from the production so that we may associate any selected issue
                process.Issues.Clear();
                //String Issues = "";

                // If any issue is selected
                if (Model.SelectedIssues != null && Model.SelectedIssues.Count() > 0)
                {
                    // Insert a list of Issues as the process Issues where the ID is found within the Issues array
                    process.Issues = db.Issues.Where(x => Model.SelectedIssues.Contains(x.IssueID.ToString())).ToList();
                }
            
                db.Entry(process).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "Process updated";
                return RedirectToAction("Index", "Manage");
            }

            // Fillout the lists if we need to return to the update view           
            Model.Issues = d.GetActiveIssues();
            Model.SelectedIssues = issues;

            TempData["Error"] = "Process not updated";
            return View(Model);
        }


        /// <summary>
        /// Admin/Employees/Activate/5
        /// Activates the specified process identifier.
        /// </summary>
        /// <param name="ProcessID">The process identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Activate(Int32 ProcessID = 0)
        {
            if (ProcessID > 0)
            {
                Process process = db.Processes.Find(ProcessID);
                if (process != null)
                {
                    process.Active = true;
                    db.Entry(process).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = process.ProcessName + " has been activated";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return View("Index");
        }

        /// <summary>
        /// Admin/Employees/Archive/5
        /// Archives the specified process identifier.
        /// </summary>
        /// <param name="ProcessID">The process identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Archive(Int32 ProcessID = 0)
        {
            if (ProcessID > 0)
            {
                Process process = db.Processes.Find(ProcessID);

                // Check to make sure there is at least one remaining active employee
                Int32 activeCount = 0;
                foreach (Process p in db.Processes)
                {
                    if (p.Active == true)
                    {
                        activeCount += 1;
                    }
                }

                if (activeCount <= 1)
                {
                    TempData["Error"] = process.ProcessName + " is the only active process remaining. Please activate another process before archiving " + process.ProcessName + ".";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }

                if (process != null)
                {
                    process.Active = false;
                    db.Entry(process).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = process.ProcessName + " has been archived";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "Manage", new { area = "Admin" });
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}