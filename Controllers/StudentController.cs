using Microsoft.AspNetCore.Mvc;
using StudentDAL;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly StudentDAL.StudentDAL _studentDal;
        //readonly: get intialized only once, cannot be reassigned
        //private readonly StudentDAL.StudentDAL studentDAL;

        public StudentController(StudentDAL.StudentDAL studentDAL)
        {
            this._studentDal = studentDAL;
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
            var students = _studentDal.GetAllStudents();
            return Ok(students);
        }

        [HttpPost("Add")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _studentDal.InsertStudent(student);
            return Ok("Student added");
        }

        [HttpPut("Update")]
        public IActionResult UpdateStudent([FromBody] Student student)
        {
            _studentDal.UpdateStudent(student);
            return Ok("Student updated");
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentDal.DeleteStudent(id);
            return Ok("Student deleted");
        }

        [HttpGet("CheckExists/{nationalId}")]
        public IActionResult CheckExists(string nationalId)
        {
            bool exists = _studentDal.CheckStudentExists(nationalId);
            return Ok(exists);
        }
    }
}
