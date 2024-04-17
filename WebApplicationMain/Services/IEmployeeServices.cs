using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public interface IEmployeeServices
    {
        Task CreateAsync(Employee employee);
        Task DeleteAsync(string id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetById(string id);
        Task UpdateAsync(string id, Employee employee);
    }
}