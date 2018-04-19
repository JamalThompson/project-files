using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASL.Data;
using ASL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Expenses
        public ActionResult Index()
        {
            List<Expenses> expenses = new List<Expenses>();
            ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
            expenses = context.FindAll();

            return View(expenses);
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Type, Total, SvcType")]Expenses expenses)
        {
            try
            {
                ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
                await context.InsertExpensesAsync(expenses);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
                Expenses expenses = context.FindOneAsync(id);

                return View(expenses);
            }
            catch
            {
                return View();
            }
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Type, Total, SvcType")]Expenses expenses)
        {
            try
            {
                ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
                context.UpdateAsync(expenses);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int id)
        {
            ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
            Expenses expenses = context.FindOneAsync(id);

            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // POST: Expenses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ExpensesContext context = HttpContext.RequestServices.GetService(typeof(ExpensesContext)) as ExpensesContext;
                context.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
    }
}