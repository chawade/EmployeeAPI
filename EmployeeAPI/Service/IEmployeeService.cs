using EmployeeAPI.Entities;

namespace EmployeeAPI.Service
{
    public interface IEmployeeService
    {
        Task<List<object>> GetEmployees();
        Task<Employee> UpdateEmployee(Employee updatedEmp);
        Task AddEmployee(Employee addEmp);
        List<Employee> RemoveEmployee(int id);
        Task<List<object>> SearchEmployees(string text);
    }
}
