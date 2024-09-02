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
            var response = new ApiResponse<List<StudentDetail>>();

            try
            {
                var studentList = await _context.Students.FromSqlRaw("EXEC sp_student @opt = 1").ToListAsync();

                var selectedFieldsRequired = studentList.Select(student => new StudentDetail
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    Email = student.Email,
                    Phone = student.Phone,
                    StudentCreatedOn = student.StudentCreatedOn
                }).ToList();

                response.Success = true;
                response.Message = "Lista de estudiantes obtenida con éxito.";
                response.Data = selectedFieldsRequired;

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

        //GetStudentByID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = new ApiResponse<Student>();

            //Validations
            if (id == 0)
            {
                response.Success = false;
                response.Message = "El ID del estudiante es necesario para esta operación.";
                return BadRequest(response);
            }

            try
            {

                var student = await _context.Students.FromSqlRaw("EXEC sp_student @opt = 2, @student_id = {0}", id).ToListAsync();

                if (student == null || !student.Any())
                {
                    return NotFound(new ApiResponse<Student> { Success = false, Message = "Estudiante no encontrado" });
                }

                response.Success = true;
                response.Message = "Estudiante obtenido con éxito";
                response.Data = student.FirstOrDefault();

                return Ok(response);
            }
            catch (SqlException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
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
