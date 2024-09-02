namespace CrudStudentsApi.Models
{
    public class StudentDetail
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
       
        public string? Email { get; set; }
     
        public string? Phone { get; set; }
      
        public DateTime? StudentCreatedOn { get; set; }
     
    }
}
