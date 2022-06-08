using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using MusicShopBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopBackend.Controllers
{

    [ApiController]
    [Route("api/products")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("count")]
        public ActionResult<int> GetProductsCount()
        {
            try
            {
                var count = _productService.GetProductsCount();

                return Ok(count);
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ProductDto>>> GetAllProductsAsync([FromQuery] ProductParameters parameters)
        {
            try
            {
                var productDtos = await _productService.GetAllProductsAsync(parameters);

                return Ok(productDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("filter/{category}")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProductsByCategory(string category)
        {
            try
            {
                var productDtos = await _productService.GetAllProductsByCategory(category);

                return Ok(productDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int productId)
        {
            try
            {
                var productDto = await _productService.GetProductByIdAysnc(productId);

                return Ok(productDto);
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
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreateProductAsync(productDto);
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

        [HttpPut("{productId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> UpdateProductAsync(int productId, [FromBody] ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newProduct = await _productService.UpdateProductAsync(productId, productDto);
                    return Ok(newProduct);
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
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            try
            {
                await _productService.DeleteProductAsync(productId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);

            }
        }


    }

}
