using FacilitySense.DataModels;
using FacilitySense.Repositories.SQL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FacilitySense.Repositories
{
    public class RatingRepository
    {
        private readonly FacilitySenseDBContext _sqlContext;
        public RatingRepository(DbContextOptions<FacilitySenseDBContext> options)
        {
            _sqlContext = new FacilitySenseDBContext(options);
        }

        public RatingRepository(FacilitySenseDBContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task DeleteAsync(Rating rating)
        {
            _sqlContext.Ratings.Remove(rating);
            await _sqlContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Rating? rating = await _sqlContext.Ratings.FindAsync(id);
            if (rating != null)
            {
                _ = _sqlContext.Ratings.Remove(rating);
                await _sqlContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            IEnumerable<Rating> ratings = await _sqlContext.Ratings.ToListAsync();
            return ratings;
        }

        public async Task<PaginatedList<Rating>> GetPaginatedListAsync(int pageIndex, int pageSize)
        {
            IQueryable<Rating> queryRatings = from f in _sqlContext.Ratings
                                                   select f;

            //Apply further filtering or sorting queryRatings.Where(f => stuff) or queryRatings.OrderBy(f => stuff)
            var ratings = await PaginatedList<Rating>.CreateAsync(queryRatings, pageIndex, pageSize);

            return ratings;
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _sqlContext.Ratings.FindAsync(id);
        }

        public async Task InsertAsync(Rating rating)
        {
            await _sqlContext.AddAsync(rating);
        }

        public async Task UpdateAsync(Rating rating)
        {
            _sqlContext.Ratings.Update(rating);
            await _sqlContext.SaveChangesAsync();
        }
    }
}
