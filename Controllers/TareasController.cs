using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigestProAPI.Data;
using SigestProAPI.Models;

namespace SigestProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            return await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.Empleado)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null) return NotFound();
            return tarea;
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> CreateTarea(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id) return BadRequest();

            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Prediccion IA
        [HttpPost("prediccion")]
        public async Task<ActionResult<string>> PredecirRiesgo([FromBody] Tarea tarea)
        {
            using (var client = new HttpClient())
            {
                // URL del microservicio en Python (lo tendremos en Flask/FastAPI)
                var url = "http://localhost:5000/predict";

                var response = await client.PostAsJsonAsync(url, new
                {
                    tareas_asignadas = tarea.TareasAsignadas,
                    retrasos_previos = tarea.RetrasosPrevios,
                    complejidad = tarea.Complejidad,
                    recursos = tarea.Recursos
                });

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al conectar con el modelo IA");
                }

                var resultado = await response.Content.ReadAsStringAsync();
                return Ok(resultado);
            }
        }

    }
}
