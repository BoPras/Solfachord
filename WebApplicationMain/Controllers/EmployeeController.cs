using Microsoft.AspNetCore.Mvc;
using WebApplicationMain.Models;
using WebApplicationMain.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationMain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _services;

        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }



        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _services.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // GET api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                
               var employee = await _services.GetById(id);
               return Ok(employee);
                
            }
            catch (Exception ex)
            {
               return StatusCode(500, ex.Message);

            }
        }

        // POST api/Employee
        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            try
            {
                await _services.CreateAsync(employee);
                return Ok("Created Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Employee employee)
        {
            try
            {
                await _services.UpdateAsync(id, employee);
                return Ok("Updated Succesfullly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _services.DeleteAsync(id);
                return Ok("Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                
            }
        }
    }
}
