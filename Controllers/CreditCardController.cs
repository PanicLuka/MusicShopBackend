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
    [Route("api/creditCards")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<CreditCardDto>>> GetAllCreditCardsAsync()
        {
            var creditCardDtos = await _creditCardService.GetAllCreditCardsAsync();

            return Ok(creditCardDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{creditCardId}")]
        public async Task<ActionResult<CreditCardDto>> GetCreditCardByIdAsync(int creditCardId)
        {
            var creditCardDto = await _creditCardService.GetCreditCardByIdAysnc(creditCardId);

            return Ok(creditCardDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCreditCardAsync([FromBody] CreditCardDto creditCardDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _creditCardService.CreateCreditCardAsync(creditCardDto);
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

        [HttpPut("{creditCardId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreditCardDto>> UpdateCreditCardAsync(int creditCardId, [FromBody] CreditCardDto creditCardDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCreditCard = await _creditCardService.UpdateCreditCardAsync(creditCardId, creditCardDto);
                    return Ok(newCreditCard);
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
        [HttpDelete("{creditCardId}")]
        public async Task<IActionResult> DeleteCreditCardAsync(int creditCardId)
        {
            try
            {
                await _creditCardService.DeleteCreditCardAsync(creditCardId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

    }
}
