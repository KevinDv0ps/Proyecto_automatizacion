namespace SigestProAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Empleado";
        public string? PasswordHash { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
