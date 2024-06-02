using BusinessLogicLayer;
using BusinessLogicLayer.Dto;
using DataAccessLayer.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Sibers.Controllers
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

        [HttpGet("allEmployee")]
        public async Task<ActionResult<IEnumerable<EmployeeGetDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("employeeForProject")]
        public async Task<ActionResult<IEnumerable<EmployeeGetDtoForProject>>> GetEmployeesForProject()
        {
            var employees = await _employeeService.GetEmployeesForProjectAsync();
            return Ok(employees);
        }

        [HttpGet("projectManager")]
        public async Task<ActionResult<IEnumerable<EmployeeGetDtoForProject>>> GetEmployeesProjectManager()
        {
            var employees = await _employeeService.GetEmployeesProjectManagerAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGetDto>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost("addEmployee")]
        public async Task<ActionResult<EmployeeGetDto>> PostEmployee([FromBody] EmployeePostDto employee)
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("updateEmployee/{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeePostDto employee)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, employee);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("deleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }


    }
}
