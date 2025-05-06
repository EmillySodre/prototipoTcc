using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prototipo1204.Data;
using prototipo1204.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using prototipo1204.Repositorios;
using prototipo1204.Repositorios.Interface;


namespace prototipo1204.Controllers
{
    public class ClientesController : Controller
    {
        private readonly oceanidDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientesController(oceanidDBContext context, IHttpContextAccessor httpcontextacessor, ILoginRepositorio loginRepositorio)
        {
            _context = context;
            _httpContextAccessor = httpcontextacessor;
        }

        


        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var oceanidDBContext = _context.Clientes.Include(c => c.endereco);
            return View(await oceanidDBContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.endereco)
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCliente,cpf,nomeCompleto,senhaCliente,emailCliente,dataNasc,idEnd")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", cliente.idEnd);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", cliente.idEnd);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCliente,cpf,nomeCompleto,senhaCliente,emailCliente,dataNasc,idEnd")] Cliente cliente)
        {
            if (id != cliente.idCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.idCliente))
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
            ViewData["idEnd"] = new SelectList(_context.Enderecos, "idEnd", "idEnd", cliente.idEnd);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.endereco)
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.idCliente == id);
        }
        public async Task<IActionResult> Cadastro(Cliente cliente)
        {
            // COLOCAR ISSO NA CONTYROLLER DO ADMMM  cliente.datacad_User = DateTime.Now;
            //Cliente cliente = await _context.Clientes.FirstOrDefault(c => c.idUser == usuario.idUser);
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            // Criando a lista de claims
            //Claims são um tipo de identificadores do usuario
            var claims = new List<Claim> // guarda os dados dos usuarios
            {
                new Claim(ClaimTypes.Name, cliente.emailCliente),
                new Claim(ClaimTypes.SerialNumber, Convert.ToString(cliente.idCliente)),
                // Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber)?.Value)
                new Claim(ClaimTypes.Role, "Cliente")
            };
            //[Authorize(Roles = "Usuario")] para tipos especificos
            //[Authorize] logado

            //Criando o Claim de identidade do usuario, juntamente de coockies
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Permite que o usuario continue logado mesmo se fechar o navegador
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Mantém o cookie ao fechar o navegador
            };
            //Vai logar o usuario com o HTTP usando tanto os coockies quanto a identidade do usuario
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            TempData["Login"] = "Cadastro efetuado com sucesso!!!";
            return RedirectToAction("Index", "Home");



        }

     
    }

}
