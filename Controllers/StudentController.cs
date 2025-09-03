using Microsoft.AspNetCore.Mvc;
using mvcrud.Models;
using mvcrud.Services;
using System.Linq;

namespace mvcrud.Controllers
{
    public class StudentsController(StudentRepository repo) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var students = await repo.GetAllAsync();
            return View(students);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student s)
        {
            if (!ModelState.IsValid) return View(s);
            await repo.CreateAsync(s);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await repo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student s)
        {
            if (!ModelState.IsValid) return View(s);
            await repo.UpdateAsync(s);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await repo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
