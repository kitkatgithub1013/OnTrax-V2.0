// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-08-2018
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
    /// Class EmployeesController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class EmployeesController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AppDbContext db = new AppDbContext();

        /// <summary>
        /// GET: Admin/Employees
        /// Index of ARCHIVED Employees
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            // Create an instance of the Data utility
            var Data = new Data();

            // Get a list of all archived employees, pass to view
            var model = Data.GetArchivedEmployees();

            // Proceed to show archived list if it is not empty or null
            if (model != null && model.Count() > 0)
            {
                return View(model);
            }

            // Otherwise, redirect to /Manage
            return RedirectToAction("Index", "Manage", new { area = "Admin" });
        }


        /// <summary>
        /// GET: Admin/Employees/Create
        /// Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Admin/Employees/Create
        /// Creates the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,EmployeeName,PIN")] Employee employee)
        {
            
            if (ModelState.IsValid && db.Employees.SingleOrDefault(e => e.EmployeeName == employee.EmployeeName ) == null && db.Employees.SingleOrDefault(e => e.PIN == employee.PIN) == null)
            {
                employee.Active = true;
                db.Employees.Add(employee);
                db.SaveChanges();
                TempData["Success"] = employee.EmployeeName + " has been added";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }
            if (db.Employees.SingleOrDefault(e => e.EmployeeName == employee.EmployeeName) != null && db.Employees.SingleOrDefault(e => e.PIN == employee.PIN) != null)
            {
                TempData["Error"] = employee.EmployeeName + " already exists and " + employee.PIN + " is already in use. Please enter unique values for both fields, or edit the other employee first.";
            } else if (db.Employees.SingleOrDefault(e => e.PIN == employee.PIN) != null)
            {
                TempData["Error"] = "PIN " + employee.PIN + " is already in use. Please enter a unique PIN.";
            } else if (db.Employees.SingleOrDefault(e => e.EmployeeName == employee.EmployeeName) != null)
            {
                TempData["Error"] = employee.EmployeeName + " already exists. Please enter a unique name or edit the other employee first.";
            }
            return View(employee);
        }

        /// <summary>
        /// GET: Admin/Employees/Edit/5
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
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        /// <summary>
        /// POST: Admin/Employees/Edit/5
        /// Edits the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Active,Role,EmployeeName,PIN")] Employee employee)
        {
            // Create an instance of the Data utility
            var Data = new Data();

            bool duplicateNameFound = false;
            bool duplicatePINFound = false;

            // Ensure EmployeeName and PIN are both unique
            foreach (Employee e in Data.GetEmployees())
            {
                if (e.EmployeeID == employee.EmployeeID)
                {
                    continue;
                } else {
                    if (e.EmployeeName == employee.EmployeeName)
                    {
                        duplicateNameFound = true;
                    } else if (e.PIN == employee.PIN)
                    {
                        duplicatePINFound = true;
                    }
                }
            }

            
            if (ModelState.IsValid && !duplicateNameFound && !duplicatePINFound)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = employee.EmployeeName + " has been succesfully edited.";
                return RedirectToAction("Index", "Manage", new { area = "Admin" });
            }


            if (duplicatePINFound && duplicateNameFound)
            {
                TempData["Error"] = "Name and PIN already in use. Please enter unique values.";
            } else if (duplicatePINFound)
            {
                TempData["Error"] = "PIN" + employee.PIN + "is already in use. Please enter a unique PIN.";
            } else if (duplicateNameFound)
            {
                TempData["Error"] = "An employee named" + employee.EmployeeName + "already exists. Please enter a unique name.";
            }

            return View(employee);
        }


        /// <summary>
        /// Admin/Employees/Activate/5
        /// Activates the specified employee identifier.
        /// </summary>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Activate(Int32 EmployeeID = 0)
        {
            if (EmployeeID > 0)
            {
                Employee employee = db.Employees.Find(EmployeeID);
                if (employee != null)
                {
                    employee.Active = true;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = employee.EmployeeName + " has been activated";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }
            }
            return View("Index");
        }

        /// <summary>
        /// Admin/Employees/Archive/5
        /// Archives the specified employee identifier.
        /// </summary>
        /// <param name="EmployeeID">The employee identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Archive(Int32 EmployeeID = 0)
        {
            if (EmployeeID > 0)
            {
                Employee employee = db.Employees.Find(EmployeeID);

                // Check to make sure there is at least one remaining active employee
                Int32 activeCount = 0;
                foreach (Employee e in db.Employees) {
                    if (e.Active == true)
                    {
                        activeCount += 1;
                    }
                }

                if (activeCount <= 1)
                {
                    TempData["Error"] = employee.EmployeeName + " is the only active employee remaining. Please activate another employee before archiving " +employee.EmployeeName +".";
                    return RedirectToAction("Index", "Manage", new { area = "Admin" });
                }

                if (employee != null)
                {
                    employee.Active = false;
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["Success"] = employee.EmployeeName + " has been archived";
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
