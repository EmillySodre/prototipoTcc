using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipo1204.Data;
using prototipo1204.Models;

namespace prototipo1204.Controllers
{
    public class ItemPedidoesController : Controller
    {
        private readonly oceanidDBContext _context;

        public ItemPedidoesController(oceanidDBContext context)
        {
            _context = context;
        }

        // GET: ItemPedidoes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.ItemPedido.Include(i => i.Pedido).Include(i => i.Produto);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: ItemPedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedido
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.idProdutoPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // GET: ItemPedidoes/Create
        public IActionResult Create()
        {
            ViewData["idPedido"] = new SelectList(_context.Pedido, "idPed", "idPed");
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd");
            return View();
        }

        // POST: ItemPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProdutoPedido,idPedido,idProd,quantidade,precoUnitario")] ItemPedido itemPedido)
        {
           
                _context.Add(itemPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["idPedido"] = new SelectList(_context.Pedido, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedido.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            ViewData["idPedido"] = new SelectList(_context.Pedido, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // POST: ItemPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProdutoPedido,idPedido,idProd,quantidade,precoUnitario")] ItemPedido itemPedido)
        {
            if (id != itemPedido.idProdutoPedido)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoExists(itemPedido.idProdutoPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["idPedido"] = new SelectList(_context.Pedido, "idPed", "idPed", itemPedido.idPedido);
            ViewData["idProd"] = new SelectList(_context.Produtos, "idProd", "idProd", itemPedido.idProd);
            return View(itemPedido);
        }

        // GET: ItemPedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedido
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.idProdutoPedido == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedido = await _context.ItemPedido.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItemPedido.Remove(itemPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItemPedido.Any(e => e.idProdutoPedido == id);
        }
    }
}
