using DataAccess;
using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly DapperContext _dapperContext;

        public RoomServices(IConfiguration configuration)
        {
            _dapperContext = new DapperContext(configuration?.GetConnectionString("SqlDbConnection"));
        }

        public List<Room> GetRoomsAsync()
        {
            string query = "SELECT * FROM Rooms";
            List<Room> result = this._dapperContext.GetList<Room>(query, new Dapper.DynamicParameters { });
            return result;
        }

        public List<Room> GetRoomById(string number)
        {
            return GetRoomsAsync().Where(r => r.Number == number).ToList();
        }
    }
}
