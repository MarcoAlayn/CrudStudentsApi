namespace CrudStudentsApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? ZipPostcode { get; set; }
        public string? State { get; set; }
        public string? Email { get; set; }
        public string? EmailType { get; set; }
        public string? Phone { get; set; }
        public string? PhoneType { get; set; }
        public string? CountryCode { get; set; }
        public string? AreaCode { get; set; }
        public DateTime? StudentCreatedOn { get; set; }
        public DateTime? StudentUpdatedOn { get; set; }
    }
}
