using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IMongoCollection<Employee> _employee;
        private readonly IOptions<MongoSettings> _mongoSettings;

        public EmployeeServices(IOptions<MongoSettings> options)
        {
            _mongoSettings = options;
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _employee = mongoDatabase.GetCollection<Employee>(options.Value.EmployeesCollectionName);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() =>
            await _employee.Find(_ => true).ToListAsync();

        public async Task<Employee> GetById(string id) =>
            await _employee.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Employee employee) =>
            await _employee.InsertOneAsync(employee);

        public async Task UpdateAsync(string id, Employee employee) =>
            await _employee.ReplaceOneAsync(e => e.Id == id, employee);

        public async Task DeleteAsync(string id) =>
            await _employee.DeleteOneAsync(e => e.Id == id);
    }
}
