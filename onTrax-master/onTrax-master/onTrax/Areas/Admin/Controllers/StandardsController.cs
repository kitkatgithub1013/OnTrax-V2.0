// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-11-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Utilities;
using onTrax.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace onTrax.Areas.Admin.Controllers
{
    /// <summary>
    /// Class StandardsController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class StandardsController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        /// <summary>
        /// GET: Admin/Standards
        /// Index of Standards
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // We will pass through an instance of ManageViewModel
            var model = new ManageViewModel();

            // Create an instance of the Data utility
            var Data = new Data();

            // Populate model with appropriate Data Utility for each property
            model.Products = Data.GetActiveProducts();
            model.Processes = Data.GetActiveProcesses();
            model.ProductProcesses = Data.GetProductProcesses();

            return View(model);
        }

        /// <summary>
        /// GET: Admin/Standards/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Admin/Standards/Create
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// POST: Admin/Standards/Edit
        /// Edits the specified standards dictionary.
        /// </summary>
        /// <param name="standardsDict">The standards dictionary.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Edit(Dictionary<String, String> standardsDict)
        {
            if (ModelState.IsValid)
            {
                foreach (KeyValuePair<String, String> i in standardsDict)
                {
                    Int32 primaryKey = Int32.Parse(i.Key);
                    try {
						Decimal standardDuration = Decimal.Parse(i.Value);
						var productProcess = db.ProductProcesses.Find(primaryKey);
						productProcess.StandardDuration = standardDuration;
						db.Entry(productProcess).State = EntityState.Modified;
					} catch (System.FormatException e) {
						TempData["Error"] = "Please enter a valid number in the time field.";
						return Json(new { completed = "false" });
					} 
                }
                db.SaveChanges();
                TempData["Success"] = "You have successfully edited standard times";
				//return RedirectToAction("Index", "Standards", new { area = "Admin" });
				return Json(new { completed = "true" });
            }        
            return View();
        }

        /// <summary>
        /// GET: Admin/Standards/Delete/5
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delete(int id)
        {
            return View();
        }


        /// <summary>
        /// POST: Admin/Standards/Delete/5
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
