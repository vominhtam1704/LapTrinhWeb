using Microsoft.AspNetCore.Mvc;
using ungdungdautien.Models;
using System.Collections.Generic;
using System.Linq;

namespace ungdungdautien.Controllers
{
    public class StudentController : Controller
    {
        // Lưu danh sách SV đăng ký tạm thời
        private static List<Student> registeredStudents = new List<Student>();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowKQ(Student student)
        {
            registeredStudents.Add(student);
            int sameMajorCount = registeredStudents.Count(s => s.Major == student.Major);

            ViewBag.MSSV = student.MSSV;
            ViewBag.FullName = student.FullName;
            ViewBag.Major = student.Major;
            ViewBag.CountSameMajor = sameMajorCount;

            return View();

        }
    }
}