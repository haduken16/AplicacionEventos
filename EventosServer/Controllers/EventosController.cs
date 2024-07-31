using EventosServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventosServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly EventosContext _context;

        public EventosController(EventosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> CrearEvento(Evento evento) {
            await _context.Eventos.AddAsync(evento);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<IEnumerable<Evento>>> ListaEventos() {
            var eventos = await _context.Eventos.ToListAsync();
            return Ok(eventos);
        }

        [HttpGet]
        [Route("ListaXId")]
        public async Task<IActionResult> ListaEventoXId(int id){
            Evento evento = await _context.Eventos.FindAsync(id);

            if(evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> ActualizarEvento(int id, Evento evento)
        {
            var eventoExistente = await _context.Eventos.FindAsync(id);

            if (eventoExistente == null)
            {
                return NotFound();
            }

            eventoExistente.FechaEvento = evento.FechaEvento;
            eventoExistente.Lugar = evento.Lugar;
            eventoExistente.Descripcion = evento.Descripcion;
            eventoExistente.Precio = evento.Precio;

            await _context.SaveChangesAsync();

            return Ok();            
        }

        [HttpPut]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarEvento(int id)
        {
            Evento evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            if (evento.Estado == "Eliminado")
            {
                return BadRequest("El evento ya se encuentra eliminado");
            }

            evento.Estado = "Eliminado";
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
