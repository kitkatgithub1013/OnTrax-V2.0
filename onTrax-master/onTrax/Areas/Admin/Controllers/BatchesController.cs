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
    /// Class BatchesController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class BatchesController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Batches
        /// <summary>
        /// Admin/Batches/
        /// Index of all ARCHIVED Batches
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // Create an instance of the Data utility
            var Data = new Data();

            // Get a list of all archived batches, pass to view
            var model = Data.GetArchivedBatches();

            // Proceed to show archived list if it is not empty or null
            if (model != null && model.Count() > 0)
            {
                return View(model);
            }

            // Otherwise, redirect to /Manage because there is nothing to show 
			// TODO: Use TempData message to inform the user that there are no archived objects to show? 
            return RedirectToAction("Index", "Manage", new { area = "Admin" });
        }

        /// <summary>
        /// GET: Admin/Batches/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Admin/Batches/Create
        /// Creates the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchID,BatchName")] Batch batch)
        {
			// If the model is valid AND a batch object does not already exist with that Batch name 
            if (ModelState.IsValid && db.Batches.SingleOrDefault(b => b.BatchName == batch.BatchName) == null)
            {
                batch.Active = true;
                db.Batches.Add(batch);
                db.SaveChanges();
				// Inform the user that the batch was added 
                TempData["Success"] = batch.BatchName + " has been added";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
			// If a batch object already exists with that batch name 
            if (db.Batches.SingleOrDefault(b => b.BatchName == batch.BatchName) != null)
            {
				// Inform the user that there is a duplicate occurrence
                TempData["Error"] = batch.BatchName + " already exists. Please reactivate the batch or give the new batch a unique name.";
            }
            return View(batch);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// GET: Admin/Batches/Edit/5
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }


        /// <summary>
        /// POST: Admin/Batches/Edit/5
        /// Edits the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchID,Active,BatchName")] Batch batch)
        {
            // Create an instance of the Data utility
            var Data = new Data();

			// Prevent the user from editing a batch such that 
			// it has the same name of another batch object 
			// It is acceptable for the user to edit a batch and save it without changing the name 
            bool duplicateBatchFound = false;
            foreach (Batch b in Data.GetBatches())
            {
                if (b.BatchID == batch.BatchID)
                {
                    continue;
                }
                else
                {
                    if (b.BatchName == batch.BatchName)
                    {
                        duplicateBatchFound = true;
                    }
                }
            }
			// If a duplicate name has not been entered and the model valid
            if (ModelState.IsValid && !duplicateBatchFound)
            {
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
				// Inform the user that the batch object has been successfully added
                TempData["Success"] = batch.BatchName + " has been successfully edited.";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
			// If a duplicate name has been entered 
            if (duplicateBatchFound)
            {
				// Inform the user of the duplicate name and recommend a course of action 
                TempData["Error"] = batch.BatchName + " already exists. Please enter a unique name or edit the other batch first.";
            }
            return View(batch);
        }

        /// <summary>
        /// POST: Admin/Batches/Activate/1
        /// Activates the specified batch identifier.
        /// </summary>
        /// <param name="BatchID">The batch identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Activate(Int32 BatchID = 0)
        {
            if (BatchID > 0)
            {
                Batch batch = db.Batches.Find(BatchID);
                if (batch != null)
                {
                    batch.Active = true;
                    db.Entry(batch).State = EntityState.Modified;
                    db.SaveChanges();
					// Inform the user that the batch object has been successfully activated
                    TempData["Success"] = batch.BatchName + " has been activated";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return View("Index");
        }


        /// <summary>
        /// POST: Admin/Batches/Archive/1
        /// Archives the specified batch identifier.
        /// </summary>
        /// <param name="BatchID">The batch identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Archive(Int32 BatchID = 0)
        {
            if (BatchID > 0)
            {
                Batch batch = db.Batches.Find(BatchID);

                // Check to make sure there is at least one remaining active batch
                Int32 activeCount = 0;
                foreach (Batch b in db.Batches)
                {
                    if (b.Active == true)
                    {
                        activeCount += 1;
                    }
                }
				
				// Ensure that there is always at least one active batch -- no active batches will cause issues in Production Portal
                if (activeCount <= 1)
                {
					// Inform the user of the issue and recommend a course of action 
                    TempData["Error"] = batch.BatchName + " is the only active Batch remaining. Please activate or add another batch before archiving " + batch.BatchName + ".";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
				
				// If there will be at least one active batch left after this one is archived 
                if (batch != null)
                {
                    batch.Active = false;
                    db.Entry(batch).State = EntityState.Modified;
                    db.SaveChanges();
					//Inform the user that the batch has been successfully archived 
                    TempData["Success"] = batch.BatchName + " has been archived";
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
