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
                                  emp.EmployeeID,
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
            var dbDept = await _context.Employees.FindAsync(id);
            if (dbDept == null) return null;
            var data = await (from emp in _context.Employees
                              join dept in _context.Departments on emp.DepartmentID equals dept.DepartmentID
                              join proj in _context.Projects on dept.DepartmentID equals proj.DepartmentID into projects
                              where emp.EmployeeID == id
                              select new
                              {
                                  emp.FirstName,
                                  emp.LastName,
                                  emp.Email,
                                  emp.Gender,
                                  emp.JobTitle,
                                  emp.DepartmentID,
                                  dept.DepartmentName,
                                  Projects = projects.Select(p => p.ProjectName).ToList()
                              }).ToListAsync<object>();

            return data;
        }

        //Update
        public async Task<Employee> UpdateEmployee(Employee updatedEmp)
        {
            var dbEmp = await _context.Employees.FindAsync(updatedEmp.EmployeeID);

            if (dbEmp == null)
            {
                // หากไม่พบข้อมูลพนักงานที่ต้องการอัปเดต
                throw new ArgumentException("Employee not found.", nameof(updatedEmp.EmployeeID));
            }

            // อัปเดตข้อมูลพนักงาน
            dbEmp.FirstName = updatedEmp.FirstName;
            dbEmp.LastName = updatedEmp.LastName;
            dbEmp.Email = updatedEmp.Email;
            dbEmp.Gender = updatedEmp.Gender;
            dbEmp.DepartmentID = updatedEmp.DepartmentID;
            dbEmp.JobTitle = updatedEmp.JobTitle;

            // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
            await _context.SaveChangesAsync();

            // ส่งค่าพนักงานที่ถูกอัปเดตกลับ
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
            var dbProj = await _context.Employees.FindAsync(id);
            if (dbProj == null) return null;
            _context.Employees.Remove(dbProj);
            _context.Entry(dbProj).State = EntityState.Deleted;
            await _context.SaveChangesAsync(); 
            return dbProj;
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
