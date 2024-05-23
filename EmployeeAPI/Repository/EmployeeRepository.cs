using EmployeeAPI.Data;
using EmployeeAPI.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        //Get All Employee
        public async Task<List<object>> GetEmployees()
        {
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
                              //join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  emp.JobTitle,
                                  dept.DepartmentName,
                                  //proj.ProjectName,
                              }).ToListAsync<object>();

            return data;
        }

        //get by id
        public async Task<List<object>> GetEmployeeById(int id)
        {
            var dbEmp = await _context.Employees.FindAsync(id);
            if (dbEmp == null) return null;
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
            //join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID
                              where emp.EmployeeID == id
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  emp.JobTitle,
                                  dept.DepartmentName,
                                  //proj.ProjectName,
                              }).ToListAsync<object>();

            return data;
        }

        //Update
        public async Task<Employee> UpdateEmployee(Employee updatedEmp)
        {
            var dbEmp = await _context.Employees.FindAsync(updatedEmp.EmployeeID);

            dbEmp.FirstName = updatedEmp.FirstName;
            dbEmp.LastName = updatedEmp.LastName;
            dbEmp.Email = updatedEmp.Email;
            dbEmp.Gender = updatedEmp.Gender;
            dbEmp.DepartmentID = updatedEmp.DepartmentID;
            dbEmp.JobTitle = updatedEmp.JobTitle;

            _context.Entry(dbEmp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return dbEmp;
        }

        //New
        public async Task AddEmployee(Employee addEmp)
        {
            _context.Employees.Add(addEmp);
            await _context.SaveChangesAsync();
        }

        //Delete
        public async Task<Employee> RemoveEmployee(int id)
        {
            var dbEmp = await _context.Employees.FindAsync(id);
            if (dbEmp == null) return null;
            _context.Employees.Remove(dbEmp);
            await _context.SaveChangesAsync();
            return dbEmp;
        }


        //Search
        public async Task<List<object>> SearchEmployees(string? text)
        {
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
                              //join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID into projects
                              where string.IsNullOrEmpty(text)
                                    || emp.FirstName.Contains(text)
                                    || emp.LastName.Contains(text)
                                    || emp.JobTitle.Contains(text)
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  dept.DepartmentName,
                                  emp.JobTitle,
                                  //Projects = projects.Select(p => p.ProjectName).ToList()
                              }).ToListAsync<object>();
            return data;
        }
    }
}
