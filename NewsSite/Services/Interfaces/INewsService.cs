using NewsSite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NewsSite.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetLatestAsync(int count);
        Task<IEnumerable<News>> GetAllAsync();
        Task<News?> GetByIdAsync(int id);

        Task<News> CreateAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(int id);
    }
}
