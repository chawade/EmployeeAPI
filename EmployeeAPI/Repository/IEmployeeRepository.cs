using EmployeeAPI.Entities;

namespace EmployeeAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<object>> GetEmployees();
        Task<List<object>> GetEmployeeById(int id);
        Task<Employee> UpdateEmployee(Employee updatedEmp);
        Task AddEmployee(Employee addEmp);
        Task<Employee> RemoveEmployee(int id);
        Task<List<object>> SearchEmployees(string? text);
    }
}
