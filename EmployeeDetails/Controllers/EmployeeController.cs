using EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Mvc;
using PagedList;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeDetailContext _Context;

        public EmployeeController(EmployeeDetailContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var employee = await _Context.EmployeesDetail.ToListAsync();
			
			return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employeeRequest)
        {
            var employee = new Employee()
            {
                EmployeeId = employeeRequest.EmployeeId,
                EmployeeName = employeeRequest.EmployeeName,
                EmployeeDOB = employeeRequest.EmployeeDOB,
                EmployeePh = employeeRequest.EmployeePh,
                Description = employeeRequest.Description
            };
            
            await _Context.EmployeesDetail.AddAsync(employee);
            await _Context.SaveChangesAsync();
			TempData["SuccessMessage"] = "Details added successfully!";

			return RedirectToAction("Add");
        }

        public async Task<IActionResult> View(int id)
        {
            var employee = await _Context.EmployeesDetail.FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (employee != null)
            {
                var viewModel = new Employee()
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeDOB = employee.EmployeeDOB,
                    EmployeePh = employee.EmployeePh,
                    Description = employee.Description,
                };
				
				return await Task.Run(() => View("View", (viewModel)));

            }
			
			return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> View (Employee model)
        {
            var employee = await _Context.EmployeesDetail.FindAsync(model.EmployeeId);
            if(employee != null)
            {
                employee.EmployeeName = model.EmployeeName;
                employee.EmployeeDOB = model.EmployeeDOB;
                employee.EmployeePh = model.EmployeePh;
                employee.Description = model.Description;

				
				await _Context.SaveChangesAsync();
                
				
				return RedirectToAction("Index");
				
			}
			
			return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Delete (Employee model)
        {
            var employee = await _Context.EmployeesDetail.FindAsync(model.EmployeeId);

            if (employee != null)
            {
                _Context.EmployeesDetail.Remove(employee);
                await _Context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
