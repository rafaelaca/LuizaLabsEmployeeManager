using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeManager.Models
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext() : base("db_luizalabs_employee_manager")
        {
            System.Data.Entity.Database.SetInitializer<SystemDbContext>(new ValidateDatabase<SystemDbContext>());
        }

        public DbSet<EmployeeManager.Models.Employee> Employees { get; set; }
    }
    
    public class ValidateDatabase<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public ValidateDatabase()
        {
        }

        public void InitializeDatabase(TContext context)
        {
            if (!context.Database.Exists())
            {
                throw new ConfigurationException("Database does not exist");
            }
        }
    }
}