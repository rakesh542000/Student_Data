using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Business;
using Student.Business.Business;
using Student.Enum;
using Student.Model;

namespace Student_Data_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("Students")]
        public ActionResult<List<studentModel>> GetStudents() => StudentBusiness.GetAllStudents();

        [HttpGet("Marksheets")]
        public ActionResult<List<Marksheet>> GetMarksheets() => StudentBusiness.GetMarksheets();

        [HttpGet("Marksheets/GetTotalMarksObtained/{id}")]
        public ActionResult<double> GetTotalMarksObtained(int id)
        {
            var res = StudentBusiness.GetTotalMarkObtained(id);
            if (res == -1) return NotFound();
            return res;
        }

        [HttpGet("Marksheets/GetAllMarksById/{id}")]
        public ActionResult<List<SubjectAndMarks>> GetAllMarksById(int id)
        {
            var res = StudentBusiness.GetAllMarksById(id);
            if (res == null) return NotFound();
            return res;
        }

        [HttpGet("Marksheets/GetTotalPercentageObtained/{id}")]
        public ActionResult<SubjectPercentage> GetTotalPercentageObtained(int id)
        {
            var res = StudentBusiness.GetTotalPercentageObtained(id);
            if (res == null) return BadRequest();
            return res;
        }

        [HttpGet("Marksheets/GetStudentList")]
        public ActionResult<List<StudentUltimateModel>> GetStudentList()
        {
            return StudentBusiness.GetStudentList();
        }


        [HttpPost("AddMarks")]
        public ActionResult AddMarks(Marksheet m)
        {
            bool res = StudentBusiness.AddMarks(m);
            if (!res) return BadRequest();

            return CreatedAtAction("AddMarks", m);
        }

        [HttpPut("UpdateMarks")]
        public ActionResult UpdateMarks(Marksheet m)
        {
            bool res = StudentBusiness.UpdateMarks(m);
            if (!res) return NoContent();

            return Ok("Content Update");
        }
    }
}
