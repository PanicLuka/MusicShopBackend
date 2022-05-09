using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface ICreditCardService
    {
        Task CreateCreditCardAsync(CreditCardDto creditCardDto);

        Task<PagedList<CreditCardDto>> GetAllCreditCardsAsync(CreditCardParameters parameters);

        Task<CreditCardDto> GetCreditCardByIdAysnc(int creditCardId);

        Task<CreditCardDto> UpdateCreditCardAsync(int creditCardId, CreditCardDto creditCardDto);

        Task DeleteCreditCardAsync(int creditCardId);
    }
}
