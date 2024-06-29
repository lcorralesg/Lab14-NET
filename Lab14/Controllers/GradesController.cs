using Lab14.Models;
using Lab14.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab14.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InsertGrade([FromBody] GradeRequestV1 request)
        {
            var grade = new Grade
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteGrade([FromBody] GradeRequestV2 request)
        {
            var grade = _context.Grades.Find(request.Id);

            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            _context.SaveChanges();

            return Ok();
        }
    }
}
