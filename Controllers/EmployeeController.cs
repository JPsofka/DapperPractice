using CrudEmployee.Data.Repository;
using CrudEmployee.Entity;
using Microsoft.AspNetCore.Mvc;

namespace CrudEmployee.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeRepository.GetAll());
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                var createdEmployee = await _employeeRepository.Create(employee);
                return CreatedAtRoute("EmployeeById", new { id = createdEmployee.Id }, createdEmployee);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            try
            {
                var employeeDb = await _employeeRepository.Get(id);
                if (employeeDb == null)
                    return NotFound();
                await _employeeRepository.Update(id, employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dbEmployee = await _employeeRepository.Get(id);
                if (dbEmployee == null)
                    return NotFound();
                await _employeeRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


    }
}
