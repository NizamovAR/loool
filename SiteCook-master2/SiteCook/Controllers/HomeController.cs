using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteCook.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SiteCook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Authorization()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authorization(string login, string password)
        {

            using (RecipeBookContext db = new RecipeBookContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(o => o.Login == login && o.Password == password);
                if (user is not null)
                {
                    var role = await db.Roles.FirstOrDefaultAsync(o => o.RoleId == user.RoleId);
                    if (role?.RoleName == "Administrator")
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), new Claim(ClaimsIdentity.DefaultRoleClaimType, "Administrator") };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction("AddingCategory");
                    }
                    else
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")};
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction("Main");
                    }

                }

                else
                {
                    ViewBag.Enter = "Неверные данные для входа";
                    return View();
                }
            }

        }
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult M(string catName)
        {

            List<Category> categories = new List<Category>();
            using (RecipeBookContext context = new RecipeBookContext())
            {
                if (string.IsNullOrEmpty(catName))
                {
                    catName = "";
                }
                categories = context.Categories.Where(s => s.NameCategory.ToLower().Contains(catName.ToLower())).ToList();
            }
            return View(categories);
        }
        public IActionResult Meal(int id)
        {
            User user = new User();
            Recipe recipe = new Recipe();
            List<Gramm> gramms = new List<Gramm>();
            var CurrentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user != null && int.TryParse(CurrentUser.Value, out int idUser))
            {

                using (RecipeBookContext context = new RecipeBookContext())
                {
                    user = context.Users.Where(p => p.UserId == idUser).FirstOrDefault();
                    recipe = context.Recipes.Where(p => p.RecipeId == id).FirstOrDefault();
                    gramms = context.Gramms.Where(p => p.RecipeId == id).ToList();
                    var ingredients = context.Ingredients.ToList();
                    var result = ingredients.Join(
                        gramms,
                        ingredient => ingredient.IngredientId,
                        gramm => gramm.IngredientId,
                        (ingredient, gramm) => new IngredientName { Ingredientname = ingredient.IngredientName }).ToList();
                    ViewData["Users"] = user;
                    ViewData["Recipes"] = recipe;
                    return View(result);
                }
            }
            return View();

        }
        public IActionResult Main()
        {
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(string login, string password)
        {
            using (RecipeBookContext db = new RecipeBookContext())
            {
                User RegistratedClient = (from c in db.Users where c.Login == login select c).FirstOrDefault();
                if (RegistratedClient != null)
                {
                    ViewBag.Enter = "Пользователь с данным логином уже зарегистрирован";
                    return View();
                }
                else
                {

                    User user = new User();
                    user.Login = login;
                    user.Password = password;
                    user.Photo = 
                    db.Add(user);
                    db.SaveChanges();
                    return View("Authorization");
                }
            }
        }
        [AllowAnonymous]

        public RedirectToActionResult LeaveAccount()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Authorization");
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]

        [Authorize(Roles = "User")]
        public IActionResult PrivateAccount()
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int id = Convert.ToInt32(user.Value);
            using (RecipeBookContext db = new RecipeBookContext())
            {
                User currentUser = db.Users.Where(e => e.UserId == id).FirstOrDefault();
                return View("PrivateAccount", currentUser);
            }

        }
        public RedirectToActionResult SaveChangesUser(string loginOrEmail, string password, string nik, DateTime Data)
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int id = Convert.ToInt32(claim.Value);
            using (RecipeBookContext db = new RecipeBookContext())
            {
                User currentUser = db.Users.Where(e => e.UserId == id).FirstOrDefault();
                currentUser.Login = loginOrEmail;
                currentUser.Password = password;
                db.SaveChanges();
            }

            ViewBag.Message = string.Format("Данные были сохранены");
            return RedirectToAction("PrivateAccount");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //[Authorize(Roles = "Moderator")]
        //public IActionResult PrivateAccountModer()
        //{
        //    var moder = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        //    int id = Convert.ToInt32(moder.Value);
        //    using (RecipeBookContext db = new RecipeBookContext())
        //    {
        //        Moderator currentModerator = db.Moderators.Where(e => e.IdModerator == id).FirstOrDefault();
        //        return View("PrivateAccountModer", currentModerator);
        //    }

        //}
        //public RedirectToActionResult SaveChangesModer(string loginOrEmail, string password, string nik, DateTime Data)
        //{
        //    var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        //    int id = Convert.ToInt32(claim.Value);
        //    using (RecipeBookContext db = new RecipeBookContext())
        //    {
        //        Moderator currentModer = db.Moderators.Where(e => e.IdModerator == id).FirstOrDefault();
        //        currentModer.Mail = loginOrEmail;
        //        currentModer.Password = password;
        //        currentModer.NikName = nik;
        //        currentModer.DateOfBirth = Data;
        //        db.SaveChanges();
        //    }

        //    ViewBag.Message = string.Format("Данные были сохранены");
        //    return RedirectToAction("PrivateAccountModer");
        //}

        [Authorize(Roles = "Administrator")]
        public IActionResult AddingCategory()
        {
            return View();
        }
        public RedirectToActionResult AddCategory(string nameCategory, IFormFile photo)
        {
            MemoryStream ms = new MemoryStream();
            photo.CopyTo(ms);
            using (RecipeBookContext db = new RecipeBookContext())
            {
                Category category = new Category();
                category.NameCategory = nameCategory;
                db.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("AddingCategory");
        }
        public IActionResult AddingMeal()
        {
            return View();
        }
        public async Task<RedirectToActionResult> AddRecipe(string nameRecipe, string desc, string namecateg, IFormFile imgRecipe)
        {
            if (imgRecipe != null && imgRecipe.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await imgRecipe.CopyToAsync(ms);
                    var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                    if (user != null && int.TryParse(user.Value, out int id))
                    {
                        using (RecipeBookContext db = new RecipeBookContext())
                        {
                            var category = await db.Categories.FirstOrDefaultAsync(c => c.NameCategory == namecateg);

                            if (category != null)
                            {
                                Recipe recipe = new Recipe
                                {
                                    Photo = ms.ToArray(),
                                    Description = desc,
                                    Name = nameRecipe,
                                    UserId = id,
                                    CategoryId = category.CategoryId
                                };
                                db.Recipes.Add(recipe);
                                await db.SaveChangesAsync();
                                List<string> selectedItems = Request.Form["nameIngredients"].ToList();
								var recipeAdded = await db.Recipes.FirstOrDefaultAsync(c => c.Name == nameRecipe);
								if (selectedItems.Count > 0)
                                {
                                    foreach (var item in selectedItems)
                                    {
                                        var selectedIng = await db.Ingredients.FirstOrDefaultAsync(c => c.IngredientName == item);
                                        Gramm ing = new Gramm
                                        {
                                            RecipeId = recipeAdded.RecipeId,
                                            IngredientId = selectedIng.IngredientId,

                                        };

                                        db.Gramms.Add(ing);
                                        await db.SaveChangesAsync();
                                    }
                                }
                                


                            }
                        }
                    }
                }
            }
            return RedirectToAction("addingMeal");
        }


		[Authorize(Roles = "Administrator")]
        public IActionResult AddingModerator()
        {
            return View();
        }
        //public RedirectToActionResult AddModerator(string email, string pass, string nickname, DateTime Data, string namecateg)
        //{
        //    using (RecipeBookContext db = new RecipeBookContext())
        //    {
        //        var categ = db.Categories.Where(x => x.NameCategory == namecateg).FirstOrDefault();
        //        int id = categ.CategoryId;
        //        Moderator moderator = new Moderator();
        //        moderator.Mail = email;
        //        moderator.Password = pass;
        //        moderator.NikName = nickname;
        //        moderator.DateOfBirth = Data;
        //        moderator.IdCategory = id;
        //        db.Add(moderator);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("AddingModerator");
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

	}
}

