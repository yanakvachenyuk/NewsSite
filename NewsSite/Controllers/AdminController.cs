using Microsoft.AspNetCore.Mvc;
using NewsSite.Models;
using NewsSite.Services;                
using NewsSite.Services.Interfaces;     

namespace NewsSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthService _authService;
        private readonly INewsService _newsService;
        private readonly IFileStorage _fileStorage;

        public AdminController(
            IAuthService authService,
            INewsService newsService,
            IFileStorage fileStorage)
        {
            _authService = authService;
            _newsService = newsService;
            _fileStorage = fileStorage;
        }

        // GET: /Admin/Login
        public IActionResult Login() => View();

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_authService.ValidateUser(email, password))
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction(nameof(NewsList));
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: /Admin/NewsList
        public async Task<IActionResult> NewsList()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            var news = await _newsService.GetAllAsync();
            return View(news);
        }

        // GET: /Admin/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            return View();
        }

        // POST: /Admin/Create
        [HttpPost]
        public async Task<IActionResult> Create(News news)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            if (!ModelState.IsValid)
                return View(news);

            if (news.ImageFile != null)
            {
                news.ImageFileName = await _fileStorage.SaveFileAsync(news.ImageFile);
            }

            news.CreatedDate = DateTime.UtcNow;
            await _newsService.CreateAsync(news);

            return RedirectToAction(nameof(NewsList));
        }

        // GET: /Admin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            var news = await _newsService.GetByIdAsync(id);
            if (news == null) return NotFound();

            return View(news);
        }

        // POST: /Admin/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, News news)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            if (id != news.Id) return NotFound();
            if (!ModelState.IsValid) return View(news);

            var existingNews = await _newsService.GetByIdAsync(id);
            if (existingNews == null) return NotFound();

            if (news.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existingNews.ImageFileName))
                    _fileStorage.DeleteFile(existingNews.ImageFileName);

                existingNews.ImageFileName = await _fileStorage.SaveFileAsync(news.ImageFile);
            }

            existingNews.Title = news.Title;
            existingNews.Subtitle = news.Subtitle;
            existingNews.Content = news.Content;

            await _newsService.UpdateAsync(existingNews);
            return RedirectToAction(nameof(NewsList));
        }

        // POST: /Admin/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction(nameof(Login));

            var news = await _newsService.GetByIdAsync(id);
            if (news != null)
            {
                if (!string.IsNullOrEmpty(news.ImageFileName))
                    _fileStorage.DeleteFile(news.ImageFileName);

                await _newsService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(NewsList));
        }

        // GET: /Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction(nameof(Login));
        }

        private bool IsAdminLoggedIn()
            => HttpContext.Session.GetString("IsAdmin") == "true";
    }
}
