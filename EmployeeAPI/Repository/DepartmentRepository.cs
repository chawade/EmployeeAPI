using EmployeeAPI.Data;
using EmployeeAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        //Get All Departmant
        public async Task<List<Department>> GetDepartments()
        {
            var data = await _context.Departments.ToListAsync();
            return data;
        }

        //Update
        public async Task<Department> UpdateDepartment(Department updatedDept)
        {
            var dbDept = await _context.Departments.FindAsync(updatedDept.DepartmentID);

            dbDept.DepartmentName = updatedDept.DepartmentName;
            dbDept.ManagerID = updatedDept.ManagerID;

            _context.Entry(dbDept).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return dbDept;
        }

        //New
        public async Task AddDepartment(Department addDept)
        {
            _context.Departments.Add(addDept);
            await _context.SaveChangesAsync();
        }

        //Delete
        public List<Department> RemoveDepartment(int id)
        {
            var dbEmp = _context.Departments.Find(id);
            if (dbEmp == null) return null;
            _context.Departments.Remove(dbEmp);
            _context.Entry(dbEmp).State = EntityState.Deleted;
            _context.SaveChangesAsync();
            return _context.Departments.ToList();
        }

        //Search
        public async Task<List<object>> SearchDepartments(string? text)
        {
            var data = await (from dept in _context.Departments
                              where string.IsNullOrEmpty(text)
                                    || dept.DepartmentName.Contains(text)
                                    || dept.ManagerID.ToString().Contains(text)
                              select new
                              {
                                  dept.DepartmentName,
                                  dept.ManagerID,
                              }).ToListAsync<object>();
            return data;
        }
    }
}
