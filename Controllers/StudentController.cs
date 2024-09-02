using CrudStudentsApi.Data;
using CrudStudentsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CrudStudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public StudentController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GetAllStudents
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = new ApiResponse<List<Student>>();

            try
            {
                var students = await _context.Students.FromSqlRaw("EXEC sp_student @opt = 1").ToListAsync();

                response.Success = true;
                response.Message = "Lista de estudiantes obtenida con éxito.";
                response.Data = students;

                return Ok(response);
            }
            catch (SqlException ex)
            {
                response.Success = false;
                response.Message = $"Error en la base de datos: {ex.Message}";
                return StatusCode(500, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error interno del servidor: {ex.Message}";
                return StatusCode(500, response);
            }
        }
    }
}
