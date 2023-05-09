using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;

public class LoginController : Controller
{
    #region Local Identity

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

        }

        return View(viewModel);
    }

    #endregion

    public async Task Facebook() => await HttpContext.ChallengeAsync(
        FacebookDefaults.AuthenticationScheme, 
        new AuthenticationProperties { RedirectUri = Url.Action("ExternalAuthentication") });

    public async Task Google() => await HttpContext.ChallengeAsync(
        GoogleDefaults.AuthenticationScheme, 
        new AuthenticationProperties { RedirectUri = Url.Action("ExternalAuthentication") });

    public async Task Twitter() => await HttpContext.ChallengeAsync(
        TwitterDefaults.AuthenticationScheme, 
        new AuthenticationProperties { RedirectUri = Url.Action("ExternalAuthentication") });


    public async Task<IActionResult> ExternalAuthentication()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Unable to login");
            return RedirectToAction("Index");
        }

        var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new
        {
            claim.Type,
            claim.Value
        });

        return Json(claims);
    }
}
