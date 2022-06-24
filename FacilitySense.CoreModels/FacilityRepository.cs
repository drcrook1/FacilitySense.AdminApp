using FacilitySense.DataModels;
using FacilitySense.IRepositories;
using FacilitySense.Repositories.SQL;
using Microsoft.EntityFrameworkCore;

namespace FacilitySense.Repositories
{
    //Uses Spatial Data: https://docs.microsoft.com/en-us/ef/core/modeling/spatial
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FacilitySenseDBContext _sqlContext;
        public FacilityRepository(FacilitySenseDBContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task DeleteAsync(Facility facility)
        {
            _sqlContext.Facilities.Remove(facility);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Facility? facility = await _sqlContext.Facilities.FindAsync(id);
            if (facility != null)
            {
                _ = _sqlContext.Facilities.Remove(facility);
                await _sqlContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Facility>> GetAllAsync()
        {
            IEnumerable<Facility> facilities = await _sqlContext.Facilities.ToListAsync();
            return facilities;
        }

        public async Task<PaginatedList<Facility>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            IQueryable<Facility> queryFacilities = from f in _sqlContext.Facilities
                                              select f;

            //Apply further filtering or sorting queryFacilities.Where(f => stuff) or queryFacilities.OrderBy(f => stuff)
            var facilities = await PaginatedList<Facility>.CreateAsync(queryFacilities, pageIndex, pageSize);

            return facilities;
        }

        public async Task<Facility?> GetByIdAsync(int id)
        {
            return await _sqlContext.Facilities.FindAsync(id);
        }

        public async Task InsertAsync(Facility facility)
        {
            await _sqlContext.AddAsync(facility);
        }

        public async Task UpdateAsync(Facility facility)
        {
            _sqlContext.Facilities.Update(facility);
            await _sqlContext.SaveChangesAsync();
        }
    }
}
