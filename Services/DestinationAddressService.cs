using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using MusicShopBackend.Validators;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicShopBackend.Services
{
    public class DestinationAddressService : IDestinationAddressService
    {
        private readonly DataContext _context;
        private readonly DestinationValidator _validator;

        public DestinationAddressService(DataContext context, DestinationValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateDestinationAddressAsync(DestinationAddressDto destinationAddressDto)
        {
            _validator.ValidateAndThrow(destinationAddressDto);

            DestinationAddress destinationAddressEntity = destinationAddressDto.DestinationAddressDtoToDestinationAddress();

            await _context.AddAsync(destinationAddressEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteDestinationAddressAsync(int destinationAddressId)
        {
            var destinationAddress = await GetDestinationAddressByIdHelperAsync(destinationAddressId);

            if (destinationAddress == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(destinationAddress);
            await SaveChangesAsync();
        }

        public async Task<List<DestinationAddressDto>> GetAllDestinationAddressesAsync()
        {
            var destinationAddresss = await _context.DestinationAddresses.ToListAsync();
            if (destinationAddresss == null || destinationAddresss.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<DestinationAddressDto> destinationAddressDtos = new List<DestinationAddressDto>();

            foreach (var destinationAddress in destinationAddresss)
            {
                DestinationAddressDto destinationAddressDto = destinationAddress.DestinationAddressToDto();
                destinationAddressDtos.Add(destinationAddressDto);
            }

            return destinationAddressDtos;

        }

        public async Task<DestinationAddressDto> GetDestinationAddressByIdAysnc(int destinationAddressId)
        {
            var destinationAddress = await _context.DestinationAddresses.FirstOrDefaultAsync(e => e.DestinationAddressId == destinationAddressId);
            if (destinationAddress == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var destinationAddressDto = destinationAddress.DestinationAddressToDto();
            return destinationAddressDto;
        }

        public async Task<DestinationAddressDto> UpdateDestinationAddressAsync(int destinationAddressId, DestinationAddressDto destinationAddressDto)
        {
            var oldDestinationAddressDto = await GetDestinationAddressByIdHelperAsync(destinationAddressId);
            if (oldDestinationAddressDto == null)
            {
                await CreateDestinationAddressAsync(destinationAddressDto);
                return oldDestinationAddressDto.DestinationAddressToDto();
            }
            else
            {
                DestinationAddress destinationAddress = destinationAddressDto.DestinationAddressDtoToDestinationAddress();
                oldDestinationAddressDto.City = destinationAddress.City;
                oldDestinationAddressDto.ZipCode = destinationAddress.ZipCode;
                oldDestinationAddressDto.Country = destinationAddress.Country;
                oldDestinationAddressDto.PhoneNumber = destinationAddress.PhoneNumber;
                oldDestinationAddressDto.Address = destinationAddress.Address;
               

                await SaveChangesAsync();
                if (oldDestinationAddressDto.DestinationAddressToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldDestinationAddressDto.DestinationAddressToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<DestinationAddress> GetDestinationAddressByIdHelperAsync(int destinationAddressId)
        {
            var destinationAddress = await _context.DestinationAddresses.FirstOrDefaultAsync(e => e.DestinationAddressId == destinationAddressId);

            if (destinationAddress == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return destinationAddress;
        }
    }
}
