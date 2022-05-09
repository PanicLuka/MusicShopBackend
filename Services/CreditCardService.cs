using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using MusicShopBackend.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicShopBackend.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly DataContext _context;
        private readonly CreditCardValidator _validator;
        private static int _count;

        public CreditCardService(DataContext context, CreditCardValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int GetCreditCardsCount()
        {
            return _count;
        }

        public async Task CreateCreditCardAsync(CreditCardDto creditCardDto)
        {
            _validator.ValidateAndThrow(creditCardDto);

            CreditCard creditCardEntity = creditCardDto.CreditCardDtoToCreditCard();

            await _context.AddAsync(creditCardEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteCreditCardAsync(int CreditCardId)
        {
            var CreditCard = await GetCreditCardByIdHelperAsync(CreditCardId);

            if (CreditCard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(CreditCard);
            await SaveChangesAsync();
        }

        public async Task<PagedList<CreditCardDto>> GetAllCreditCardsAsync(CreditCardParameters parameters)
        {
            var creditCards = await _context.CreditCards.ToListAsync();

            _count = creditCards.Count;

            if (creditCards == null || creditCards.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<CreditCardDto> creditCardDtos = new List<CreditCardDto>();

            foreach (var creditCard in creditCards)
            {
                CreditCardDto creditCardDto = creditCard.CreditCardToDto();
                creditCardDtos.Add(creditCardDto);
            }

            IQueryable<CreditCardDto> queryable = creditCardDtos.AsQueryable();

            return PagedList<CreditCardDto>.ToPagedList(queryable, parameters._pageNumber, parameters.PageSize);

        }

        public async Task<CreditCardDto> GetCreditCardByIdAysnc(int creditCardId)
        {
            var creditCard = await _context.CreditCards.FirstOrDefaultAsync(e => e.CreditCardId == creditCardId);
            if (creditCard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var creditCardDto = creditCard.CreditCardToDto();
            return creditCardDto;
        }

        public async Task<CreditCardDto> UpdateCreditCardAsync(int creditCardId, CreditCardDto creditCardDto)
        {
            var oldCreditCardDto = await GetCreditCardByIdHelperAsync(creditCardId);
            if (oldCreditCardDto == null)
            {
                await CreateCreditCardAsync(creditCardDto);
                return oldCreditCardDto.CreditCardToDto();
            }
            else
            {
                CreditCard creditCard = creditCardDto.CreditCardDtoToCreditCard();
                oldCreditCardDto.CreditCardNumber = creditCard.CreditCardNumber;
                oldCreditCardDto.Cvv = creditCard.Cvv;
                oldCreditCardDto.ExpireDate = creditCard.ExpireDate;

                await SaveChangesAsync();
                if (oldCreditCardDto.CreditCardToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldCreditCardDto.CreditCardToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<CreditCard> GetCreditCardByIdHelperAsync(int creditCardId)
        {
            var creditCard = await _context.CreditCards.FirstOrDefaultAsync(e => e.CreditCardId == creditCardId);

            if (creditCard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return creditCard;
        }
    }
}
