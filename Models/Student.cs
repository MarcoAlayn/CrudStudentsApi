namespace CrudStudentsApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? StudentCreatedOn { get; set; }
        public DateTime? StudentUpdatedOn { get; set; }
    }
}
