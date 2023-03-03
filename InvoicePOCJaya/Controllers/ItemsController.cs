using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoicePOCJaya.Models;

namespace InvoicePOCJaya.Controllers
{
    public class ItemsController : Controller
    {
        private readonly invoiceContext _context;

        public ItemsController(invoiceContext context)
        {
            _context = context;
        }

        // GET: Items
        

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.ItemGuid == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string InvoiceId, string ItemName, int Quantity, string UnitPrice, string LineTotal)
        {

            Item I = new Item();
            var invoice = _context.Invoices.FirstOrDefault(i => i.InvoiceID == InvoiceId);
            I.InvoiceGuid = invoice.InvoiceGuid;
            I.ItemName = ItemName;
            I.Quantity = Quantity;
            I.UnitPrice = UnitPrice;
            I.LineTotal = LineTotal;
            _context.Add(I);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            //return View(food);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.Include(x => x.Invoice).ToListAsync());
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["InvoiceGuid"] = new SelectList(_context.Invoices, "InvoiceGuid", "InvoiceGuid", item.InvoiceGuid);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemGuid,ItemName,Quantity,UnitPrice,LineTotal,InvoiceGuid")] Item item)
        {
            if (id != item.ItemGuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemGuid))
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
            ViewData["InvoiceGuid"] = new SelectList(_context.Invoices, "InvoiceGuid", "InvoiceGuid", item.InvoiceGuid);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.ItemGuid == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'invoiceContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
          return _context.Items.Any(e => e.ItemGuid == id);
        }
    }
}
