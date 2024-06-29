using Lab14.Models;
namespace Lab14.Request
{
    public class StudentRequestV4
    {
        public int IdGrade { get; set; }
        public List<Student> Students { get; set; }
    }
}
