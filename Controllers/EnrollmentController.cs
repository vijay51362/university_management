using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using university_management.Data;
using university_management.Models;

namespace university_management.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly UniversityContext _context;

        public EnrollmentController(UniversityContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index()
        {
            var universityContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await universityContext.ToListAsync());
        }

        // GET: Enrollment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Enrollment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrollmentDate,Grade")] Enrollment enrollment)
        {
            Console.WriteLine("Create action hit");
            Console.WriteLine($"enrollment Data: StudentId={enrollment.StudentId}, CourseId={enrollment.CourseId}, EnrollmentDate={enrollment.EnrollmentDate}, Grade={enrollment.Grade}");

            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors for debugging
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            PopulateDropdowns(enrollment);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            PopulateDropdowns(enrollment);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrollmentDate,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Log validation errors for debugging
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            PopulateDropdowns(enrollment);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }

        private void PopulateDropdowns(Enrollment enrollment = null)
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", enrollment?.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment?.StudentId);
        }
    }
}
