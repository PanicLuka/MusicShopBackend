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
    [Route("api/brands")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<BrandDto>>> GetAllBrandsAsync([FromQuery] BrandParameters parameters)
        {
            try
            {
                var brandDtos = await _brandService.GetAllBrandsAsync(parameters);

                return Ok(brandDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandDto>> GetBrandByIdAsync(int brandId)
        {
            try
            {
                var brandDto = await _brandService.GetBrandByIdAysnc(brandId);

                return Ok(brandDto);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBrandAsync([FromBody] BrandDto brandDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    
                    await _brandService.CreateBrandAsync(brandDto);
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

        [HttpPut("{brandId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandDto>> UpdateBrandAsync(int brandId, [FromBody] BrandDto brandDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var newBrand = await _brandService.UpdateBrandAsync(brandId, brandDto);
                    return Ok(newBrand);
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
        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrandAsync(int brandId)
        {
            try
            {
                await _brandService.DeleteBrandAsync(brandId);
                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }

    }
}
