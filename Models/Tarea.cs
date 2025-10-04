using System.Text.Json.Serialization;

namespace SigestProAPI.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        [JsonIgnore]
        public Proyecto? Proyecto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinPlan { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public int Prioridad { get; set; } = 2;
        public int? EmpleadoId { get; set; }
        public Usuario? Empleado { get; set; }

        // Campos para IA
        public int TareasAsignadas { get; set; } = 0;
        public int RetrasosPrevios { get; set; } = 0;
        public int Complejidad { get; set; } = 2;
        public int Recursos { get; set; } = 1;
        public string? Riesgo { get; set; }
    }
}
