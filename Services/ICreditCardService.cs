using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface ICreditCardService
    {
        Task CreateCreditCardAsync(CreditCardDto creditCardDto);

        Task<List<CreditCardDto>> GetAllCreditCardsAsync();

        Task<CreditCardDto> GetCreditCardByIdAysnc(int creditCardId);

        Task<CreditCardDto> UpdateCreditCardAsync(int creditCardId, CreditCardDto creditCardDto);

        Task DeleteCreditCardAsync(int creditCardId);
    }
}
