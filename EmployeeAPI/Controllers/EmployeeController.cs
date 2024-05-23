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

        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<object>>> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult<List<object>>>  GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee(Employee updatedEmp)
        {
            if (updatedEmp == null)
            {
                return BadRequest("Invalid employee data.");
            }

            if (string.IsNullOrEmpty(updatedEmp.FirstName))
            {
                return BadRequest("Please enter a valid Firstname.");
            }

            if (updatedEmp.DepartmentID == 0)
            {
                return BadRequest("Please enter a valid DepartmentID (1.IT, 2.HR, 3.Marketing).");
            }

            try
            {
                await _employeeService.UpdateEmployee(updatedEmp);
                return Ok("Employee updated successfully!!");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the employee.");
            }
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
        public async Task<ActionResult<List<Employee>>> RemoveEmployee(int id)
        {
            try
            {
            var result = await _employeeService.RemoveEmployee(id);
            return result is null ? NotFound("Employee Not Found.") : Ok("Successfully!!");
            } catch (Exception ex)
            {
                return Ok("Don't find this id");
            }
        }

        [HttpGet("SearchEmployee")]
        public async Task<ActionResult<List<object>>> SearchEmployees(string? text)
        {
            var result = await _employeeService.SearchEmployees(text);
            return result.Count > 0 ? Ok(result) : NotFound("Employee Not Found.");
        }
    }

}