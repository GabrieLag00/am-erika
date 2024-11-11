using Microsoft.AspNetCore.Mvc;
using ArqBackReact.Server.Models;
using ArqBackReact.Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArqBackReact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        //private object _service;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentService.GetStudentsAsync();
            return Ok(students);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            var createdStudent = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        // PUT: api/Student/5
        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            // En lugar de comparar el id del cuerpo con el de la URL,
            // directamente establece el id del objeto 'student' como el de la URL
            student.Id = id;

            var success = await _studentService.UpdateStudentAsync(id, student);

            if (!success)
            {
                return NotFound("Student not found");
            }

            return NoContent();
        }


        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudentAsync(id);

            if (!success)
            {
                return NotFound("Student not found");
            }

            return NoContent();
        }

        //[HttpGet("pdf")]
        //public async Task<ActionResult> GetPDF()
        // {
        // var pdfFile = await _service.GetPDF();
        //  return File(pdfFile, "aplication/pdf", "Estudiantes.pdf");
        // }
    }
}
