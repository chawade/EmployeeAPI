namespace EmployeeAPI.Entities
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public required string DepartmentName { get; set; }
        public int ManagerID { get; set; }
    }
}
