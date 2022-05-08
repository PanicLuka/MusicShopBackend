using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IDestinationAddressService
    {
        Task CreateDestinationAddressAsync(DestinationAddressDto destinationAddressDto);

        Task<List<DestinationAddressDto>> GetAllDestinationAddressesAsync();

        Task<DestinationAddressDto> GetDestinationAddressByIdAysnc(int destinationAddressId);

        Task<DestinationAddressDto> UpdateDestinationAddressAsync(int destinationAddressId, DestinationAddressDto destinationAddressDto);

        Task DeleteDestinationAddressAsync(int destinationAddressId);
    }
}
