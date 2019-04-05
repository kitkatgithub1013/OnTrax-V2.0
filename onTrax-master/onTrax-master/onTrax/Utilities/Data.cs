// ***********************************************************************
// Assembly         : onTrax
// Created          : 04-09-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary>Utility methods used throughout assembly</summary>
// ***********************************************************************
using onTrax.DAL;
using onTrax.Models;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The Utilities namespace.
/// </summary>
namespace onTrax.Utilities
{
    /// <summary>
    /// Class Data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// The database
        /// </summary>
        private DAL.AppDbContext db = new AppDbContext();

        /*************************************/
        /*  Start Product Data Utilities    */

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>List&lt;Employee&gt;.</returns>
        public List<Employee> GetEmployees()
        {

            var model = db.Employees.OrderBy(x => x.EmployeeName).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the active employees.
        /// </summary>
        /// <returns>List&lt;Employee&gt;.</returns>
        public List<Employee> GetActiveEmployees()
        {

            var model = db.Employees.OrderBy(x => x.EmployeeName).Where(x => x.Active == true).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the archived employees.
        /// </summary>
        /// <returns>List&lt;Employee&gt;.</returns>
        public List<Employee> GetArchivedEmployees()
        {

            var model = db.Employees.OrderBy(x => x.EmployeeName).Where(x => x.Active == false).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }
        /*  END Employee Data Utilities     */



        /*************************************/
        /*  START Product Data Utilities    */


        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns>List&lt;Product&gt;.</returns>
        public List<Product> GetProducts()
        {

            var model = db.Products.OrderBy(x => x.ProductName).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the active products.
        /// </summary>
        /// <returns>List&lt;Product&gt;.</returns>
        public List<Product> GetActiveProducts()
        {

            var model = db.Products.OrderBy(x => x.ProductName).Where(x => x.Active == true).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the archived products.
        /// </summary>
        /// <returns>List&lt;Product&gt;.</returns>
        public List<Product> GetArchivedProducts()
        {

            var model = db.Products.OrderBy(x => x.ProductName).Where(x => x.Active == false).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /*  END Product Data Utilities    */


        /*************************************/
        /*  START Process Data Utilities    */


        /// <summary>
        /// Gets the processes.
        /// </summary>
        /// <returns>List&lt;Process&gt;.</returns>
        public List<Process> GetProcesses()
        {

            var model = db.Processes.OrderBy(x => x.ProcessID).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the active processes.
        /// </summary>
        /// <returns>List&lt;Process&gt;.</returns>
        public List<Process> GetActiveProcesses()
        {

            var model = db.Processes.OrderBy(x => x.ProcessID).Where(x => x.Active == true).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the archived processes.
        /// </summary>
        /// <returns>List&lt;Process&gt;.</returns>
        public List<Process> GetArchivedProcesses()
        {

            var model = db.Processes.OrderBy(x => x.ProcessID).Where(x => x.Active == false).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /*  END Process Data Utilities    */


        /// <summary>
        /// Gets the product processes.
        /// </summary>
        /// <returns>List&lt;ProductProcess&gt;.</returns>
        public List<ProductProcess> GetProductProcesses()
        {

            var model = db.ProductProcesses.OrderBy(x => x.ProductProcessID).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /*  START Issue Data Utilities    */


        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <returns>List&lt;Issue&gt;.</returns>
        public List<Issue> GetIssues()
        {
            // Query Issues table, do not include Quarantine
            var model = db.Issues.Where(x => x.IssueID != 5).OrderBy(x => x.IssueName).ToList();

            // Quarantine is appended to the end of the list so it is always displayed last
            var Quarantine = db.Issues.Where(x => x.IssueID == 5).FirstOrDefault();
            model.Add(Quarantine);

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /// <summary>
        /// Gets the active issues.
        /// </summary>
        /// <returns>List&lt;Issue&gt;.</returns>
        public List<Issue> GetActiveIssues()
        {

            // Query Issues table, do not include Quarantine
            var model = db.Issues.Where(x => x.Active == true).Where(x => x.IssueID != 5).OrderBy(x => x.IssueName).ToList();

            // Quarantine is appended to the end of the list so it is always displayed last
            var Quarantine = db.Issues.Where(x => x.IssueID == 5).FirstOrDefault();
            model.Add(Quarantine);


            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the archived issues.
        /// </summary>
        /// <returns>List&lt;Issue&gt;.</returns>
        public List<Issue> GetArchivedIssues()
        {

            // Query Issues table, do not include Quarantine
            var model = db.Issues.Where(x => x.Active == false).Where(x => x.IssueID != 5).OrderBy(x => x.IssueName).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /*  END Issue Data Utilities    */




        /*************************************/
        /*  START Batch Data Utilities      */


        /// <summary>
        /// Gets the batches.
        /// </summary>
        /// <returns>List&lt;Batch&gt;.</returns>
        public List<Batch> GetBatches()
        {

            var model = db.Batches.OrderBy(x => x.BatchName).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the active batches.
        /// </summary>
        /// <returns>List&lt;Batch&gt;.</returns>
        public List<Batch> GetActiveBatches()
        {

            var model = db.Batches.OrderBy(x => x.BatchName).Where(x => x.Active == true).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }


        /// <summary>
        /// Gets the archived batches.
        /// </summary>
        /// <returns>List&lt;Batch&gt;.</returns>
        public List<Batch> GetArchivedBatches()
        {

            var model = db.Batches.OrderBy(x => x.BatchName).Where(x => x.Active == false).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /*  END Batch Data Utilities    */


        /*************************************/
        /*  START Productions Data Utilities */


        /// <summary>
        /// Gets the productions.
        /// </summary>
        /// <returns>List&lt;Production&gt;.</returns>
        public List<Production> GetProductions()
        {

            var model = db.Productions.OrderByDescending(x => x.StartTime).ToList();

            if (model != null && model.Count() > 0) return model;

            return null;
        }

        /// <summary>
        /// Gets the filtered productions.
        /// </summary>
        /// <returns>List&lt;Production&gt;.</returns>
        public List<Production> GetFilteredProductions()
        {


            return null;
        }



        /***************************************/
        /*  START ProductProcess Data Utilities */

        /// <summary>
        /// Generates the default standards.
        /// Runs each time a product or process is created
        /// Reconciles any missing pair of product and process
        /// Defaults to 0 standard duration and quantity
        /// </summary>
        public void GenerateDefaultStandards()
        {

            // Get a list of all products and processes
            // We want both Active and Inactive to not orphan any that may potentially be activated

            List<Product> products = GetProducts();
            List<Process> processes = GetProcesses();

            // Get all existing ProductsProcessses
            List<ProductProcess> product_processes = GetProductProcesses();


            // Create a dictionary using each product as a key and a list of all processes as value
            // i.e. prodprocdict['Woom 1'][List<Process>]

            var prodprocdict = products.Distinct().ToDictionary(x => x, x => processes);

            // Iterate through dictionary keys 
            // i.e. 'Woom 1'
            foreach (var i in prodprocdict)
            {



                // Iterate through each value for this key
                // i.e. 'Pre-Assembly'
                foreach (var p in i.Value)
                {
                    // if the combination of key and value results in less than 1 record found in ProductProcess table
                    // i.e. 'Woom 1' 'Pre-Assembly'
                    if (product_processes.Where(x => x.Product == i.Key).Where(x => x.Process == p).ToList().Count() < 1)
                    {
                        // Add new combination to ProductProcess table
                        // Set Default values = 0

                        ProductProcess pp = new ProductProcess();

                        pp.Process = p;
                        pp.Product = i.Key;
                        pp.Quantity = 0;
                        pp.StandardDuration = 0;

                        db.ProductProcesses.Add(pp);
                        
                    }
                }
            }
            // Save changes to database
            db.SaveChanges();
        }
    }
}