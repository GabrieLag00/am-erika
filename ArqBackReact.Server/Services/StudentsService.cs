using ArqBackReact.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArqBackReact.Server.Services
{
    //public async Task<byte[]> GetPDF()
    //{
        //ObjectDataSource source = new ObjectDataSource();

        //var report = new AplicationCore.PDF.StudentsPDF();

      //  var estudiantes
    //}
    public class StudentService : IStudentService
    {
        private readonly NetdbContext _context;

        public StudentService(NetdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            var studentbd = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            if (studentbd == null) return false; // Si no se encuentra el estudiante

            studentbd.Nombre = student.Nombre;
            studentbd.Edad = student.Edad;
            studentbd.Correo = student.Correo;

            _context.Entry(studentbd).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true; // Indicar que fue exitoso
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false; // Indicar que no se encontró
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true; // Indicar que fue exitoso
        }
    }
}
