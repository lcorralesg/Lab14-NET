using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab14.Request;
using Lab14.Models;

namespace Lab14.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InsertCourse([FromBody] CourseRequestV1 request)
        {
            var course = new Course
            {
                Name = request.Name,
                Credit = request.Credit
            };

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IActionResult DeleteCourse([FromBody] CourseRequestV2 request)
        {
            var course = _context.Courses.Find(request.Id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCourses([FromBody] CourseRequestV3 request)
        {
            foreach (var id in request.Ids)
            {
                var course = _context.Courses.Find(id);

                if (course == null)
                {
                    return NotFound();
                }

                _context.Courses.Remove(course);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
