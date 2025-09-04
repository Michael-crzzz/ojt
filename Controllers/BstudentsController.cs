using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class BstudentsController : Controller
    {
        private readonly StudentContext dbContext;

        public BstudentsController(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBStudentViewModel viewModel)
        {
            var bstudent = new BStudent
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Course = viewModel.Course,
                Enrolled = viewModel.Enrolled,
            };

            await dbContext.BSutends.AddAsync(bstudent);

            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.BSutends.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.BSutends.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BStudent viewModel)
        {
            var student = await dbContext.BSutends.FindAsync(viewModel.Id);

            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Course = viewModel.Course;
                student.Enrolled = viewModel.Enrolled;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "BStudents");
        }
    }
}
