using Microsoft.AspNetCore.Mvc;
using StudentDAL;
using StudentDAL.Interfaces;
using StudentDAL.Models;

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


        /*
            IActionResult is interface that represents the result of an action method can return:
                OK() 200 , Created () 201 
                BadRequest() 400 , Unauthorized() 401, Forbidden() 403 , NotFound() 404 
                ServerError() 500
                View();
                Json()
                File()
                ETC....
         */
        [HttpGet("GetAll")]
        public IActionResult GetAllStudents([FromQuery] string? nationalId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            int startRow = ((page - 1) * pageSize) + 1;
            int endRow = page * pageSize;

            var students = _studentRepo.GetAllStudents(nationalId, startRow, endRow);

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
