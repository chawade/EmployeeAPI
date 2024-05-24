using EmployeeAPI.Entities;
using EmployeeAPI.Repository;

namespace EmployeeAPI.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departRepository;
        public DepartmentService(IDepartmentRepository departRepository)
        {
            _departRepository = departRepository;
        }

        public async Task<List<Department>> GetDepartments()
        {
            var result = await _departRepository.GetDepartments();
            return result;
        }

        public async Task<List<object>> GetDepartmentById(int id)
        {
            var result = await _departRepository.GetDepartmentById(id);
            return result;
        }

        public async Task<Department> UpdateDepartment(Department updatedDept)
        {
            var result = await _departRepository.UpdateDepartment(updatedDept);
            return result;
        }

        public async Task AddDepartment(Department addDept)
        {
            await _departRepository.AddDepartment(addDept);
        }

        public async Task<Department> RemoveDepartment(int id)
        {
            var result = await _departRepository.RemoveDepartment(id);
            return result;
        }

        public async Task<List<object>> SearchDepartments(string text)
        {
            var result = await _departRepository.SearchDepartments(text);
            return result;
        }
    }
}
