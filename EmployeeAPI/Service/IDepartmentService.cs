using EmployeeAPI.Entities;

namespace EmployeeAPI.Service
{
    public interface IDepartmentService
    {
        Task<List<object>> GetDepartments();
        Task<Department> UpdateDepartment(Department updatedDept);
        Task AddDepartment(Department addDept);
        List<Department> RemoveDepartment(int id);
        Task<List<object>> SearchDepartments(string text);
    }
}
