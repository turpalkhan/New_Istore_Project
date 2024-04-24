using Delete.ClassLibrary1;
using Delete.ClassLibrary1.Models;
using Delete.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Delete.Controllers
{
    public class Catalog : Controller
    {
        private readonly ILogger<Catalog> _logger;
        private readonly MainContext _mainContext;

        public Catalog(ILogger<Catalog> logger, MainContext mainContext)
        {
            _logger = logger;
            _mainContext = mainContext;
        }


      
        /// <summary>
        /// /kmkmo
        /// </summary>
        /// <returns></returns>
        public IActionResult VsePhone()
        {
            var phones = _mainContext.Phones.OrderByDescending(x => x.VNalichie).ToList();
            return View(phones);
        }



        [HttpPost]
        public IActionResult VsePhone(List<string> name, List<string> baground, List<string>gb)
        {
             
            //int d = 1;
            //string ns;
            //ns = name[d];
            //var ph = _mainContext.Phones.ToList();

            //ph = null;

            List<Phone> ph = new List<Phone>();
            string n;
            string b;
            string c;

            int nCol = name.Count();
            int bCol = baground.Count();
            int gCol = gb.Count();


            for (int i = 0; i < name.Count(); i++)
            {
                n = name[i];

                
                if (bCol + gCol == 0)
                {
                   var p = _mainContext.Phones.Where(x => x.Name == n).ToList();
                    ph.AddRange(p);
                }

             
                for (int j = 0; j < baground.Count(); j++)
                {
                    b = baground[j];

                    //var phone = _mainContext.Phones.Where(x => x.Name == n && x.Baground == b).ToList();

                    //if (phone != null)
                    //{
                    //    ph.AddRange(phone);
                    //}

                    for (int l = 0; l < gb.Count(); l++)
                    {
                        c = gb[l];

                        var phone = _mainContext.Phones.Where(x => x.Name == n && x.Baground == b && x.Gb == c).ToList();

                        //if (nCol + bCol == 0)
                        //{
                        //    var p = _mainContext.Phones.Where(x => x.Gb == c).ToList();
                        //    ph.AddRange(p);
                        //}


                        if (phone != null)
                        {
                            ph.AddRange(phone);
                        }
                    }

                }







            }

            return View(ph);
            //var a = 0;
            //ph[a] = phone[a];
            //foreach (var item in phone)
            //{
            //    var g1 = phone.FirstOrDefault(x => x.Name != null).Name;
            //    var g2 = phone.FirstOrDefault(x => x.Sale != 0).Sale;
            //    var g3 = phone.FirstOrDefault(x => x.VNalichie != null).VNalichie;
            //    var g4 = phone.FirstOrDefault(x => x.Amount != null).Amount;
            //    var g5 = phone.FirstOrDefault(x => x.Name != null).Name;
            //    var g6 = phone.FirstOrDefault(x => x.Name != null).Name;
            //    var g7 = phone.FirstOrDefault(x => x.Name != null).Name;
            //    var g8 = phone.FirstOrDefault(x => x.Name != null).Name;
            //}
        }


        //    var phones = _mainContext.Phones.Where(x => x.Name == name && x.Baground == baground).ToList();

        //    if (baground == null)
        //    {
        //        phones = _mainContext.Phones.Where(x => x.Name == name).ToList();
        //    }
        //    if (name == null)
        //    {
        //        phones = _mainContext.Phones.Where(x => x.Baground == baground).ToList();
        //    }
        //    return View(phones);
        //}


        public async Task<IActionResult> EditPhone(int? id)
        {
            PhoneViewModels phoneViewModels = new PhoneViewModels();
            if (id != 0)
            {

                Phone phone = await _mainContext.Phones.FindAsync(id);
                if (phone != null)
                {
                    PhoneEditNewModels phoneEditNew = new PhoneEditNewModels();
                    phoneEditNew.Id = phone.Id;
                    phoneEditNew.Image = phoneViewModels.Image;
                    phoneEditNew.Name = phone.Name;
                    phoneEditNew.Gb = phone.Gb;
                    phoneEditNew.Baground = phone.Baground;
                    phoneEditNew.VNalichie = phone.VNalichie;
                    return View(phoneEditNew);

                }
                return RedirectToAction("Error");
            }
            else
                return RedirectToAction("Error");
        }



        [HttpPost]
        public async Task<IActionResult> EditPhone(PhoneEditNewModels phoneEditNew)
        {
            var phone = await _mainContext.Phones.FindAsync(phoneEditNew.Id);

            phone.Name = phoneEditNew.Name;
            phone.Gb = phoneEditNew.Gb;
            phone.Baground = phoneEditNew.Baground;
            phone.Sale = phoneEditNew.Sale;
            phone.VNalichie = phoneEditNew.VNalichie;
            phone.Amount = phoneEditNew.Amount;
            

            if (phoneEditNew.Image != null)
            {
                using (var binaryRedear = new BinaryReader(phoneEditNew.Image.OpenReadStream()))
                {
                    phone.Image = binaryRedear.ReadBytes((int)phoneEditNew.Image.Length);
                }
            }

            _mainContext.Phones.Update(phone);
            await _mainContext.SaveChangesAsync();
            return RedirectToAction("VsePhone");
        }



        [HttpGet]
        public IActionResult CreatePhone()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreatePhone(PhoneViewModels phone)
        {
            Phone n = new Phone { Id = phone.Id, Name = phone.Name, Gb = phone.Gb, Baground = phone.Baground, Sale = phone.Sale, VNalichie = phone.VNalichie, Amount = phone.Amount };
            if (phone.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(phone.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)phone.Image.Length);
                }
                // установка массива байтов
                n.Image = imageData;
            }
            _mainContext.Phones.Add(n);
            _mainContext.SaveChanges();
            return RedirectToAction("VsePhone");
        }


        public IActionResult Oformleniy()
        {
            return View();
        }

        public async Task<IActionResult> DeletePhone(int? id)
        {
            if (id != 0)
            {
                Phone n = await _mainContext.Phones.FindAsync(id);

                if (n != null)
                {
                    _mainContext.Phones.Remove(n);
                    await _mainContext.SaveChangesAsync();
                    return RedirectToAction("VsePhone");
                }
                return RedirectToAction("Error");
            }
            else
                return RedirectToAction("Error");
        }



        public async Task<IActionResult> DeleteFavorite(int? id)
        {
            if (id != 0)
            {
                Favorite n = await _mainContext.Favorites.FindAsync(id);

                if (n != null)
                {
                    _mainContext.Favorites.Remove(n);
                    await _mainContext.SaveChangesAsync();
                    return RedirectToAction("Oformleniy");
                }
                return RedirectToAction("Error");
            }
            else
                return RedirectToAction("Error");
            //SAFDJASF
        }


        public async Task<IActionResult> DeleteThis(int? id)
        {
            if (id != 0)
            {
                Favorite n = await _mainContext.Favorites.FindAsync(id);

                if (n != null)
                {
                    _mainContext.Favorites.Remove(n);
                    await _mainContext.SaveChangesAsync();
                    return RedirectToAction("FavoritePage");
                }
                return RedirectToAction("Error");
            }
            else
                return RedirectToAction("Error");
        }



        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Finish()
        {
            return View();
        }

        public async Task<IActionResult> AddFavorite(int id)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                if (id != 0)
                {
                    var rezult = _mainContext.Favorites.FirstOrDefault(f => f.PhoneId == id && f.UserId == _mainContext.Users.FirstOrDefault(k => k.Login == User.Identity.Name).Id);
                    if (rezult == null)
                    {
                        Favorite favorite = new Favorite();
                        var p = _mainContext.Phones.Find(id);

                        if (p.Amount != 0 || p.Amount > 0)
                        {
                            favorite = new Favorite
                            {
                                PhoneId = _mainContext.Phones.Find(id).Id,
                                UserId = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id,
                                Image = _mainContext.Phones.Find(id).Image,
                                Name = _mainContext.Phones.Find(id).Name,
                                Gb = _mainContext.Phones.Find(id).Gb,
                                Baground = _mainContext.Phones.Find(id).Baground,
                                Sale = _mainContext.Phones.Find(id).Sale,
                                VNalichie = _mainContext.Phones.Find(id).VNalichie,
                                Amount = 1
                            };
                            _mainContext.Favorites.Add(favorite);
                        }

                        if (p.Amount <= 0)
                        {
                            _mainContext.Phones.Remove(p);
                            ArhivPhone f = new ArhivPhone
                            {
                                PhoneId = _mainContext.Phones.Find(id).Id,
                                Name = _mainContext.Phones.Find(id).Name,
                                Baground = _mainContext.Phones.Find(id).Baground,
                                Image = _mainContext.Phones.Find(id).Image,
                                Gb = _mainContext.Phones.Find(id).Gb,
                                Sale = _mainContext.Phones.Find(id).Sale,
                                VNalichie = _mainContext.Phones.Find(id).VNalichie,
                                Amount = _mainContext.Phones.Find(id).Amount
                            };
                           _mainContext.ArhivPhones.Add(f);

                        }

                        await _mainContext.SaveChangesAsync();
                    }

                }
                return RedirectToAction("VsePhone");
            }
            else
                return RedirectToAction("Registration");
        }

        public IActionResult FavoritePage()
        {
            var id = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;
            return View(_mainContext.Favorites.Where(x => x.UserId == id).Where(x => x.VNalichie == "В наличии").ToList());
        }


        public async Task<IActionResult> ArhivPhone(int id)
        {
            if (id != 0)
            {
                var rezult = await _mainContext.Phones.FindAsync(id);
                if (rezult != null)
                {
                    ArhivPhone arhiv = new ArhivPhone
                    {
                        PhoneId = _mainContext.Phones.Find(id).Id,
                        Image = _mainContext.Phones.Find(id).Image,
                        Name = _mainContext.Phones.Find(id).Name,
                        Gb = _mainContext.Phones.Find(id).Gb,
                        Baground = _mainContext.Phones.Find(id).Baground,
                        VNalichie = _mainContext.Phones.Find(id).VNalichie,
                        Sale = _mainContext.Phones.Find(id).Sale,
                        Amount = _mainContext.Phones.Find(id).Amount
                    };

                    _mainContext.ArhivPhones.Add(arhiv);
                    await _mainContext.SaveChangesAsync();

                    _mainContext.Phones.Remove(rezult);
                    await _mainContext.SaveChangesAsync();
                }


            }
            return RedirectToAction("Arhive");
        }



        public IActionResult Arhive()
        {
            var arhive = _mainContext.ArhivPhones.OrderByDescending(x => x.Id).ToList();
            return View(arhive);
        }


        public async Task<IActionResult> Vostanovlenie(int id)
        {
            var arhive = _mainContext.ArhivPhones.Find(id);
            if (arhive != null)
            {
                Phone phone = new Phone
                {
                    //Id = _mainContext.ArhivPhones.FirstOrDefault(x => x.Id == id).PhoneId,
                    Image = _mainContext.ArhivPhones.Find(id).Image,
                    Name = _mainContext.ArhivPhones.Find(id).Name,
                    Baground = _mainContext.ArhivPhones.Find(id).Baground,
                    Gb = _mainContext.ArhivPhones.Find(id).Gb,
                    Sale = _mainContext.ArhivPhones.Find(id).Sale,
                    VNalichie = _mainContext.ArhivPhones.Find(id).VNalichie,
                    Amount = 5
                };
                _mainContext.Phones.Add(phone);
                await _mainContext.SaveChangesAsync();

                _mainContext.ArhivPhones.Remove(arhive);
                await _mainContext.SaveChangesAsync();
            }
            return RedirectToAction("VsePhone");
        }


        public async Task<IActionResult> DeleteZakaz(int? id)
        {
            //         DbSet<Favorite> favorites;  
            //         MainContext mainContext = new MainContext();
            //_mainContext.Phones.Remove(DbSet <Favorite> favorites);
            //await _mainContext.SaveChangesAsync();
            //return RedirectToAction("VsePhone");

            var catalog = _mainContext.Favorites.FindAsync(id);
            _mainContext.Remove(catalog);
            await _mainContext.SaveChangesAsync();
            return RedirectToAction("FavoritePage");
        }





        [HttpPost]
        public async Task<IActionResult> Zakaz(List<int> id, Dictionary<int, int> amount)
        {
            //var user = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;

            //for (int i = 0; i < id.Count; i++)
            //{
            //    int did = 0;
            //    did = id[i];
            //    var ph = _mainContext.Favorites.FirstOrDefault(x => x.UserId == user);

                
                
            //}


            int a = 1;
            Zakaz x = new Zakaz();
            var dj = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;
            x.UserId = dj;
            var f = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
            x.Login = f.Login;
            x.Email = f.Email;
            x.Name = f.Name;
            x.Surname = f.Surname;


            var d = 0;

            if (_mainContext.Pokupkas.FirstOrDefault(x => x.UserId == dj) != null)
            {
                d = _mainContext.Pokupkas.Where(x => x.UserId == dj).Max(x => x.NumberZakaz);
            }

            for (int i = 0; d >= a; i++)
            {
                a++;
            }
            x.Number = a;

            _mainContext.Zakazs.Add(x);
            await _mainContext.SaveChangesAsync();

            if (id != null)
            {
                for (int i = 0; i < id.Count; i++)
                {

                    Pokupka pokupka = new Pokupka();
                    int did = 0;
                    did = id[i];

                    
                    pokupka.PhoneId = _mainContext.Favorites.Find(did).PhoneId;
                    pokupka.ZakazId = x.Id;
                    pokupka.UserId = _mainContext.Users.FirstOrDefault(x => x.Name == User.Identity.Name).Id;

                    pokupka.Image = _mainContext.Favorites.Find(did).Image;
                    pokupka.Name = _mainContext.Favorites.Find(did).Name;
                    pokupka.Gb = _mainContext.Favorites.Find(did).Gb;
                    pokupka.Baground = _mainContext.Favorites.Find(did).Baground;
                    pokupka.Sale = _mainContext.Favorites.Find(did).Sale;

                    int col;
                    col = amount[did];

                    pokupka.Amount = col;
                    pokupka.NumberZakaz = a;

                    var idP = _mainContext.Favorites.Find(did).PhoneId;

                    var phoneMinus = _mainContext.Phones.Where(x => x.Id == idP);
                    foreach (var p in phoneMinus)
                    {
                        p.Amount -= col;

                        if (p.Amount < 1)
                        {
                            ModelState.AddModelError("", "Такого кол-во товара нет");
                            return RedirectToAction("FavoritePage", "Catalog");
                        }
                        _mainContext.Phones.Update(p);
                     
                    }
                    

                    var n =  _mainContext.Favorites.FirstOrDefault(x => x.Id == did);

                    //_mainContext.Zakazs.Add(x);
                    //await _mainContext.SaveChangesAsync();

                    _mainContext.Favorites.Remove(n);
                   
                    _mainContext.Pokupkas.Add(pokupka);
                    await _mainContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Oformleniy", "Catalog");

        }

    }
        
}

