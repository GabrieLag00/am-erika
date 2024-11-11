using ArqBackReact.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArqBackReact.Server.Services
{
    public interface IStudentService
    {
        //Task<byte[]> GetPDF();
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id); // El '?' indica que puede ser nulo.
        Task<Student> AddStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(int id, Student student); // Cambiamos el retorno para indicar si hubo éxito.
        Task<bool> DeleteStudentAsync(int id); // Cambiamos el retorno para indicar si hubo éxito.
    }
}
