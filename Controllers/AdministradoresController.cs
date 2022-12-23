using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Servico;
using EntityFrameworkPaginateCore;
using webapi.ModelViews;

namespace webapi.Controllers
{
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly DbContexto _context;

        public AdministradoresController(DbContexto context)
        {
            _context = context;
        }

        // GET: /administradores
        [HttpGet]
        [Route("/administradores")]
        public async Task<IActionResult> Index()
        {
            var administradores = await _context.Administradores.Select(a => new{
                            Id = a.Id,
                            Nome = a.Nome,
                            Email = a.Email        
                            // etc, don't include the image column
                        }).ToListAsync();

            return StatusCode(200, administradores );
        }

        // POST: /administradores/{id}
        [HttpPost]
        [Route("/administradores/login")]
        public async Task<IActionResult> Login([FromBody] AdmLoginView adm)
        {
            if (string.IsNullOrWhiteSpace(adm.Email) || string.IsNullOrWhiteSpace(adm.Senha))
            {
                return StatusCode(400, new {
                    Mensagem = "É Obrigatório preencher Email e Senha válidos."
                });
            }

            var administrador = (await _context.Administradores.Where(a => a.Email == adm.Email && a.Senha == adm.Senha).FirstAsync());
            if (administrador != null)
            {
                return StatusCode(200, new{
                    Id = administrador.Id,
                    Nome = administrador.Nome,
                    Email = administrador.Email
                });
            }

            return StatusCode(401, new {
                    Mensagem = "Usuário ou Senha inválidos." 
                });
            
        }

        // GET: /administradores/{id}
        [HttpGet]
        [Route("/administradores/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrador = await _context.Administradores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            return StatusCode(200, new{
                Id = administrador.Id,
                Nome = administrador.Nome,
                Email = administrador.Email
            });
        }

        
        // POST: /administradores
        [HttpPost]
        [Route("/administradores")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Matricula,Notas")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrador);
                await _context.SaveChangesAsync();
                return StatusCode(201, administrador);
            }
            return StatusCode(201, administrador);
        }

        
        // PUT: /administradores/{id}
        [HttpPut]
        [Route("/administradores/{id}")]
        public async Task<IActionResult> Edit(int id, Administrador administrador)
        {

            if (ModelState.IsValid)
            {
                try
                {   administrador.Id = id;
                    _context.Update(administrador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministradorExists(administrador.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return StatusCode(200, administrador);
            }
            return StatusCode(200, administrador);
        }


        // DELETE: /administradores/{id}
        [HttpDelete]
        [Route("/administradores/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrador = await _context.Administradores.FindAsync(id);
            _context.Administradores.Remove(administrador);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }

            private bool AdministradorExists(int id)
        {
            return _context.Administradores.Any(e => e.Id == id);
        }
    }
}
