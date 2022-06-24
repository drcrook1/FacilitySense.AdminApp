using FacilitySense.DataModels;

namespace FacilitySense.IRepositories
{
    public interface IFacilityRepository
    {
        public Task<IEnumerable<Facility>> GetAllAsync();
        public Task<PaginatedList<Facility>> GetPaginatedListAsync(int pageIndex, int pageSize);
        public Task<Facility?> GetByIdAsync(int id);
        public Task InsertAsync(Facility facility);
        public Task UpdateAsync(Facility facility);
        public Task DeleteAsync(Facility facility);
        public Task DeleteAsync(int id);
    }
}