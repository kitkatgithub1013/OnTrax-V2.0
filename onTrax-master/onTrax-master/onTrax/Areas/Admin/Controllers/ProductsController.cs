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
    /// Class ProductsController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ProductsController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        /// <summary>
        /// GET: Admin/Products
        /// Index of ARCHIVED Products
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // Create an instance of the Data utility
            var Data = new Data();

            // Get a list of all archived products, pass to view
            var model = Data.GetArchivedProducts();


            // Proceed to show archived list if it is not empty or null
            if (model != null && model.Count() > 0)
            {

                return View(model);

            }

            // Otherwise, redirect to /Manage

            return RedirectToAction("Index", "Manage", new { area = "Admin" });
        }

        /// <summary>
        /// GET: Admin/Products/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Admin/Products/Create
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName")] Product product)
        {
            if (ModelState.IsValid && db.Products.SingleOrDefault(p => p.ProductName == product.ProductName) == null)
            {
                product.Active = true;
                db.Products.Add(product);

                db.SaveChanges();

                // Generate standards for the new product
                // As well as any missing product:process combination
                Data data = new Data();
                data.GenerateDefaultStandards();

                TempData["Success"] = product.ProductName + " has been added";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
            else if (db.Products.SingleOrDefault(p => p.ProductName == product.ProductName) != null)
            {
                TempData["Error"] = product.ProductName + " already exists. Please reactivate the product or give the new product a unique name.";
            }
            return View(product);
        }


        /// <summary>
        /// GET: Admin/Products/Edit/5
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        /// <summary>
        /// POST: Admin/Products/Edit/5
        /// Edits the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName")] Product product)
        {
            // Create an instance of the Data utility
            var Data = new Data();

            bool duplicateProductFound = false;
            
            // Check if duplicate product name found
            foreach (Product p in Data.GetProducts())
            {
                if (p.ProductID == product.ProductID)
                {
                    continue;
                }
                else
                {
                    if (p.ProductName == product.ProductName)
                    {
                        duplicateProductFound = true;
                    }                   
                }
            }
            if (ModelState.IsValid && !duplicateProductFound)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = product.ProductName + " has been successfully edited.";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
            if (duplicateProductFound)
            {
                TempData["Error"] = product.ProductName + " already exists. Please enter a unique name or edit the other product first.";
            }
            return View(product);
        }

        /// <summary>
        /// Admin/Products/Activate/5
        /// Activates the specified product identifier.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Activate(Int32 ProductID = 0)
        {
            if (ProductID > 0)
            {
                Product product = db.Products.Find(ProductID);

                if (product != null)
                {
                    product.Active = true;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = product.ProductName + " has been activated";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return View("Index");
        }

        /// <summary>
        /// Admin/Products/Archive/5
        /// Archives the specified product identifier.
        /// </summary>
        /// <param name="ProductID">The product identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Archive(Int32 ProductID = 0)
        {
            if (ProductID > 0)
            {
                Product product = db.Products.Find(ProductID);

                // Check to make sure there is at least one remaining active employee
                Int32 activeCount = 0;
                foreach (Product p in db.Products)
                {
                    if (p.Active == true)
                    {
                        activeCount += 1;
                    }
                }

                if (activeCount <= 1)
                {
                    TempData["Error"] = product.ProductName + " is the only active product remaining. Please activate another product before archiving " + product.ProductName + ".";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
                if (product != null)
                {
                    product.Active = false;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = product.ProductName + " has been archived";
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
