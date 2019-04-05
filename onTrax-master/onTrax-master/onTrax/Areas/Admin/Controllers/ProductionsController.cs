// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-10-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Models;
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
    /// Class ProductionsController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProductionsController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Productions
        /// <summary>
        /// Index of all Productions
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View(db.Productions.ToList());
        }

        /// <summary>
        /// GET: Admin/Productions/Details/5
        /// Details of specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        /// <summary>
        /// GET: Admin/Productions/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Admin/Productions/Create
        /// Creates the specified production.
        /// </summary>
        /// <param name="production">The production.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductionID,DateRecorded,StartTime,EndTime,Duration,Quantity")] Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.Add(production);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(production);
        }

        // GET: Admin/Productions/Edit/5
        /// <summary>
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
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        /// <summary>
        /// POST: Admin/Productions/Edit/5
        /// Edits the specified production.
        /// </summary>
        /// <param name="production">The production.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductionID,DateRecorded,StartTime,EndTime,Duration,Quantity")] Production production)
        {
            if (ModelState.IsValid)
            {
                db.Entry(production).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "Production updated";
                return RedirectToAction("Index");
            }
            return View(production);
        }


        /// <summary>
        /// GET: Admin/Productions/Delete/5
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Production production = db.Productions.Find(id);
            if (production == null)
            {
                return HttpNotFound();
            }
            return View(production);
        }

        /// <summary>
        /// POST: Admin/Productions/Delete/5
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Production production = db.Productions.Find(id);
            db.Productions.Remove(production);
            db.SaveChanges();
            TempData["Success"] = "Production deleted";
            return RedirectToAction("Index");
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
