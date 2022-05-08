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
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        private readonly EmployeeValidator _validator;

        public EmployeeService(DataContext context, EmployeeValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            _validator.ValidateAndThrow(employeeDto);

            Employee employeeEntity = employeeDto.EmployeeDtoToEmployee();

            await _context.AddAsync(employeeEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await GetEmployeeByIdHelperAsync(employeeId);

            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(employee);
            await SaveChangesAsync();
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            if (employees == null || employees.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                EmployeeDto employeeDto = employee.EmployeeToDto();
                employeeDtos.Add(employeeDto);
            }

            return employeeDtos;

        }

        public async Task<EmployeeDto> GetEmployeeByIdAysnc(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var employeeDto = employee.EmployeeToDto();
            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int employeeId, EmployeeDto employeeDto)
        {
            var oldEmployeeDto = await GetEmployeeByIdHelperAsync(employeeId);
            if (oldEmployeeDto == null)
            {
                await CreateEmployeeAsync(employeeDto);
                return oldEmployeeDto.EmployeeToDto();
            }
            else
            {
                Employee employee = employeeDto.EmployeeDtoToEmployee();
                oldEmployeeDto.FirstName = employee.FirstName;
                oldEmployeeDto.LastName = employee.LastName;
                oldEmployeeDto.Email = employee.Email;
                oldEmployeeDto.Password = employee.Password;
                oldEmployeeDto.Contact = employee.Contact;
                oldEmployeeDto.City = employee.City;

                await SaveChangesAsync();
                if (oldEmployeeDto.EmployeeToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldEmployeeDto.EmployeeToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Employee> GetEmployeeByIdHelperAsync(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return employee;
        }
    }
}
