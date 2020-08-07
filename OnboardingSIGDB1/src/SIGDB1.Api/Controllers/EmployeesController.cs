using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGDB1.Application.Dtos;
using SIGDB1.Application.Services;
using SIGDB1.Core.Extensions;

namespace SIGDB1.Api.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.Filter(new FilterEmployeeDto());

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpPost("pesquisar")]
        public async Task<IActionResult> Get(FilterEmployeeDto filter)
        {
            var result = await _employeeService.Filter(filter);

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeeService.Filter(new FilterEmployeeDto { Id = id });

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto emplyeeDto)
        {
            var result = await _employeeService.Create(emplyeeDto);

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateEmployeeDto emplyeeDto)
        {
            var result = await _employeeService.Update(id, emplyeeDto);

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.Delete(id);

            if (!_employeeService.Success())
                return BadRequest(_employeeService.Errors);

            else if (result)
                return NoContent();

            return NotFound();
        }
    }
}
