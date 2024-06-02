using BusinessLogicLayer.Dto;
using DataAccessLayer.Entity.Models;


namespace BusinessLogicLayer
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeGetDto>> GetAllEmployeesAsync();
        Task<EmployeeGetDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeGetDto> AddEmployeeAsync(EmployeePostDto employee);
        Task<IEnumerable<EmployeeGetDtoForProject>> GetEmployeesForProjectAsync();
        Task<IEnumerable<EmployeeGetDtoForProject>> GetEmployeesProjectManagerAsync();
        Task<bool> UpdateEmployeeAsync(int id, EmployeePostDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
