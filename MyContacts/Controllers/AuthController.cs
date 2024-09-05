using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Data.Models;
using MyContacts.Data.Models.DTO;
using MyContacts.Services;

namespace MyContacts.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IContactService contactService, ILogger<AuthController> logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contactDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegistrationDto contactDto)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return BadRequest("Logged in user cannot register another one");
        
        Contact contact = new Contact
        {
            PhoneNumber = contactDto.PhoneNumber,
            Name = contactDto.Name,
        };
        try
        {
            contactService.CreateContactUser(contact);
            contact = contactService.GetContactUser(contact.PhoneNumber);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        List<Claim> claims = new List<Claim>
        {
            new("phone", contact.PhoneNumber),
            new("name", contact.Name),
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authenticationProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            RedirectUri = new PathString("/contact/me"),
        };
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authenticationProperties
        );
        
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto contactDto)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return BadRequest("User already logged in");
                
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(contactDto.PhoneNumber);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e);
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim("phone", contact.PhoneNumber),
            new Claim("name", contact.Name)
        };
        
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authenticationProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            RedirectUri = new PathString("/contact/me"),
        };
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authenticationProperties
        );
        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}