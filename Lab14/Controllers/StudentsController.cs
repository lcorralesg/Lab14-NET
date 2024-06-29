using Lab14.Models;
using Lab14.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Lab14.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InsertStudent([FromBody] StudentRequestV1 request)
        {
            var student = new Student
            {
                GradeID = request.GradeID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateStudentContact([FromBody] StudentRequestV2 request)
        {
            var student = _context.Students.Find(request.Id);

            if (student == null)
            {
                return NotFound();
            }

            student.Phone = request.Phone;
            student.Email = request.Email;

            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateStudentPersonalData([FromBody] StudentRequestV3 request)
        {
            var student = _context.Students.Find(request.Id);

            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult InsertStudentsByGrade([FromBody] StudentRequestV4 request)
        {
            var grade = _context.Grades.Find(request.IdGrade);

            if (grade == null)
            {
                return NotFound();
            }

            foreach (var studentRequest in request.Students)
            {
                var student = new Student
                {
                    GradeID = request.IdGrade,
                    FirstName = studentRequest.FirstName,
                    LastName = studentRequest.LastName,
                    Phone = studentRequest.Phone,
                    Email = studentRequest.Email
                };

                _context.Students.Add(student);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
