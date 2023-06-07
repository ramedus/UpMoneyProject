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
    public class MySubscribesController : Controller
    {
        private readonly WalletContext _context;

        public MySubscribesController(WalletContext context)
        {
            _context = context;
        }

        // GET: MySubscribes
        public async Task<IActionResult> Index()
        {
            var walletContext = _context.MySubscribes.Include(m => m.Customer).Include(m => m.SubscribeType);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;

            }
            return View(list.ToList());
        }
        public async Task<IActionResult> EditSubscribe(int? id)
        {
            var walletContext = _context.MySubscribes.Include(m => m.Customer).Include(m => m.SubscribeType);
            string member = HttpContext.Session.GetString("customer");
            var list = walletContext.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.Customer.CustomerName;

            }
            return View(list.ToList());
        }
        [HttpPost]

        public async Task<IActionResult> EditSubscribe(int id)
        {
            var subscribe = await _context.MySubscribes.FindAsync(id);
            if (subscribe != null)
            {
                _context.MySubscribes.Remove(subscribe);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: MySubscribes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubscribe = await _context.MySubscribes
                .Include(m => m.Customer)
                .Include(m => m.SubscribeType)
                .FirstOrDefaultAsync(m => m.SubscribeId == id);
            if (mySubscribe == null)
            {
                return NotFound();
            }

            return View(mySubscribe);
        }
        public IActionResult CreateSubscribe()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");
            ViewData["SubscribeTypeId"] = new SelectList(_context.SubscribeTypes, "SubscribeTypeId", "SubscribeName");
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
        public IActionResult CreateSubscribe(int SubscribeTypeId, int SubscribeValue, int CustomerId)
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
                    SubscribeTypeId = SubscribeTypeId,
                    SubscribeValue = SubscribeValue,
                    CustomerId = CustomerId
                });

            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail");
            ViewData["SubscribeTypeId"] = new SelectList(_context.SubscribeTypes, "SubscribeTypeId", "SubscribeName");
            return View();

        }
        
        public async Task<IActionResult> Createis([Bind("SubscribeId,SubscribeTypeId,SubscribeValue,CustomerId")] MySubscribe mySubscribe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mySubscribe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", mySubscribe.CustomerId);
            ViewData["SubscribeTypeId"] = new SelectList(_context.SubscribeTypes, "SubscribeTypeId", "SubscribeName", mySubscribe.SubscribeTypeId);
            return View(mySubscribe);
        }

        // GET: MySubscribes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubscribe = await _context.MySubscribes.FindAsync(id);
            if (mySubscribe == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", mySubscribe.CustomerId);
            ViewData["SubscribeTypeId"] = new SelectList(_context.SubscribeTypes, "SubscribeTypeId", "SubscribeName", mySubscribe.SubscribeTypeId);
            return View(mySubscribe);
        }

        // POST: MySubscribes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubscribeId,SubscribeTypeId,SubscribeValue,CustomerId")] MySubscribe mySubscribe)
        {
            if (id != mySubscribe.SubscribeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mySubscribe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MySubscribeExists(mySubscribe.SubscribeId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", mySubscribe.CustomerId);
            ViewData["SubscribeTypeId"] = new SelectList(_context.SubscribeTypes, "SubscribeTypeId", "SubscribeName", mySubscribe.SubscribeTypeId);
            return View(mySubscribe);
        }

        // GET: MySubscribes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mySubscribe = await _context.MySubscribes
                .Include(m => m.Customer)
                .Include(m => m.SubscribeType)
                .FirstOrDefaultAsync(m => m.SubscribeId == id);
            if (mySubscribe == null)
            {
                return NotFound();
            }

            return View(mySubscribe);
        }

        // POST: MySubscribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mySubscribe = await _context.MySubscribes.FindAsync(id);
            _context.MySubscribes.Remove(mySubscribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MySubscribeExists(int id)
        {
            return _context.MySubscribes.Any(e => e.SubscribeId == id);
        }
    }
}
