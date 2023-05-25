using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GameStore.Models.ViewModel;

namespace GameStore.Controllers
{
  public class ShoppingCartsController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ShoppingCartsController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: ShoppingCarts
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
      var claimsIdentity = (ClaimsIdentity)User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

      var shoppingCartList = _context.ShoppingCart.Include(s => s.identityUser)
                                                  .Include(s => s.product)
                                                  .Where(s => s.IdentityUserId == claim.Value);

      double totalPrix = 0;
      foreach (ShoppingCart cart in shoppingCartList)
      {
        totalPrix += cart.Quantity * cart.product.Price;
      }

      ShoppingCartVM shoppingCartVM = new()
      {
        ShoppingCarts = shoppingCartList,
        TotalPrice = totalPrix
      };

      return View(shoppingCartVM);
    }

    public async Task<IActionResult> AddQuantity(int CartId)
    {
      var cartFromDB = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == CartId);
      if (cartFromDB != null) cartFromDB.Quantity++;
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> MinusQuantity(int id)
    {
      var cartFromDB = await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == id);
      if (cartFromDB != null)
      {
        cartFromDB.Quantity--;
        if (cartFromDB.Quantity <= 0)
        {
          _context.ShoppingCart.Remove(cartFromDB);
        }
      }
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
      _context.ShoppingCart.Remove(await _context.ShoppingCart.FirstOrDefaultAsync(c => c.Id == id));
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
