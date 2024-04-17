using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public interface ILogServices
    {
        Task CreateAsync(Log log);
    }
}