using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        //-----------------  PEDIIDOOOOO     -------------------------------------

        [HttpPost]
        public async Task<IActionResult> Pedido(int idCliente, List<ItemPedido> itensCarrinho)
        {
            var pedido = new Pedido
            {
                idCliente = idCliente,
                dataPed = DateTime.Now,
                totalPed = itensCarrinho.Sum(i => i.Produto.precoProd * i.quantidade),
                ItemPedido = itensCarrinho.Select(i => new ItemPedido
                {
                    idProd = i.idProd,
                    quantidade = i.quantidade,
                    precoUnitario = i.Produto.precoProd
                }).ToList()
            };

            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction("MeusPedidos"); // Redireciona para a página dos pedidos após a criação
        }



        [HttpGet]
        public async Task<IActionResult> MeusPedidos()
        {
            //pega a sessao do cleinte
            int? idCliente = HttpContext.Session.GetInt32("idCliente");     
              //AARRRUMAR ISSO AQUIII
         //   if (idCliente == null)
          //  {
                // se não tiver logado, redirecionar para a tela de login
         ///       return RedirectToAction("Login", "Logins");
           // }


            var pedidos = await _context.Pedido
                .Include(p => p.ItemPedido).ThenInclude(i => i.Produto)
                .Where(p => p.idCliente == idCliente)
                .ToListAsync();

            return View(pedidos);
        }

        //REMOVER PRODUTOOO DA SACOLAAA
        [HttpPost]
        public async Task<IActionResult> RemoverItem(int id)
        {
            var item = await _context.ItemPedido.FindAsync(id);
            if (item != null)
            {
                _context.ItemPedido.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MeusPedidos");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItem(int idProduto, int quantidade = 1)
        {
            // 1. Verificar login
            var idCliente = HttpContext.Session.GetInt32("idCliente");
            if (idCliente == null)
            {
                return RedirectToAction("Login", "Logins");
            }

            // 2. Buscar produto
            var produto = await _context.Produtos.FindAsync(idProduto);
            if (produto == null)
            {
                return NotFound();
            }

            // 3. Verificar pedido existente (não finalizado)
            var pedido = await _context.Pedido
    .Include(p => p.ItemPedido)
    .ThenInclude(i => i.Produto)
    .Include(p => p.Pagamento) // Carrega os dados de pagamento
    .FirstOrDefaultAsync(p => p.idCliente == idCliente &&
                            (p.Pagamento == null ||
                             p.Pagamento.statusPag == "Pendente"));

            // Criar novo pedido se necessário
            if (pedido == null)
            {
                pedido = new Pedido
                {
                    idCliente = idCliente.Value,
                    dataPed = DateTime.Now,
                    Pagamento = new Pagamento
                    {
                        statusPag = "Pendente",
                        metodoPag = "Não definido"
                    },
                    ItemPedido = new List<ItemPedido>()
                };
                _context.Pedido.Add(pedido);
            }


            // 5. Adicionar/atualizar item
            var itemExistente = pedido.ItemPedido.FirstOrDefault(i => i.idProd == idProduto);
            if (itemExistente != null)
            {
                itemExistente.quantidade += quantidade;
            }
            else
            {
                pedido.ItemPedido.Add(new ItemPedido
                {
                    idProd = idProduto,
                    quantidade = quantidade,
                    precoUnitario = produto.precoProd,
                    Produto = produto
                });
            }

            // 6. Atualizar total
            pedido.totalPed = pedido.ItemPedido.Sum(i => i.quantidade * i.precoUnitario);

            await _context.SaveChangesAsync();

            return RedirectToAction("MeusPedidos");
        }

    }
}
