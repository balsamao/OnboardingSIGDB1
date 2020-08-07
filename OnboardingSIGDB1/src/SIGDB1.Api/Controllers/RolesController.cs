using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIGDB1.Application.Dtos;
using SIGDB1.Application.Services;
using SIGDB1.Core.Extensions;

namespace SIGDB1.Api.Controllers
{
    [Route("api/cargos")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.Filter(new FilterRoleDto());

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpPost("pesquisar")]
        public async Task<IActionResult> Get(FilterRoleDto filter)
        {
            var result = await _roleService.Filter(filter);

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _roleService.Filter(new FilterRoleDto { Id = id });

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            if (result.IsEmpty())
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleDto roleDto)
        {
            var result = await _roleService.Create(roleDto);

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateRoleDto roleDto)
        {
            var result = await _roleService.Update(id, roleDto);

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.Delete(id);

            if (!_roleService.Success())
                return BadRequest(_roleService.Errors);

            else if (result)
                return NoContent();

            return NotFound();
        }
    }
}
