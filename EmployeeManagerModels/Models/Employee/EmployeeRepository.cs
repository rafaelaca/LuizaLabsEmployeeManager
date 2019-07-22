using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace EmployeeManager.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        SystemDbContext context = new SystemDbContext();

        public IQueryable<Employee> All
        {
            get { return context.Employees; }
        }

        public IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties)
        {
            IQueryable<Employee> query = context.Employees;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Employee Find(int id)
        {
            return context.Employees.FirstOrDefault(p => p.id == id);
        }

        public Employee FindNotTracking(int id)
        {
            return context.Employees.AsNoTracking().FirstOrDefault(p => p.id == id);
        }

        public void InsertOrUpdate(Employee item)
        {
            if (item.id == default(int))
            {
                // New entity
                context.Employees.Add(item);
            }
            else
            {
                // Existing entity
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var item = context.Employees.Find(id);
            context.Employees.Remove(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

    public interface IEmployeeRepository : IDisposable
    {
        IQueryable<Employee> All { get; }
        IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties);
        Employee Find(int id);
        Employee FindNotTracking(int id);
        void InsertOrUpdate(Employee item);
        void Delete(int id);
        void Save();
    }
}