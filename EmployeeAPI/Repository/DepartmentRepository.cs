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


        //get by id
        public async Task<List<object>> GetDepartmentById(int id)
        {
            var dbEmp = await _context.Departments.FindAsync(id);
            if (dbEmp == null) return null;

            var data = await (from dept in _context.Departments
                   join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID into projects
                   where dept.DepartmentID == id
                   select new
                   {
                       dept.DepartmentName,
                       dept.ManagerID,
                       Projects = projects.Select(p => p.ProjectName).ToList()
                   }).ToListAsync<object>();

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
        public async Task<Department> RemoveDepartment(int id)
        {
            var dbdept = await _context.Departments.FindAsync(id);
            if (dbdept == null) return null;
            _context.Departments.Remove(dbdept);
            _context.Entry(dbdept).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return dbdept;
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
