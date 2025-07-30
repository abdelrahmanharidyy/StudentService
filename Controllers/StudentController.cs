using Microsoft.AspNetCore.Mvc;
using StudentDAL;
using StudentDAL.Interfaces;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepo = studentRepository;
        }

        [HttpGet("GetAll")]
        /*
            IActionResult is interface that represents the result of an action method can return:
                OK() 200
                NotFound() 404
                BadRequest() 400
                View();
                Json()
                File()
                ETC....
         */
        public IActionResult GetAllStudents()
        {
            var students = _studentRepo.GetAllStudents();
            return Ok(students);
        }

        [HttpPost("Add")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _studentRepo.InsertStudent(student);
            return Ok("Student added");
        }

        [HttpPut("Update")]
        public IActionResult UpdateStudent([FromBody] Student student)
        {
            _studentRepo.UpdateStudent(student);
            return Ok("Student updated");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentRepo.DeleteStudent(id);
            return Ok("Student deleted");
        }

        [HttpGet("CheckExists/{nationalId}")]
        public IActionResult CheckExists(string nationalId)
        {
            bool exists = _studentRepo.CheckStudentExists(nationalId);
            return Ok(exists);
        }
    }
}
