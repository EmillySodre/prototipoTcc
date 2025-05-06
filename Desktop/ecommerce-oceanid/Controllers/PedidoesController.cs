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
    public class PedidoesController : Controller
    {
        private readonly oceanidDBContext _context;

        public PedidoesController(oceanidDBContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.Pedido.Include(p => p.Cliente).Include(p => p.Endereco).Include(p => p.Pagamento);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Endereco)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(m => m.idPed == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente");
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd");
            ViewData["idPag"] = new SelectList(_context.Pagamento, "idPag", "idPag");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idPed,idCliente,idEnd,idPag,dataPed,totalPed")] Pedido pedido)
        {
           
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamento, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamento, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idPed,idCliente,idEnd,idPag,dataPed,totalPed")] Pedido pedido)
        {
            if (id != pedido.idPed)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.idPed))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["idCliente"] = new SelectList(_context.Clientes, "idCliente", "idCliente", pedido.idCliente);
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", pedido.idEnd);
            ViewData["idPag"] = new SelectList(_context.Pagamento, "idPag", "idPag", pedido.idPag);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Endereco)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(m => m.idPed == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.idPed == id);
        }
    }
}
