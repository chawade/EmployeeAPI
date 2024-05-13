using EmployeeAPI.Entities;

namespace EmployeeAPI.Service
{
    public interface IProjectService
    {
        Task<List<object>> GetProjects();
        Task<Project> UpdateProject(Project updatedProj);
        Task AddProject(Project addProj);
        List<Project> RemoveProject(int id);
        Task<List<object>> SearchProjects(string text);
    }
}
