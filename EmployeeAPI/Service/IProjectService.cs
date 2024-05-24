using EmployeeAPI.Entities;

namespace EmployeeAPI.Service
{
    public interface IProjectService
    {
        Task<List<object>> GetProjects();
        Task<List<object>> GetProjectById(int id);
        Task<Project> UpdateProject(Project updatedProj);
        Task AddProject(Project addProj);
        Task<Project> RemoveProject(int id);
        Task<List<object>> SearchProjects(string text);
    }
}
