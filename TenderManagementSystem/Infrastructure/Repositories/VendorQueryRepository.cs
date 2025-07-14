using Dapper;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Infrastructure.Abstractions;
using TenderManagementSystem.Infrastructure.Data.Dapper;

namespace TenderManagementSystem.Infrastructure.Repositories
{
    public class VendorQueryRepository : IVendorQueryRepository
    {
        private readonly IDapperContext _context;

        public VendorQueryRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<Vendor> GetByIdAsync(string id)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                SELECT * FROM Vendors 
                WHERE Id = @Id AND IsDeleted = 0";

            return await connection.QuerySingleOrDefaultAsync<Vendor>(sql, new {Id = id});
        }

        public async Task<Vendor> GetByUserIdAsync(string userId)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                SELECT * FROM Vendors 
                WHERE UserId = @UserId AND IsDeleted = 0";

            return await connection.QuerySingleOrDefaultAsync<Vendor>(sql, new {UserId = userId});
        }

        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                SELECT * FROM Vendors 
                WHERE IsDeleted = 0 
                ORDER BY Name";

            return await connection.QueryAsync<Vendor>(sql);
        }

        public async Task<Vendor> GetWithDetailsAsync(string id)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                SELECT v.*, u.UserName, u.Email 
                FROM Vendors v
                JOIN Users u ON v.UserId = u.Id
                WHERE v.Id = @Id AND v.IsDeleted = 0";

            return await connection.QuerySingleOrDefaultAsync<Vendor>(sql, new {Id = id});
        }
    }
}