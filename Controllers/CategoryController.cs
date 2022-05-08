using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Models;
using MusicShopBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var categoryDtos = await _categoryService.GetAllCategoriesAsync();

            return Ok(categoryDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int categoryId)
        {
            var categoryDto = await _categoryService.GetCategoryByIdAysnc(categoryId);

            return Ok(categoryDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryService.CreateCategoryAsync(categoryDto);
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

        [HttpPut("{categoryId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = await _categoryService.UpdateCategoryAsync(categoryId, categoryDto);
                    return Ok(newCategory);
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
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
    }
}
