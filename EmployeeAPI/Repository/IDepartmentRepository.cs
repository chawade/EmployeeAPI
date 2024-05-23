using EmployeeAPI.Entities;

namespace EmployeeAPI.Repository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task<Department> UpdateDepartment(Department updatedDept);
        Task AddDepartment(Department addDept);
        List<Department> RemoveDepartment(int id);
        Task<List<object>> SearchDepartments(string? text);
    }
}
