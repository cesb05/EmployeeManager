using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebApp.Models
{
    [Table("Empleado")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El Nombre no puede tener más de 100 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El Apellido no puede tener más de 100 caracteres.")]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email no es una dirección de correo válida.")]
        [StringLength(100, ErrorMessage = "El Email no puede tener más de 100 caracteres.")]
        public required string Email { get; set; }

        [StringLength(20, ErrorMessage = "El Teléfono no puede tener más de 20 caracteres.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El campo 'Salario' es obligatorio.")]
        [Range(350.01, double.MaxValue, ErrorMessage = "El Salario debe ser un valor positivo y mayor a 350")]
        public required decimal Salario { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaIngreso { get; set; }
    }
}