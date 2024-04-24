using Delete.ClassLibrary1;
using Delete.ClassLibrary1.Models;
using Delete.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace Delete.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainContext _mainContext;  

        public HomeController(ILogger<HomeController> logger, MainContext mainContext)
        {
            _logger = logger;
            _mainContext = mainContext;
        }

       



        public async Task<IActionResult> MoyPokupki()
        {
            var id = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;

            var ts = _mainContext.Zakazs.Where(x => x.UserId == id).Include(c => c.Pokupkas).Where(x => x.UserId == id).ToList();
       
            return View(ts);
        }


        public async Task<IActionResult> Zakazy()
        {
           
            var ts = _mainContext.Zakazs.Include(c => c.Pokupkas).OrderBy(x => x.UserId).ToList();

            return View(ts);
        }




        //public async Task<IActionResult> MoyPokupkis()
        //{
        //    var id = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;

        //    var a = _mainContext.Zakazs.Where(x => x.UserId == id).Include(t => t.Pokupkas.Where(x => x.UserId == id)).ToList();

        //    List<Zakaz> zakazs = new List<Zakaz>();

           

        //    if (a != null)
        //    {
        //        foreach (var item in a)
        //        { 
        //            zakazs = _mainContext.Zakazs.Where(x => x.UserId == item.UserId).ToList();
        //            //item.PokupkaId = _mainContext.Zakazs.Find(result).PokupkaId;
        //            //item.UserId = _mainContext.Zakazs.Find(result).UserId;
        //            //item.Number = _mainContext.Zakazs.Find(result).Number;

        //            foreach (var item1 in zakazs)
        //            {
        //                foreach (var item2 in item1.Pokupkas)
        //                {
        //                    var a1 = item2.Id;
        //                    var a2 = item2.ZakazId;
        //                    var a3 = item2.UserId;
        //                    var a4 = item2.Name;
        //                    var a5 = item2.Gb;
        //                    var a6 = item2.Sale;
        //                }
        //            }
        //        }

        //    }



        //    return View(zakazs);
        //}





        public IActionResult Iphoneel() 
        {
            return View();
        }

        public IActionResult IphoneP()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ONas()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}