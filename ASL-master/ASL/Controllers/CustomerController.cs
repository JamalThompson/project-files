using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASL.Models;
using Customers_page.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                CustomerContext context = HttpContext.RequestServices.GetService(typeof(CustomerContext)) as CustomerContext;
                customers = context.FindAll();

                return View(customers);
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id, Name, Address1, BillCycle, Contact_Name, Contact_Address, Address2, City, State, Zip, Phone_Number")]Customer customer)
        {
            try
            {
                CustomerContext context = HttpContext.RequestServices.GetService(typeof(CustomerContext)) as CustomerContext;
                await context.InsertCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int Id)
        {
            try
            {
                CustomerContext context = HttpContext.RequestServices.GetService(typeof(CustomerContext)) as CustomerContext;
                Customer customer = context.FindOneAsync(Id);

                return View(customer);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id, Name, Address1, BillCycle, Contact_Name, Contact_Address, Address2, City, State, Zip, Phone_Number")] Customer customer)
        {
            try
            {
                CustomerContext context = HttpContext.RequestServices.GetService(typeof(CustomerContext)) as CustomerContext;
                context.UpdateAsync(customer);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}