using EmployeeAPI.Entities;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GetProjects")]
        public async Task<ActionResult<List<object>>> GetProjects()
        {
            var project = await _projectService.GetProjects();
            return Ok(project);
        }

        [HttpPut("UpdateProjects")]
        public async Task<ActionResult<Project>> UpdateProject(Project updatedProj)
        {
            if (string.IsNullOrEmpty(updatedProj.ProjectName))
            {
                return BadRequest("Please Enter ProjectName.");
            }

            if (updatedProj.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID (1.IT 2.HR 3.Marketing)");
            }
            await _projectService.UpdateProject(updatedProj);
            return Ok(updatedProj);
        }

        [HttpPost("AddNewProject")]
        public async Task<ActionResult> AddProject(Project addProj)
        {
            if (string.IsNullOrEmpty(addProj.ProjectName))
            {
                return BadRequest("Please Enter ProjectName.");
            }

            if (addProj.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID (1.IT 2.HR 3.Marketing)");
            }
            await _projectService.AddProject(addProj);
            return Ok(addProj);
        }

        [HttpDelete("DeleteProject")]
        public ActionResult<List<Project>> DeleteProject(int id)
        {
            var result = _projectService.RemoveProject(id);
            return result is null ? NotFound("Project Not Found.") : Ok("Successfully!!");
        }

        [HttpGet("SearchProjects")]
        public async Task<ActionResult<List<object>>> SearchProjects(string? text)
        {
            var result = await _projectService.SearchProjects(text);
            return result.Count > 0 ? Ok(result) : NotFound("Project Not Found.");
        }
    }
}
