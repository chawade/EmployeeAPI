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
                                  proj.ProjectName,
                                  Department = dept.DepartmentName,
                                  StartDate = proj.StartDate != null ? proj.StartDate.Value.ToString("yyyy-MM-dd HH:mm") : null,
                                  EndDate = proj.EndDate != null ? proj.EndDate.Value.ToString("yyyy-MM-dd HH:mm") : null
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
        public List<Project> RemoveProject(int id)
        {
            var addProj = _context.Projects.Find(id);

            if (addProj == null) return null;

            _context.Projects.Remove(addProj);
            _context.Entry(addProj).State = EntityState.Deleted;
            _context.SaveChangesAsync();
            return _context.Projects.ToList();
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
                                  StartDate = proj.StartDate != null ? proj.StartDate.Value.ToString("yyyy-MM-dd HH:mm") : null,
                                  EndDate = proj.EndDate != null ? proj.EndDate.Value.ToString("yyyy-MM-dd HH:mm") : null
                              }).ToListAsync<object>();
            return data;
        }
    }
}
