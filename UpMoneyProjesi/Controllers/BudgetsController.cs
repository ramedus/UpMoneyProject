using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UpMoneyProjesi.Models;

namespace UpMoneyProjesi.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly WalletContext _context;

        public BudgetsController(WalletContext context)
        {
            _context = context;
        }

        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            var walletContext = _context.Budgets.Include(b => b.BudgetType).Include(b => b.Customer);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;

            }
            return View(list.ToList());
            
        }
        public async Task<IActionResult> EditBudget(int? id)
        {
            var walletContext = _context.Budgets.Include(b => b.BudgetType).Include(b => b.Customer);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;

            }
            return View(list.ToList());
        }
        [HttpPost]

        public async Task<IActionResult> EditBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
                .Include(b => b.BudgetType)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BudgetId == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        public IActionResult CreateBudget()
        {
            ViewData["BudgetTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");
            string member = HttpContext.Session.GetString("customer");
            var list = _context.Customers.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.CustomerName;
                return View();
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateBudget(int Budget1, int BudgetTypeId, int CustomerId)
        {
            string member = HttpContext.Session.GetString("customer");
            var list = _context.Customers.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                CustomerId = item.CustomerId;
                ViewData["Message"] = item.CustomerName;
                return RedirectToAction("Createis",
                new
                {
                    Budget1 = Budget1,
                    BudgetTypeId = BudgetTypeId,
                    CustomerId = CustomerId
                });

            }
            ViewData["BudgetTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");

            return View();
        }
        public async Task<IActionResult> Createis([Bind("BudgetId,Budget1,BudgetTypeId,CustomerId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", budget.BudgetTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", budget.CustomerId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["BudgetTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", budget.BudgetTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", budget.CustomerId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetId,Budget1,BudgetTypeId,CustomerId")] Budget budget)
        {
            if (id != budget.BudgetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.BudgetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", budget.BudgetTypeId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", budget.CustomerId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
                .Include(b => b.BudgetType)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BudgetId == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.BudgetId == id);
        }
    }
}
