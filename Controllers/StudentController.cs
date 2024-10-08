﻿using CrudStudentsApi.Data;
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
                response.Message = "Información obtenida con éxito";
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


        //CreateStudent
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {

            var response = new ApiResponse<Student>();

            //Validations
            if (student == null || string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName) || string.IsNullOrWhiteSpace(student.Email))
            {
                response.Success = false;
                response.Message = "Datos requeridos faltantes o incompletos para crear el estudiante.";
                return BadRequest(response);
            }


            var parameters = new[]
         {
            new SqlParameter("@opt", 3),
            new SqlParameter("@student_id", DBNull.Value),
            new SqlParameter("@first_name", student.FirstName),
            new SqlParameter("@middle_name", student.MiddleName),
            new SqlParameter("@last_name", student.LastName),
            new SqlParameter("@gender", student.Gender),
            new SqlParameter("@address_line", student.AddressLine),
            new SqlParameter("@city", student.City),
            new SqlParameter("@zip_postcode", student.ZipPostcode),
            new SqlParameter("@state", student.State),
            new SqlParameter("@email", student.Email),
            new SqlParameter("@email_type", student.EmailType),
            new SqlParameter("@phone", student.Phone),
            new SqlParameter("@phone_type", student.PhoneType),
            new SqlParameter("@country_code", student.CountryCode),
            new SqlParameter("@area_code", student.AreaCode)
        };

            try
            {

                await _context.Database.ExecuteSqlRawAsync("EXEC sp_student @opt, @student_id, @first_name, @middle_name, @last_name, @gender, @address_line, @city, @zip_postcode, @state, @email, @email_type, @phone, @phone_type, @country_code, @area_code", parameters);

                response.Success = true;
                response.Message = "Estudiante creado con éxito";
                response.Data = student;

                var studentId = student.StudentId.ToString();

                return Created(studentId, response);
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


        //UpdateStudentData
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {

            var response = new ApiResponse<Student>();

            //Validations
            if (id == 0)
            {
                response.Success = false;
                response.Message = "El ID del estudiante es necesario para esta operación.";
                return BadRequest(response);
            }

            var parameters = new[]
            {
                new SqlParameter("@opt", 4),
                new SqlParameter("@student_id", id),
                new SqlParameter("@first_name", (object)student.FirstName ?? DBNull.Value),
                new SqlParameter("@middle_name", (object)student.MiddleName ?? DBNull.Value),
                new SqlParameter("@last_name", (object)student.LastName ?? DBNull.Value),
                new SqlParameter("@gender", (object)student.Gender ?? DBNull.Value),
                new SqlParameter("@address_line", (object)student.AddressLine ?? DBNull.Value),
                new SqlParameter("@city", (object)student.City ?? DBNull.Value),
                new SqlParameter("@zip_postcode", (object)student.ZipPostcode ?? DBNull.Value),
                new SqlParameter("@state", (object)student.State ?? DBNull.Value),
                new SqlParameter("@email", (object)student.Email ?? DBNull.Value),
                new SqlParameter("@email_type", (object)student.EmailType ?? DBNull.Value),
                new SqlParameter("@phone", (object)student.Phone ?? DBNull.Value),
                new SqlParameter("@phone_type", (object)student.PhoneType ?? DBNull.Value),
                new SqlParameter("@country_code", (object)student.CountryCode ?? DBNull.Value),
                new SqlParameter("@area_code", (object)student.AreaCode ?? DBNull.Value)
                    };

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_student @opt, @student_id, @first_name, @middle_name, @last_name, @gender, @address_line, @city, @zip_postcode, @state, @email, @email_type, @phone, @phone_type, @country_code, @area_code", parameters);

                response.Success = true;
                response.Message = "Estudiante actualizado con éxito";
                response.Data = student;

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

        //DeleteStudent
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            var response = new ApiResponse<int>();

            //Validations
            if (id == 0)
            {
                response.Success = false;
                response.Message = "El ID del estudiante es necesario para esta operación.";
                return BadRequest(response);
            }


            var parameter = new SqlParameter("@opt", 5);
            var studentIdParam = new SqlParameter("@student_id", id);

            try
            {

                await _context.Database.ExecuteSqlRawAsync("EXEC sp_student @opt, @student_id", parameter, studentIdParam);

                response.Success = true;
                response.Message = "Estudiante eliminado con éxito";
                response.Data = id;

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
