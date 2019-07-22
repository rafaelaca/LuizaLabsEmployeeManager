using EmployeeManager.Models;
using EmployeeManagerModels.Helpers.PagedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeManager.Controllers
{
    /// <summary>
    /// Manage Employees. List, Add, Delete and Update
    /// </summary>
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository employeeRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public EmployeeController() : this(new EmployeeRepository())
        {
        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            try
            {
                this.employeeRepository = employeeRepository;
            }
            catch (Exception l_exception)
            {
                
            }
        }

        /// <summary>
        /// Get employees list
        /// </summary>
        /// <param name="search">string to search an employee by name, e-mail or department</param>
        /// <param name="size">Number of employees returned. Default: 20</param>
        /// <param name="page">For paging. Skip (size x page) employees</param>
        /// <returns>List of employees that matched the search</returns>
        // GET: api/Employee
        public IEnumerable<Employee> Get(string search = "", int size = 20, int page = 0)
        {
            try
            {
                IEnumerable<Employee> l_Employees = employeeRepository.All;
                int l_size = size;

                if (!string.IsNullOrEmpty(search))
                    l_Employees = l_Employees.Where(p => p.name.IndexOf(search) > -1 ||
                                                         p.email.IndexOf(search) > -1 ||
                                                         p.department.IndexOf(search) > -1);

                if (page > 0)
                    l_Employees = l_Employees.Skip(page * l_size);

                l_Employees = l_Employees.Take(l_size);

                return l_Employees.OrderBy(p => p.name).ToList();
            }
            catch (Exception Error)
            {
                throw Error;
            }
            
        }

        /// <summary>
        /// Get employees list in a paged structure
        /// </summary>
        /// <param name="search">string to search an employee by name, e-mail or department</param>
        /// <param name="size">Number of employees returned. Default: 20</param>
        /// <param name="page">For paging. Skip (size x page) employees</param>
        /// <returns>List of employees that mached the search in a paged structure</returns>
        [HttpGet]
        public PagedData<Employee> Paged(string search = "", int size = 20, int page = 0)
        {
            PagedData<Employee> l_PagedData = new PagedData<Employee>();
            try
            {
                int l_page = (page == 0) ? 1 : page;
                
                IQueryable<Employee> all = employeeRepository.All;

                if (!string.IsNullOrEmpty(search))
                    all = all.Where(p => p.name.IndexOf(search) > -1 ||
                                         p.email.IndexOf(search) > -1 ||
                                         p.department.IndexOf(search) > -1);

                int l_count = all.Count<Employee>();
                l_PagedData.Data = all.OrderBy(p => p.name)
                                        .Skip<Employee>(size * (l_page - 1))
                                        .Take<Employee>(size)
                                        .ToList<Employee>().OrderByDescending(x => x.name);

                l_PagedData.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)l_count / size));
                l_PagedData.TotalNumberOfRecords = l_count;
                l_PagedData.CurrentPage = l_page;

                return l_PagedData;
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }

        /// <summary>
        /// Get specific Employee by id
        /// </summary>
        /// <param name="id">Employee's id</param>
        /// <returns></returns>
        // GET: api/Employee/5
        public Employee Get(int id)
        {
            try
            {
                Employee l_Employee = employeeRepository.Find(id);
                
                return l_Employee;
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }

        /// <summary>
        /// Add new Employee
        /// </summary>
        /// <param name="value">Employee object</param>
        // POST: api/Employee
        public void Post([FromBody]Employee value)
        {
            try
            {
                Employee l_Employee = value;

                employeeRepository.InsertOrUpdate(l_Employee);

                employeeRepository.Save();
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="id">Employees's id to be updated</param>
        /// <param name="value">Employee object</param>
        // PUT: api/Employee/5
        public void Put(int id, [FromBody]Employee value)
        {
            try
            {
                Employee l_Employee = employeeRepository.Find(id);

                l_Employee.name = value.name;
                l_Employee.email = value.email;
                l_Employee.department = value.department;

                employeeRepository.InsertOrUpdate(l_Employee);

                employeeRepository.Save();
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }

        /// <summary>
        /// Delete Employee by id
        /// </summary>
        /// <param name="id">Employee's id to be deleted</param>
        // DELETE: api/Employee/5
        public void Delete(int id)
        {
            try
            {
                employeeRepository.Delete(id);

                employeeRepository.Save();
            }
            catch (Exception Error)
            {
                throw Error;
            }
        }
    }
}
