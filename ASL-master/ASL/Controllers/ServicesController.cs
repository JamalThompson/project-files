using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASL.Data;
using ASL.Models;
using Microsoft.AspNetCore.Http;
using Customers_page.Data;
using ASL.ViewModels;
using MySql.Data.MySqlClient;

namespace ASL.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ServicesContext _context;

        public ServicesController(ServicesContext context)
        {
            _context = context;
        }


       // GET: Services
        public IActionResult Index()
        {
            try
            {
                List<IndexServiceViewModel> services = new List<IndexServiceViewModel>();
                ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;

                services = context.CopyDaily();


                return View(services);
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }


        // GET: Service/Details/5
        public ActionResult Details(int Id)
        {

            ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
            string name = "";
            Service service = context.FindOneAsyncByName(Id, ref name);
            ViewBag.name = name;


            return View(service);


        }



        //Post: Services/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("Id,SvcType,Address1,Address2,City,State,Zip,SvcDescription,Sched_Date,comp_date,BillSubtotal", Prefix = "ServiceDetails")] Service service)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    service.Id = Int32.Parse(this.Request.Form["Cust.Id"]);
                    ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
                    context.UpdateAsync(service);
                    await _context.InsertServiceAsync(service);

                }
                        
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Details));
        }

        //Set: Complete Date
        public async Task<IActionResult> SetCompDATE(int id)
        {
            try
            {
                
                    ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
                    await context.SetCompletionDate(id);
               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex);

            }
                          
        }



        // GET: Services/Create
        public ActionResult Create()
        {

            CustomerContext context = HttpContext.RequestServices.GetService(typeof(CustomerContext)) as CustomerContext;
            CreateServiceViewModel Service = new CreateServiceViewModel();
            Service.PopulateCustomers(context.FindAll());
            return View(Service);
        }


        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( [Bind("Id,SvcType,Address1,Address2,City,State,Zip,SvcDescription,Sched_Date,comp_date,BillSubtotal, Phone_Number",Prefix ="ServiceCreate" )] Service service)

        {
            try
            {
                if (ModelState.IsValid) {
                    service.Cust_Id = Int32.Parse(this.Request.Form["Cust_Id"]);
                ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
                await _context.InsertServiceAsync(service);

                   
                }
 
            }
            catch (Exception ex)
            {

                return View(ex);
            }

            return RedirectToAction(nameof(Index));

        }


        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
                EditServiceViewModel Service = new EditServiceViewModel();
                Service.setName(context.FindCustName(id));
                Service.ServiceEdit = context.FindOneAsync(id);
                return View(Service);
            }
            catch(Exception ex)
            {
                return View();
            }
        }//Completed

      
        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SvcType,Address1,Address2,City,State,Zip,SvcDescription,Sched_Date,comp_date,BillSubtotal", Prefix = "ServiceEdit")] Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Id = Int32.Parse(this.Request.Form["ServiceEdit.Id"]);
                    ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
                    await context.UpdateAsync(service);
                    await context.InsertServiceAsync(service);
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }

            return RedirectToAction(nameof(Index));

        }




        // GET: Service/Delete/5
        public ActionResult Delete(int id)
        {
            ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
            string name = "";
            Service service = context.FindOneAsyncByName(id, ref name);
            ViewBag.name = name;
            return View(service);
        }//Completed



        // POST: Service/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            ServicesContext context = HttpContext.RequestServices.GetService(typeof(ServicesContext)) as ServicesContext;
            context.Delete(id);
            return RedirectToAction(nameof(Index));
        }//Completed



    }
}
