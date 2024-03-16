using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails.Models
{
    public class Employee
    {
        [Key]
        public  int  EmployeeId  { get; set; }
        public string EmployeeName { get; set; } = "";
        public string EmployeeDOB { get; set; } = "";
        public string EmployeePh { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
