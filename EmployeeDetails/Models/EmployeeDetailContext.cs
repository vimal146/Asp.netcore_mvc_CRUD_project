using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Models
{
    public class EmployeeDetailContext : DbContext
    {
        public EmployeeDetailContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<Employee> EmployeesDetail { get; set; }
    }
}
