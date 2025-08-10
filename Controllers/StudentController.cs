using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var student = await _unitOfWork.StudentRepository.GetStudents();
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,Email")]
        Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.StudentRepository.Add(student);
                    await _unitOfWork.SaveAsync();
                    TempData["successMessage"] = "Student has been added.";
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var student = await _unitOfWork.StudentRepository.GetStudentById(id);
            if (student == null) {
                return View();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Phone,Address,Email")]
        Student student)
        {
            try
            {
                if (id != student.Id)
                {
                    return View();
                }
                if (ModelState.IsValid)
                {
                    await _unitOfWork.StudentRepository.Update(student);
                    await _unitOfWork.SaveAsync();
                    TempData["successMessage"] = "Student has been updated.";
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var student = await _unitOfWork.StudentRepository.GetStudentById(id);
            if (student == null)
            {
                return View();
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var student = await _unitOfWork.StudentRepository.GetStudentById(id);
            if (student == null)
            {
                return View();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, [Bind("Id,Name,Phone,Address,Email")]
        Student student)
        {
            try
            {
                if (id != student.Id)
                {
                    return View();
                }
                if (ModelState.IsValid)
                {
                    await _unitOfWork.StudentRepository.Delete(id);
                    await _unitOfWork.SaveAsync();
                    TempData["successMessage"] = "Student has been deleted.";
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
