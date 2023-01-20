namespace CrudEmployee.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
            
    }
}
