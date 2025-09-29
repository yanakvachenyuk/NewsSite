using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Services;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;

        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            var latestNews = await _newsService.GetLatestAsync(3);
            return View(latestNews);
        }

        // GET: /Home/AllNews
        public async Task<IActionResult> AllNews()
        {
            var allNews = await _newsService.GetAllAsync();
            return View(allNews);
        }

        // GET: /Home/NewsDetails/5
        public async Task<IActionResult> NewsDetails(int id)
        {
            var news = await _newsService.GetByIdAsync(id);
            if (news == null) return NotFound();

            return View(news);
        }

        // GET: /Home/About
        public IActionResult About() => View();

        // GET: /Home/Contact
        public IActionResult Contact() => View();

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                });

            return LocalRedirect(returnUrl);
        }
    }
}
