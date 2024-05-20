using EmployeeAPI.Entities;

namespace EmployeeAPI.Repository
{
    public interface IProjectRepository
    {
        Task<List<object>> GetProjects();
        Task<Project> UpdateProject(Project updatedProj);
        Task AddProject(Project addProj);
        List<Project> RemoveProject(int id);
        Task<List<object>> SearchProjects(string? text);
    }
}
