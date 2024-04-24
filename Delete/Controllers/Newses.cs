using Delete.ClassLibrary1.Models;
using Microsoft.AspNetCore.Mvc;
using Delete.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Delete.ClassLibrary1;
using System.Security.Cryptography.Xml;


namespace Delete.Controllers
{
    public class Newses : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainContext _mainContext;

        public Newses(ILogger<HomeController> logger, MainContext mainContext)
        {
            _logger = logger;
            _mainContext = mainContext;
        }

        public IActionResult VseNews()
        {
            var news = _mainContext.News.OrderByDescending(x => x.Date).ToList();
            return View(news);
        }

        public IActionResult CreateNews() 
        {
            return View();
		}

        [HttpPost]
  
        public async Task<IActionResult> CreateNews(NewsViewModel news)
		{
            News n = new News { Name = news.Name, Description = news.Description};
            if (news.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(news.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)news.Image.Length);
                }
                // установка массива байтов
                n.Image = imageData;
            }
            _mainContext.News.Add(n);
			await _mainContext.SaveChangesAsync();
			return RedirectToAction("VseNews");
		}


        public async Task<IActionResult> DeleteNews(int? id)
        {
            if (id != 0)
            {
                News n = await _mainContext.News.FindAsync(id);

                if (n != null)
                {
                    _mainContext.News.Remove(n);
                    await _mainContext.SaveChangesAsync();
                    return RedirectToAction("VseNews");
                }
                return RedirectToAction("Error");
            }
            else
                return RedirectToAction("Error");

        }
    }
}
