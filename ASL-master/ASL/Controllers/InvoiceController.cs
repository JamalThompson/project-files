using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASL.Data;
using ASL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                InvoiceContext context = HttpContext.RequestServices.GetService(typeof(InvoiceContext)) as InvoiceContext;
                invoices = context.FindAll();

                return View(invoices);
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Invoice/Create
        public async Task<IActionResult> Create()
        {

            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvNum,CustId,Subtotal,Total,Paid,PymntTerms,Bill_Date,Due_Date")] Invoice invoice)
        {
            try
            {

                //InvoiceContext context = HttpContext.RequestServices.GetService(typeof(InvoiceContext)) as InvoiceContext;

                //InvoiceLineItem invoiceLineItem = new InvoiceLineItem();
                //invoiceLineItem.InvNum = 145;
                //invoiceLineItem.CustId = 1;
                //invoiceLineItem.Total = 22;
                //await context.InsertInvoiceLIAsync(invoiceLineItem);

                //return View("Index");


                InvoiceContext context = HttpContext.RequestServices.GetService(typeof(InvoiceContext)) as InvoiceContext;
                await context.InsertInvoiceAsync(invoice);

                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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