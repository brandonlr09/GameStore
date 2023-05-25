using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace GameStore.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Products
    public async Task<IActionResult> Index(int? category)
    {
      if (category == null)
      {
        return _context.Product != null ?
                    View(await _context.Product.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Product'  is null.");
      }
      else
      {
        var productList = await _context.Product.Where(c => (int)c.Category == category).ToListAsync();
        return View(productList);
      }

    }

    [HttpPost]
    public async Task<IActionResult> Index(int category)
    {
        var productList = await _context.Product.Where(c => (int)c.Category == category).ToListAsync();
        return View(productList);
    }



    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.Product == null)
      {
        return NotFound();
      }

      var product = await _context.Product
          .FirstOrDefaultAsync(m => m.Id == id);
      if (product == null)
      {
        return NotFound();
      }

      return View(product);
    }
    private readonly ILogger<HomeController> _logger;


    [HttpGet]
    public async Task<IActionResult> CustomerDetails(int Productid)
    {
      ShoppingCart shoppingCart = new ShoppingCart
      {
        product = await _context.Product
          .FirstOrDefaultAsync(m => m.Id == Productid),
        ProductId = Productid,
        Quantity = 1
      };

      return View(shoppingCart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> CustomerDetails(ShoppingCart shoppingCart)
    {
      var claimsIdentity = (ClaimsIdentity)User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

      shoppingCart.IdentityUserId = claim.Value;
      _context.ShoppingCart.Add(shoppingCart);

      await _context.SaveChangesAsync();

      return RedirectToAction("Index", "ShoppingCarts", new { area = "" });
    }


    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult AboutUs()
    {
      return View();
    }

    public IActionResult ContactUs()
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