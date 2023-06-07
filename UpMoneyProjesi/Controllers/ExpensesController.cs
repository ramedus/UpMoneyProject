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
    public class ExpensesController : Controller
    {
        private readonly WalletContext _context;

        public ExpensesController(WalletContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var walletContext = _context.Expenses.Include(e => e.Customer).Include(e => e.ExpensesType);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;
                
            }
            return View(list.ToList());
        }
        public async Task<IActionResult> EditExpense(int? id)
        {
            var walletContext = _context.Expenses.Include(e => e.Customer).Include(e => e.ExpensesType);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;

            }
            return View(list.ToList());
        }
        [HttpPost]
        
        public async Task<IActionResult> EditExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Customer)
                .Include(e => e.ExpensesType)
                .FirstOrDefaultAsync(m => m.ExpensesId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        public IActionResult CreateExpense()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");
            ViewData["ExpensesTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName");
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
        public IActionResult CreateExpense(int ExpensesFee, int ExpensesTypeId, int CustomerId)
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
                    ExpensesTypeId = ExpensesTypeId,
                    ExpensesFee = ExpensesFee,
                    CustomerId = CustomerId
                });

            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");
            ViewData["ExpensesTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName");
            return View();

        }

        public async Task<IActionResult> Createis([Bind("ExpensesId,ExpensesTypeId,ExpensesFee,CustomerId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", expense.CustomerId);
            ViewData["ExpensesTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", expense.ExpensesTypeId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", expense.CustomerId);
            ViewData["ExpensesTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", expense.ExpensesTypeId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpensesId,ExpensesTypeId,ExpensesFee,CustomerId")] Expense expense)
        {
            if (id != expense.ExpensesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpensesId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", expense.CustomerId);
            ViewData["ExpensesTypeId"] = new SelectList(_context.ExpensesTypes, "ExpensesTypeId", "ExpensesName", expense.ExpensesTypeId);
            return View(expense);
        }

        // GET: Expenses/Delete/5






        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpensesId == id);
        }
    }
}
