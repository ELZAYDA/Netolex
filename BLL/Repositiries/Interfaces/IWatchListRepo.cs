using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IWatchListRepo : IGenericRepo<WatchList>
    {
        Task<List<Movie>> GetUserWatchlistAsync(string userId);
        Task<bool> AddToWatchlistAsync(string userId, int movieId);
        Task<bool> RemoveFromWatchlistAsync(string userId, int movieId);
        Task<bool> IsInWatchlistAsync(string userId, int movieId);
        Task<int> GetWatchlistCountAsync(string userId);
    }
}
