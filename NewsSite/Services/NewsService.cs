using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models;
using NewsSite.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  

namespace NewsSite.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetLatestAsync(int count)
        {
            return await _context.News
                .OrderByDescending(n => n.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<News> CreateAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.News.FindAsync(id);
            if (entity != null)
            {
                _context.News.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
