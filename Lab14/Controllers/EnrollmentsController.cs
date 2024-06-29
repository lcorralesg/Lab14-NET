using Lab14.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab14.Request;

namespace Lab14.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Enroll([FromBody] EnrollmentRequestV1 request)
        {
            var student = _context.Students.Find(request.IdStudent);

            if (student == null)
            {
                return NotFound();
            }

            foreach (var courseId in request.IdCourses)
            {
                var course = _context.Courses.Find(courseId);

                if (course == null)
                {
                    return NotFound();
                }

                var enrollment = new Enrollment
                {
                    Student = student,
                    Course = course
                };

                _context.Enrollments.Add(enrollment);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
