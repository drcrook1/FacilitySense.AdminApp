using FacilitySense.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilitySense.IRepositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<PaginatedList<User>> GetPaginatedListAsync(int pageIndex, int pageSize);
        public Task<User?> GetByIdAsync(int id);
        public Task InsertAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
        public Task DeleteAsync(int id);
    }
}
