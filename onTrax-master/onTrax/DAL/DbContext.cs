// ***********************************************************************
// Assembly         : onTrax
// Created          : 03-25-2018
//
// Last Modified On : 04-19-2018
// ***********************************************************************
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using onTrax.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

/// <summary>
/// The DAL namespace.
/// </summary>
namespace onTrax.DAL
{
    /// <summary>
    /// Class AppDbContext.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        public AppDbContext() : base("onTraxDB") { }

        /// <summary>
        /// Gets or sets the productions.
        /// </summary>
        /// <value>The productions.</value>
        public DbSet <Production> Productions { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public DbSet <Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the batches.
        /// </summary>
        /// <value>The batches.</value>
        public DbSet <Batch> Batches { get; set; }

        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        /// <value>The issues.</value>
        public DbSet <Issue> Issues { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        public DbSet <Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the processes.
        /// </summary>
        /// <value>The processes.</value>
        public DbSet<Process> Processes { get; set; }

        /// <summary>
        /// Gets or sets the product processes.
        /// </summary>
        /// <value>The product processes.</value>
        public DbSet<ProductProcess> ProductProcesses { get; set; }

    }
}