using EmployeeAPI.Data;
using EmployeeAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        //Get All Project
        public async Task<List<object>> GetProjects()
        {
            var data = await (from proj in _context.Projects
                              join dept in _context.Departments on proj.DepartmentID equals dept.DepartmentID
                              select new
                              {
                                  proj.ProjectID,
                                  proj.ProjectName,
                                  Department = dept.DepartmentName,
                                  StartDate = proj.StartDate != null ? proj.StartDate.Value.ToString("yyyy-MM-dd HH:mm") : null,
                                  EndDate = proj.EndDate != null ? proj.EndDate.Value.ToString("yyyy-MM-dd HH:mm") : null
                              }).ToListAsync<object>();
            return data;
        }


        //get by id
        public async Task<List<object>> GetProjectById(int id)
        {
            var dbProj = await _context.Projects.FindAsync(id);
            if (dbProj == null) return null;

            var data = await (from proj in _context.Projects
                              join dept in _context.Departments on proj.DepartmentID equals dept.DepartmentID
                              where proj.ProjectID == id
                              select new
                              {
                                  proj.ProjectID,
                                  proj.ProjectName,
                                  Department = dept.DepartmentName,
                                  dept.DepartmentID,
                                  StartDate = proj.StartDate != null ? proj.StartDate : null,
                                  EndDate = proj.EndDate != null ? proj.EndDate : null
                              }).ToListAsync<object>();

            return data;
        }
        //Update
        public async Task<Project> UpdateProject(Project updatedProj)
        {
            var dbProj = await _context.Projects.FindAsync(updatedProj.ProjectID);

            dbProj.ProjectName = updatedProj.ProjectName;
            dbProj.DepartmentID = updatedProj.DepartmentID;
            dbProj.StartDate = updatedProj.StartDate;
            dbProj.EndDate = updatedProj.EndDate;

            _context.Entry(dbProj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return dbProj;
        }

        //New
        public async Task AddProject(Project addProj)
        {
            _context.Projects.Add(addProj);
            await _context.SaveChangesAsync();
        }

        //Delete
        public async Task<Project> RemoveProject(int id)
        {
            var dbProj = await _context.Projects.FindAsync(id);

            if (dbProj == null) return null;

            _context.Projects.Remove(dbProj);
            _context.Entry(dbProj).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return dbProj;
        }

        //Search
        public async Task<List<object>> SearchProjects(string? text)
        {
            var data = await (from proj in _context.Projects
                              join dept in _context.Departments on proj.DepartmentID equals dept.DepartmentID
                              where string.IsNullOrEmpty(text)
                                    || proj.ProjectName.Contains(text)
                                    || dept.DepartmentName.Contains(text)
                              select new
                              {
                                  proj.ProjectName,
                                  Department = dept.DepartmentName,
                                  StartDate = proj.StartDate != null ? proj.StartDate : null,
                                  EndDate = proj.EndDate != null ? proj.EndDate : null
                              }).ToListAsync<object>();
            return data;
        }
    }
}
