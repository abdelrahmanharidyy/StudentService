
using Microsoft.AspNetCore.Mvc;
using StudentDAL.Interfaces;
using StudentDAL.Models;
namespace CollegeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController : Controller
    {
        private readonly ICollegeRepository _collegeRepository;

        public CollegeController( ICollegeRepository collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllColleges()
        {
            var colleges = _collegeRepository.GetAllColleges();
            return Ok(colleges);
        }

        [HttpPost("Add")]
        public IActionResult AddCollege([FromBody] Colleges college)
        {
            _collegeRepository.InsertCollege(college);
            return Ok("College added");
        }

        [HttpPut("Update")]
        public IActionResult UpdateCollege([FromBody] Colleges college)
        {
            _collegeRepository.UpdateCollege(college);
            return Ok("College updated");
        }
        
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCollege(int id)
        {
            _collegeRepository.DeleteCollege(id);
            return Ok("College deleted");
        }
    }
}
