using EmployeeAPI.Entities;

namespace EmployeeAPI.Service
{
    public interface IEmployeeService
    {
        Task<List<object>> GetEmployees();
        Task<List<object>> GetEmployeeById(int id);
        Task<Employee> UpdateEmployee(Employee updatedEmp);
        Task AddEmployee(Employee addEmp);
        Task<Employee> RemoveEmployee(int id);
        Task<List<object>> SearchEmployees(string text);
    }
}
