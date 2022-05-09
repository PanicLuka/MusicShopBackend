using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IDestinationAddressService
    {
        Task CreateDestinationAddressAsync(DestinationAddressDto destinationAddressDto);

        Task<PagedList<DestinationAddressDto>> GetAllDestinationAddressesAsync(DestinationAddressParameters parameters);

        Task<DestinationAddressDto> GetDestinationAddressByIdAysnc(int destinationAddressId);

        Task<DestinationAddressDto> UpdateDestinationAddressAsync(int destinationAddressId, DestinationAddressDto destinationAddressDto);

        Task DeleteDestinationAddressAsync(int destinationAddressId);
    }
}
