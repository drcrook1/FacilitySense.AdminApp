using FacilitySense.DataModels;

namespace FacilitySense.IRepositories
{
    public interface IRatingRepository
    {
        public Task<IEnumerable<Rating>> GetAllAsync();
        public Task<PaginatedList<Rating>> GetPaginatedListAsync(int pageIndex, int pageSize);
        public Task<Rating?> GetByIdAsync(int id);
        public Task InsertAsync(Rating rating);
        public Task UpdateAsync(Rating rating);
        public Task DeleteAsync(Rating rating);
        public Task DeleteAsync(int id);
    }
}
