using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EmployeeManager.Models;
using Newtonsoft.Json;
using System.Text;
using EmployeeManagerModels.Helpers.PagedData;
using Helpers.Layout;

namespace EmployeeManager.Controllers
{
    public class EmployeesController : Controller
    {
        private string m_ApiUrl = string.Empty;
        
        public EmployeesController()
        {
            m_ApiUrl = System.Configuration.ConfigurationManager.AppSettings["EmployeeManagerApiUrl"];
            if(m_ApiUrl != null)
                m_ApiUrl = m_ApiUrl.TrimEnd('/');
        }

        // GET: Employees
        public ActionResult Index(string search = "", int size = 20, int page = 0)
        {
            try
            {
                PagedData<EmployeeManager.Models.Employee> employees = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                    //Called Member default GET All records  
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  

                    string l_qs = string.Empty;

                    if (!string.IsNullOrEmpty(search))
                        l_qs += (string.IsNullOrEmpty(l_qs) ? "?" : "&") + "search=" + search;

                    if (size != 20)
                        l_qs += (string.IsNullOrEmpty(l_qs) ? "?" : "&") + "size=" + size;

                    if (page != 0)
                        l_qs += (string.IsNullOrEmpty(l_qs) ? "?" : "&") + "page=" + page;

                    var responseTask = client.GetAsync("Employee/Paged" + l_qs);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<PagedData<Employee>>();
                        readTask.Wait();

                        employees = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        employees = new PagedData<Employee>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");

                        LayoutMessageHelper.SetMessage("Error trying to get employees. Try again later.", LayoutMessageType.Alert);
                    }
                }

                return View(employees);
            }
            catch (Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View();
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee collection)
        {
            try
            {

                IEnumerable<Employee> employees = null;
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                        string l_Json = JsonConvert.SerializeObject(collection);
                        StringContent l_StringContent = new StringContent(l_Json, UnicodeEncoding.UTF8, "application/json");

                        var responseTask = client.PostAsync("Employee", l_StringContent);
                        responseTask.Wait();

                        //To store result of web api response.   
                        var result = responseTask.Result;

                        //If success received   
                        if (!result.IsSuccessStatusCode)
                        {
                            //Error response received   
                            employees = Enumerable.Empty<EmployeeManager.Models.Employee>();
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");

                            LayoutMessageHelper.SetMessage("Server error", LayoutMessageType.Error);

                            return View(collection);
                        }

                        LayoutMessageHelper.SetMessage("Employee saved successfully", LayoutMessageType.Success);
                    }
                }
                else
                {
                    return View(collection);
                }
                
                return RedirectToAction("Index");
            }
            catch(Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View(collection);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Employee l_employee = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                    var responseTask = client.GetAsync("Employee/" + id);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Employee>();
                        readTask.Wait();

                        l_employee = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        l_employee = new Employee();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }

                return View(l_employee);
            }
            catch (Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View();
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                        string l_Json = JsonConvert.SerializeObject(collection);
                        StringContent l_StringContent = new StringContent(l_Json, UnicodeEncoding.UTF8, "application/json");

                        //Called Member default GET All records  
                        //GetAsync to send a GET request   
                        // PutAsync to send a PUT request  
                        var responseTask = client.PutAsync("Employee/" + id, l_StringContent);
                        responseTask.Wait();

                        //To store result of web api response.   
                        var result = responseTask.Result;

                        //If success received   
                        if (!result.IsSuccessStatusCode)
                        {
                            //Error response received   
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");

                            LayoutMessageHelper.SetMessage("Error trying to edit employee. Try again later.", LayoutMessageType.Alert);

                            return View(collection);
                        }

                        LayoutMessageHelper.SetMessage("Employee saved successfully", LayoutMessageType.Success);
                    }
                }
                else
                {
                    return View(collection);
                }

                return RedirectToAction("Index");
            }
            catch(Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View(collection);
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Employee l_employee = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                    var responseTask = client.GetAsync("Employee/" + id);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Employee>();
                        readTask.Wait();

                        l_employee = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        l_employee = new Employee();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                    LayoutMessageHelper.SetMessage($"Do you confirm the exclusion of { l_employee.name }?", LayoutMessageType.Alert);
                }

                return View(l_employee);
            }
            catch (Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View();
            }
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee collection)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(m_ApiUrl + "/api/");

                        string l_Json = JsonConvert.SerializeObject(collection);
                        StringContent l_StringContent = new StringContent(l_Json, UnicodeEncoding.UTF8, "application/json");

                        var responseTask = client.DeleteAsync("Employee/" + id);
                        responseTask.Wait();

                        //To store result of web api response.   
                        var result = responseTask.Result;

                        //If success received   
                        if (!result.IsSuccessStatusCode)
                        {
                            //Error response received   
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");

                            LayoutMessageHelper.SetMessage("Error trying to delete employee. Try again later.", LayoutMessageType.Alert);

                            return View(collection);
                        }

                        LayoutMessageHelper.SetMessage("Employee deleted successfully", LayoutMessageType.Success);
                    }
                }
                else
                {
                    return View(collection);
                }
                
                return RedirectToAction("Index");
            }
            catch(Exception Error)
            {
                LayoutMessageHelper.SetMessage(Error.Message, LayoutMessageType.Error);

                return View(collection);
            }
        }
    }
}
