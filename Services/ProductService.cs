﻿using FluentValidation;
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
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly ProductValidator _validator;

        public ProductService(DataContext context, ProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateProductAsync(ProductDto productDto)
        {
            _validator.ValidateAndThrow(productDto);

            Product productEntity = productDto.ProductDtoToProduct();

            await _context.AddAsync(productEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdHelperAsync(productId);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(product);
            await SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null || products.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<ProductDto> productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                ProductDto productDto = product.ProductToDto();
                productDtos.Add(productDto);
            }

            return productDtos;

        }

        public async Task<ProductDto> GetProductByIdAysnc(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(e => e.ProductId == productId);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var productDto = product.ProductToDto();
            return productDto;
        }

        public async Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto)
        {
            var oldProductDto = await GetProductByIdHelperAsync(productId);
            if (oldProductDto == null)
            {
                await CreateProductAsync(productDto);
                return oldProductDto.ProductToDto();
            }
            else
            {
                Product product = productDto.ProductDtoToProduct();
                oldProductDto.ProductName = product.ProductName;
                oldProductDto.ProductPrice = product.ProductPrice;
                oldProductDto.ProductDescription = product.ProductDescription;
                oldProductDto.CategoryId = product.CategoryId;
                oldProductDto.BrandId = product.BrandId;
                oldProductDto.EmployeeId = product.EmployeeId;


                await SaveChangesAsync();
                if (oldProductDto.ProductToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldProductDto.ProductToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Product> GetProductByIdHelperAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(e => e.ProductId == productId);

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return product;
        }
    }
}
