namespace EmployeeAPI.Entities
{
    public class Project
    {
        public  int ProjectID { get; set; }
        public required string ProjectName { get; set; }
        public int DepartmentID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
