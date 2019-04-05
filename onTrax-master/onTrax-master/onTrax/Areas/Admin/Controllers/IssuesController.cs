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
using System;
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
    /// Class IssuesController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class IssuesController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();


        /// <summary>
        /// GET: Admin/Issues
        /// Index of ARCHIVED Issues
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // Create an instance of the Data utility
            var Data = new Data();

            // Get a list of all archived employees, pass to view
            var model = Data.GetArchivedIssues();

            // Proceed to show archived list if it is not empty or null
            if (model != null && model.Count() > 0)
            {
                return View(model);
            }

            // Otherwise, redirect to /Manage
            return RedirectToAction("Index", "Manage", new { area = "Admin" });
        }


        /// <summary>
        /// GET: Admin/Issues/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// POST: Admin/Issues/Create
        /// Creates the specified issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IssueID,IssueName")] Issue issue)
        {
            if (ModelState.IsValid && db.Issues.SingleOrDefault(i => i.IssueName == issue.IssueName) == null)
            {
                issue.Active = true;
                db.Issues.Add(issue);
                db.SaveChanges();
                TempData["Success"] = issue.IssueName + " has been added";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
            if (db.Issues.SingleOrDefault(i => i.IssueName == issue.IssueName) != null)
            {
                TempData["Error"] = issue.IssueName + " already exists. Please reactivate the issue or give the new issue a unique name.";
            }
            return View(issue);
        }


        /// <summary>
        /// GET: Admin/Issues/Edit/5
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
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        /// <summary>
        /// POST: Admin/Issues/Edit/5
        /// Edits the specified issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssueID,Active,IssueName")] Issue issue)
        {
            // Create an instance of the Data utility
            var Data = new Data();

            bool duplicateIssueFound = false;
            foreach (Issue i in Data.GetIssues())
            {
                if (i.IssueID == issue.IssueID)
                {
                    continue;
                }
                else
                {
                    if (i.IssueName == issue.IssueName)
                    {
                        duplicateIssueFound = true;
                    }                    
                }
            }

            if (ModelState.IsValid && !duplicateIssueFound)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = issue.IssueName + " has been successfully edited.";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
            if (duplicateIssueFound)
            {
                TempData["Error"] = issue.IssueName + " already exists. Please enter a unique name or edit the other issue first.";
            }
            return View(issue);
        }

        /// <summary>
        /// POST: Admin/Issues/Activate/5
        /// Activates the specified issue identifier.
        /// </summary>
        /// <param name="IssueID">The issue identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Activate(Int32 IssueID = 0)
        {
            if (IssueID > 0)
            {
                Issue issue = db.Issues.Find(IssueID);
                if (issue != null)
                {
                    issue.Active = true;

                    db.Entry(issue).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = issue.IssueName + " has been activated";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return View("Index");
        }

        /// <summary>
        /// POST: Admin/Issues/Archive/5
        /// Archives the specified issue identifier.
        /// </summary>
        /// <param name="IssueID">The issue identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Archive(Int32 IssueID = 0)
        {
            if (IssueID > 0)
            {
                Issue issue = db.Issues.Find(IssueID);

                // Check to make sure there is at least one remaining active employee
                Int32 activeCount = 0;
                foreach (Issue i in db.Issues)
                {
                    if (i.Active == true)
                    {
                        activeCount += 1;
                    }
                }

                if (activeCount <= 1)
                {
                    TempData["Error"] = issue.IssueName + " is the only active issue remaining. Please activate another issue before archiving " + issue.IssueName + ".";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }

                if (issue != null)
                {
                    issue.Active = false;
                    db.Entry(issue).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = issue.IssueName + " has been archived";
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