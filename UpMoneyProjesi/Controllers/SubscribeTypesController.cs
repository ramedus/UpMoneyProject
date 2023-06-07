using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UpMoneyProjesi.Models;

namespace UpMoneyProjesi.Controllers
{
    public class SubscribeTypesController : Controller
    {
        private readonly WalletContext _context;

        public SubscribeTypesController(WalletContext context)
        {
            _context = context;
        }

        // GET: SubscribeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubscribeTypes.ToListAsync());
        }

        // GET: SubscribeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeType = await _context.SubscribeTypes
                .FirstOrDefaultAsync(m => m.SubscribeTypeId == id);
            if (subscribeType == null)
            {
                return NotFound();
            }

            return View(subscribeType);
        }

        // GET: SubscribeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubscribeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubscribeTypeId,SubscribeName")] SubscribeType subscribeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscribeType);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateSubscribe", "MySubscribes");
            }
            return View(subscribeType);
        }

        // GET: SubscribeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeType = await _context.SubscribeTypes.FindAsync(id);
            if (subscribeType == null)
            {
                return NotFound();
            }
            return View(subscribeType);
        }

        // POST: SubscribeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubscribeTypeId,SubscribeName")] SubscribeType subscribeType)
        {
            if (id != subscribeType.SubscribeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscribeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscribeTypeExists(subscribeType.SubscribeTypeId))
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
            return View(subscribeType);
        }

        // GET: SubscribeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeType = await _context.SubscribeTypes
                .FirstOrDefaultAsync(m => m.SubscribeTypeId == id);
            if (subscribeType == null)
            {
                return NotFound();
            }

            return View(subscribeType);
        }

        // POST: SubscribeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscribeType = await _context.SubscribeTypes.FindAsync(id);
            _context.SubscribeTypes.Remove(subscribeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscribeTypeExists(int id)
        {
            return _context.SubscribeTypes.Any(e => e.SubscribeTypeId == id);
        }
    }
}
