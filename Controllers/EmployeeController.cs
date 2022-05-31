using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using MusicShopBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllEmployeesAsync([FromQuery] EmployeeParameters parameters)
        {
            try
            {
                var employeeDtos = await _employeeService.GetAllEmployeesAsync(parameters);

                return Ok(employeeDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employeeDto = await _employeeService.GetEmployeeByIdAysnc(employeeId);

                return Ok(employeeDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateEmployeeAsync([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.CreateEmployeeAsync(employeeDto);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{employeeId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeAsync(int employeeId, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newEmployee = await _employeeService.UpdateEmployeeAsync(employeeId, employeeDto);
                    return Ok(newEmployee);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }



    }
}
