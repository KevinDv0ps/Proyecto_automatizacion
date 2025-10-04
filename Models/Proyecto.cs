using System.Text.Json.Serialization;
using System.Threading;

namespace SigestProAPI.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPlan { get; set; }
        public string Estado { get; set; } = "Activo";
        public int? GerenteId { get; set; }
        public Usuario? Gerente { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
