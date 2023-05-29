using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_11050.Data;
using Web_11050.Models;
using Web_11050.Models.Domain;

namespace Web_11050.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        public EmployeesController(MVCDemoDbContext mvcdemoDbContext)
        {
            this.mvcDemoDbContext = mvcdemoDbContext;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addBookRequest)
        {
            var book = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addBookRequest.Name,
                Email = addBookRequest.Email,
                Price = addBookRequest.Price,
                Name_authour = addBookRequest.Name_authour

            };
            await mvcDemoDbContext.Employees.AddAsync(book);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {


                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Price = employee.Price,
                    Name_authour = employee.Name_authour
                };
                return await Task.Run(() => View("View", viewModel));

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {

            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;

                employee.Email = model.Email;
                employee.Price = model.Price;
                employee.Name_authour = model.Name_authour;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]

        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
   

}
