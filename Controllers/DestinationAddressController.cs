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
    [Route("api/destinations")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DestinationAddressController : ControllerBase
    {
        private readonly IDestinationAddressService _destinationAddressService;

        public DestinationAddressController(IDestinationAddressService destinationAddressService)
        {
            _destinationAddressService = destinationAddressService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<DestinationAddressDto>>> GetAllDestinationAddressesAsync([FromQuery] DestinationAddressParameters parameters)
        {
            try
            {
                var destinationAddressDtos = await _destinationAddressService.GetAllDestinationAddressesAsync(parameters);

                return Ok(destinationAddressDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{destinationAddressId}")]
        public async Task<ActionResult<DestinationAddressDto>> GetDestinationAddressByIdAsync(int destinationAddressId)
        {
            try
            {
                var destinationAddressDto = await _destinationAddressService.GetDestinationAddressByIdAysnc(destinationAddressId);

                return Ok(destinationAddressDto);
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
        public async Task<ActionResult> CreateDestinationAddressAsync([FromBody] DestinationAddressDto destinationAddressDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _destinationAddressService.CreateDestinationAddressAsync(destinationAddressDto);
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

        [HttpPut("{destinationAddressId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DestinationAddressDto>> UpdateDestinationAddressAsync(int destinationAddressId, [FromBody] DestinationAddressDto destinationAddressDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newDestinationAddress = await _destinationAddressService.UpdateDestinationAddressAsync(destinationAddressId, destinationAddressDto);
                    return Ok(newDestinationAddress);
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
        [HttpDelete("{destinationAddressId}")]
        public async Task<IActionResult> DeleteDestinationAddressAsync(int destinationAddressId)
        {
            try
            {
                await _destinationAddressService.DeleteDestinationAddressAsync(destinationAddressId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }
    }
}
