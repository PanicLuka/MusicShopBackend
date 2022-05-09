using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(EmployeeDto employeeDto);

        Task<PagedList<EmployeeDto>> GetAllEmployeesAsync(EmployeeParameters parameteres);

        Task<EmployeeDto> GetEmployeeByIdAysnc(int employeeId);

        Task<EmployeeDto> UpdateEmployeeAsync(int employeeId, EmployeeDto employeeDto);

        Task DeleteEmployeeAsync(int employeeId);
    }
}
