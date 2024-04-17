using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public interface IRoomServices
    {
        List<Room> GetRoomById(string number);
        List<Room> GetRoomsAsync();
    }
}