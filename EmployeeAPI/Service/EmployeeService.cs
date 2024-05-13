using EmployeeAPI.Entities;
using EmployeeAPI.Repository;

namespace EmployeeAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<object>> GetEmployees()
        {
            var result = await _employeeRepository.GetEmployees();
            return result;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmp)
        {
            var result = await _employeeRepository.UpdateEmployee(updatedEmp);
            return result;
        }

        public async Task AddEmployee(Employee addEmp)
        {
            await _employeeRepository.AddEmployee(addEmp);
        }

        public List<Employee> RemoveEmployee(int id)
        {
            var result = _employeeRepository.RemoveEmployee(id);
            return result;
        }

        public async Task<List<object>> SearchEmployees(string text)
        {
            var result = await _employeeRepository.SearchEmployees(text);
            return result;
        }
    }
}
