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
    public class WalletsController : Controller
    {
        private readonly WalletContext _context;

        public WalletsController(WalletContext context)
        {
            _context = context;
        }

        // GET: Wallets
        public async Task<IActionResult> Index()
        {
            var walletContext = _context.Wallets.Include(w => w.Customer);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            

            foreach (var item in list)
            {
                decimal expenses = _context.Expenses.Where(x => x.CustomerId == item.Customer.CustomerId).Sum(x => x.ExpensesFee);
                decimal subscribe = _context.MySubscribes.Where(x => x.CustomerId == item.Customer.CustomerId).Sum(x => x.SubscribeValue);
                decimal budget = _context.Budgets.Where(x => x.CustomerId == item.Customer.CustomerId).Sum(x => x.Budget1);
                ViewData["budget"] = budget;
                ViewData["Message"] = item.Customer.CustomerName;
                ViewData["expenses"] = expenses;
                ViewData["subscribe"] = subscribe;
                decimal sum = expenses + subscribe;
                ViewData["sum"] = sum;
                decimal balance = _context.Wallets
                 .Where(x => x.CustomerId == item.Customer.CustomerId)
                 .Select(x => x.Balance)
                 .FirstOrDefault();
                ViewData["balance"] = balance;
              
                return View();
            }
            return View(list.ToList());
        }

        // GET: Wallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.Customer)
                .FirstOrDefaultAsync(m => m.WalletId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult CreateWallet()
        {
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
        public IActionResult CreateWallet(int Balance, int CustomerId)
        {
            string member = HttpContext.Session.GetString("customer");
            var list = _context.Customers.Where(x => x.CustomerId.ToString() == member);
            var walletcount = _context.Wallets.Count(x => x.CustomerId.ToString() == member);
            if (walletcount > 0)
            {
                return BadRequest("You already have a wallet.");
            }
            foreach (var item in list)
            {
                CustomerId = item.CustomerId;
                ViewData["Message"] = item.CustomerName;
                return RedirectToAction("Createis",
                new
                {
                    Balance = Balance,
                    CustomerId = CustomerId
                });

            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");

            return View();
        }
        public async Task<IActionResult> Createis([Bind("WalletId,Balance,CustomerId")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", wallet.CustomerId);
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", wallet.CustomerId);
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalletId,Balance,CustomerId")] Wallet wallet)
        {
            if (id != wallet.WalletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.WalletId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", wallet.CustomerId);
            return View(wallet);
        }
        public IActionResult WalletSum(int id)
        {




            return View();
        }
        // GET: Wallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.Customer)
                .FirstOrDefaultAsync(m => m.WalletId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.WalletId == id);
        }
    }
}
