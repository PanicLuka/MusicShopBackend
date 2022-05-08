using MusicShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(EmployeeDto employeeDto);

        Task<List<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto> GetEmployeeByIdAysnc(int employeeId);

        Task<EmployeeDto> UpdateEmployeeAsync(int employeeId, EmployeeDto employeeDto);

        Task DeleteEmployeeAsync(int employeeId);
    }
}
