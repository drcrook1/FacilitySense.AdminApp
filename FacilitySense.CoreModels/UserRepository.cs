using FacilitySense.DataModels;
using FacilitySense.Repositories.SQL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilitySense.Repositories
{
    public class UserRepository
    {
        private readonly FacilitySenseDBContext _sqlContext;
        public UserRepository(DbContextOptions<FacilitySenseDBContext> options)
        {
            _sqlContext = new FacilitySenseDBContext(options);
        }

        public UserRepository(FacilitySenseDBContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task DeleteAsync(User user)
        {
            _sqlContext.Users.Remove(user);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User? user = await _sqlContext.Users.FindAsync(id);
            if (user != null)
            {
                _ = _sqlContext.Users.Remove(user);
                await _sqlContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IEnumerable<User> users = await _sqlContext.Users.ToListAsync();
            return users;
        }

        public async Task<PaginatedList<User>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            IQueryable<User> queryUsers = from f in _sqlContext.Users
                                              select f;

            //Apply further filtering or sorting queryUsers.Where(f => stuff) or queryUsers.OrderBy(f => stuff)
            var users = await PaginatedList<User>.CreateAsync(queryUsers, pageIndex, pageSize);

            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _sqlContext.Users.FindAsync(id);
        }

        public async Task InsertAsync(User user)
        {
            await _sqlContext.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            _sqlContext.Users.Update(user);
            await _sqlContext.SaveChangesAsync();
        }
    }
}