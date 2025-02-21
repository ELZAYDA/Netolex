//using BLL.Repositiries.Interfaces;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Implementation
{
    public class WatchListRepo : GenericRepo<WatchList>, IWatchListRepo
    {
     

        private readonly DbApplicationContext _context;

        public WatchListRepo(DbApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetUserWatchlistAsync(string userId)
        {
            return await _context.WatchLists
                .Where(item => item.UserId == userId)
                .Include(item => item.Movie)
                    .ThenInclude(m => m.MovieGenres)
                .OrderByDescending(item => item.DateAdded)
                .Select(item => item.Movie)
                .ToListAsync();
        }

        public async Task<bool> AddToWatchlistAsync(string userId, int movieId)
        {
            // Check if movie exists
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
            if (!movieExists)
            {
                return false;
            }

            // Check if already in watchlist
            var alreadyInWatchlist = await _context.WatchLists
                .AnyAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (alreadyInWatchlist)
            {
                return true; // Already in watchlist, consider it a success
            }

            // Add to watchlist
            var watchlistItem = new WatchList
            {
                UserId = userId,
                MovieId = movieId,
                DateAdded = DateTime.UtcNow
            };

            await _context.WatchLists.AddAsync(watchlistItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromWatchlistAsync(string userId, int movieId)
        {
            var watchlistItem = await _context.WatchLists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (watchlistItem == null)
            {
                return false;
            }

            _context.WatchLists.Remove(watchlistItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsInWatchlistAsync(string userId, int movieId)
        {
            return await _context.WatchLists
                .AnyAsync(w => w.UserId == userId && w.MovieId == movieId);
        }

        public async Task<int> GetWatchlistCountAsync(string userId)
        {
            return await _context.WatchLists
                .CountAsync(w => w.UserId == userId);
        }
    }
}
