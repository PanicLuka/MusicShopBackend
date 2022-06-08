using MusicShopBackend.Entities;
using MusicShopBackend.Models;

namespace MusicShopBackend.Helpers
{
    public static class ManualMapper
    {
        public static BrandDto BrandToDto(this Brand brand)
        {
            if (brand != null)
            {
                return new BrandDto
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName
                };

            }
            return null;
        }
        public static Brand BrandDtoToBrand(this BrandDto brandDto)
        {
            if (brandDto != null)
            {
                return new Brand
                {
                    BrandId = brandDto.BrandId,
                    BrandName = brandDto.BrandName
                };

            }
            return null;
        }

        public static CategoryDto CategoryToDto(this Category category)
        {
            if (category != null)
            {
                return new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription
                };
            }
            return null;

        }

        public static Category CategoryDtoToCategory(this CategoryDto categoryDto)
        {
            if (categoryDto != null)
            {
                return new Category
                {
                    CategoryId = categoryDto.CategoryId,
                    CategoryName = categoryDto.CategoryName,
                    CategoryDescription = categoryDto.CategoryDescription
                };
            }
            return null;

        }

        public static CreditCardDto CreditCardToDto(this CreditCard creditCard)
        {
            if (creditCard != null)
            {
                return new CreditCardDto
                {
                    CreditCardId = creditCard.CreditCardId,
                    CreditCardNumber = creditCard.CreditCardNumber,
                    Cvv = creditCard.Cvv,
                    ExpireDate = creditCard.ExpireDate
                };
            }
            return null;

        }

        public static CreditCard CreditCardDtoToCreditCard(this CreditCardDto creditCardDto)
        {
            if (creditCardDto != null)
            {
                return new CreditCard
                {
                    CreditCardId = creditCardDto.CreditCardId,
                    CreditCardNumber = creditCardDto.CreditCardNumber,
                    Cvv = creditCardDto.Cvv,
                    ExpireDate = creditCardDto.ExpireDate
                };
            }
            return null;

        }

        public static DestinationAddressDto DestinationAddressToDto(this DestinationAddress destinationAddress)
        {
            if (destinationAddress != null)
            {
                return new DestinationAddressDto
                {
                    DestinationAddressId = destinationAddress.DestinationAddressId,
                    City = destinationAddress.City,
                    ZipCode = destinationAddress.ZipCode,
                    Country = destinationAddress.Country,
                    PhoneNumber = destinationAddress.PhoneNumber,
                    Address = destinationAddress.Address,
                };
            }
            return null;
        }

        public static DestinationAddress DestinationAddressDtoToDestinationAddress(this DestinationAddressDto destinationAddressDto)
        {
            if (destinationAddressDto != null)
            {
                return new DestinationAddress
                {
                    DestinationAddressId = destinationAddressDto.DestinationAddressId,
                    City = destinationAddressDto.City,
                    ZipCode = destinationAddressDto.ZipCode,
                    Country = destinationAddressDto.Country,
                    PhoneNumber = destinationAddressDto.PhoneNumber,
                    Address = destinationAddressDto.Address,
                };
            }
            return null;
        }

        public static EmployeeDto EmployeeToDto(this Employee employee)
        {
            if (employee != null)
            {
                return new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    Email = employee.Email,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Password = employee.Password,
                    City = employee.City,
                    Contact = employee.Contact
                };
            }
            return null;
        }

        public static Employee EmployeeDtoToEmployee(this EmployeeDto employeeDto)
        {
            if (employeeDto != null)
            {
                return new Employee
                {
                    EmployeeId = employeeDto.EmployeeId,
                    Email = employeeDto.Email,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Password = employeeDto.Password,
                    City = employeeDto.City,
                    Contact = employeeDto.Contact
                };
            }
            return null;
        }

        public static OrderDto OrderToDto(this Order order)
        {
            if (order != null)
            {
                return new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderArrival = order.OrderArrival,
                    OrderDate = order.OrderDate,
                    PaymentType = order.PaymentType,
                    OrderStatus = order.OrderStatus,
                    CreditCardId = order.CreditCardId,
                    UserId = order.UserId,
                    DestinationAddressId = order.DestinationAddressId,

                };

            }
            return null;
        }
        public static Order OrderDtoToOrder(this OrderDto orderDto)
        {
            if (orderDto != null)
            {
                return new Order
                {
                    OrderId = orderDto.OrderId,
                    OrderArrival = orderDto.OrderArrival,
                    OrderDate = orderDto.OrderDate,
                    PaymentType = orderDto.PaymentType,
                    OrderStatus = orderDto.OrderStatus,
                    CreditCardId = orderDto.CreditCardId,
                    UserId = orderDto.UserId,
                    DestinationAddressId = orderDto.DestinationAddressId,

                };

            }
            return null;
        }

        public static OrderProductDto OrderProductToDto(this OrderProduct orderProduct)
        {
            if (orderProduct != null)
            {
                return new OrderProductDto
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    OrderQuantity = orderProduct.OrderQuantity
                };
            }
            return null;
        }

        public static OrderProduct OrderProductDtoToOrderProduct(this OrderProductDto orderProductDto)
        {
            if (orderProductDto != null)
            {
                return new OrderProduct
                {
                    OrderId = orderProductDto.OrderId,
                    ProductId = orderProductDto.ProductId,
                    OrderQuantity = orderProductDto.OrderQuantity
                };
            }
            return null;
        }

        public static ProductDto ProductToDto(this Product product)
        {
            if (product != null)
            {
                return new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductDescription = product.ProductDescription,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ImgPath = product.ImgPath,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    EmployeeId = product.EmployeeId,
                    
                };
            }
            return null;
        }

        public static Product ProductDtoToProduct(this ProductDto productDto)
        {
            if (productDto != null)
            {
                return new Product
                {
                    ProductId = productDto.ProductId,
                    ProductDescription = productDto.ProductDescription,
                    ProductName = productDto.ProductName,
                    ProductPrice = productDto.ProductPrice,
                    ImgPath = productDto.ImgPath,
                    BrandId = productDto.BrandId,
                    CategoryId = productDto.CategoryId,
                    EmployeeId = productDto.EmployeeId
                };
            }
            return null;
        }

        public static UserDto UserToDto(this User user)
        {
            if (user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                };
            }
            return null;
        }

        public static User UserDtoToUser(this UserDto userDto)
        {
            if (userDto != null)
            {
                return new User
                {
                    UserId = userDto.UserId,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Password = userDto.Password


                };
            }
            return null;
        }


        public static RoleDto RoleToDto(this Role role)
        {
            if (role != null)
            {
                return new RoleDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                };
            }
            return null;
        }

        public static Role RoleDtoToRole(this RoleDto roleDto)
        {
            if (roleDto != null)
            {
                return new Role
                {   
                    RoleId = roleDto.RoleId,
                    RoleName = roleDto.RoleName
                };
            }
            return null;
        }


    }
}
