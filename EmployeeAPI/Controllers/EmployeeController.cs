using EmployeeAPI.Entities;
using EmployeeAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //GetAllEmployee
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<dynamic>>> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee(Employee updatedEmp)
        {
            if (string.IsNullOrEmpty(updatedEmp.FirstName))
            {
                return BadRequest("Please Enter Firstname.");
            }

            if (updatedEmp.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID (1.IT 2.HR 3.Marketing)");
            }

            await _employeeService.UpdateEmployee(updatedEmp);
            return Ok("Successfully!!");
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult<List<object>>> AddEmployee(Employee addEmp)
        {
            if (string.IsNullOrEmpty(addEmp.FirstName))
            {
                return BadRequest("Please Enter Firstname.");
            }

            if (addEmp.DepartmentID == 0)
            {
                return BadRequest("Please Enter DepartmentID (1.IT 2.HR 3.Marketing)");
            }

            await _employeeService.AddEmployee(addEmp);
            return Ok("Successfully!!");
        }

        [HttpDelete("DeleteEmployee")]
        public ActionResult<List<Employee>> RemoveEmployee(int id)
        {

            var result = _employeeService.RemoveEmployee(id);
            return result is null ? NotFound("Employee Not Found.") : Ok("Successfully!!");
        }

        [HttpGet("SearchEmployee")]
        public async Task<ActionResult<List<object>>> SearchEmployees(string? text)
        {
            var result = await _employeeService.SearchEmployees(text);
            return result.Count > 0 ? Ok(result) : NotFound("Employee Not Found.");
        }
    }

}

