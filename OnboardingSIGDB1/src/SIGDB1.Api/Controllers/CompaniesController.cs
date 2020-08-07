using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SIGDB1.Application.Dtos;
using SIGDB1.Application.Services;
using System.Threading.Tasks;
using SIGDB1.Core.Extensions;
using System.Linq;

namespace SIGDB1.Api.Controllers
{
    [Route("api/empresas")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var result = await _companyService.Filter(new FilterCompanyDto());

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpPost("pesquisar")]
        public async Task<IActionResult> Get(FilterCompanyDto filter)
        {
            var result = await _companyService.Filter(filter);

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyService.Filter(new FilterCompanyDto { Id = id });

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCompanyDto companyDto)
        {
            var result = await _companyService.Create(companyDto);

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCompanyDto companyDto)
        {
            var result = await _companyService.Update(id, companyDto);

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.Delete(id);

            if (!_companyService.Success())
                return BadRequest(_companyService.Errors);

            else if (result)
                return NoContent();

            return NotFound();
        }
    }
}
