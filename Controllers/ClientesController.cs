using DeudasAPI.Data;
using DeudasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeudasAPI.Controllers
{
    [ApiController]
    [Route("/api/clientes")]
   
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

       

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok(new { mensaje = "¡Clientes cargados!" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }



        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            // Agregar el cliente al contexto de la base de datos
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Retorna un 201 Created con la ruta para obtener el cliente creado
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("El cliente a eliminar no fue encontrado.");
            }

            // Remueve el cliente del contexto y persiste el cambio
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            // Retorna NoContent tras la eliminación
            return NoContent();
        }

        // Método auxiliar para verificar si existe un cliente con el ID dado
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        // Puedes agregar más métodos: POST, PUT, DELETE, etc.
    }
}
