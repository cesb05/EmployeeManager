namespace EmployeeWebApp.DTO
{
    public class EmployeeDTO
    {
        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string Email { get; set; }

        public required string? Telefono { get; set; }

        public required decimal Salario { get; set; }

        public DateTime FechaIngreso { get; set; }
    }
}