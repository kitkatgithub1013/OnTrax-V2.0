// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-05-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Utilities;
using onTrax.ViewModels;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace onTrax.Areas.Admin.Controllers
{
    /// <summary>
    /// Class ManageController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ManageController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private DAL.AppDbContext db = new AppDbContext();

        /// <summary>
        /// GET: Admin/Manage
        /// Index for Edit onTrax
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {

            // We will pass through an instance of ManageViewModel

            var model = new ManageViewModel();

            // Create an instance of the Data utility
            var Data = new Data();


            // Fill ManageViewModel using the appropriate Data utiltiy

            model.Employees = Data.GetActiveEmployees();
            model.Products = Data.GetActiveProducts();
            model.Processes = Data.GetActiveProcesses();
            model.Issues = Data.GetActiveIssues();
            model.Batches = Data.GetActiveBatches();


            // Return model to view
            return View(model);
        }
    }
}