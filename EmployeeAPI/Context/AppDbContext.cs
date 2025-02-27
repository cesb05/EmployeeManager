using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        //Modelo a utilizar para la tabla Empleados
        public DbSet<Employee> Employees { get; set; }
    }
}