using EmployeeAPI.Entities;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartments")]
        public async Task<ActionResult<List<object>>> GetDepartments()
        {
            var result = await _departmentService.GetDepartments();
            return Ok(result);
        }

        [HttpPut("Update Dept")]
        public async Task<ActionResult<Department>> UpdateDepartment(Department updatedDept)
        {
            if (string.IsNullOrEmpty(updatedDept.DepartmentName))
            {
                return BadRequest("Please Enter DepartmentName.");
            }

            var result = await _departmentService.UpdateDepartment(updatedDept);
            return Ok("Successfully!!");
        }

        [HttpPost("Add Dept")]
        public async Task<ActionResult> AddDepartment(Department addDept)
        {
            if (string.IsNullOrEmpty(addDept.DepartmentName))
            {
                return BadRequest("Please Enter DepartmentName.");
            }

            await _departmentService.AddDepartment(addDept);
            return Ok("Successfully!!");
        }
        [HttpDelete("Delete Dept")]
        public ActionResult<List<Department>> RemoveDepartment(int id)
        {
            var result = _departmentService.RemoveDepartment(id);
            return result is null ? NotFound("Employee Not Found.") : Ok("Successfully!!");
        }

        [HttpGet("Search Dept")]
        public async Task<ActionResult<List<object>>> SearchDepartments(string? text)
        {
            var result = await _departmentService.SearchDepartments(text);
            return result.Count > 0 ? Ok(result) : NotFound("Department Not Found.");
        }
    }
}
