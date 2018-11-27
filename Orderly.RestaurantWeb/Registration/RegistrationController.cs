using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Orderly.RestaurantWeb.Registration.ViewModels;

namespace Orderly.RestaurantWeb.Registration
{

  public class RegistrationController : Controller
  {
    [HttpGet]
    [Route("register-restaurant")]
    public IActionResult Register(CancellationToken cancellationToken)
    {
      var model = new RegistrationViewModel();

      return View(model);
    }

    [HttpPost]
    [Route("register-restaurant")]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegistrationViewModel model, CancellationToken cancellationToken)
    {
      return View(model);
    }
  }
}