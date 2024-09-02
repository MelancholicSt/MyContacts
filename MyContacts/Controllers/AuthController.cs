using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Data.Models;
using MyContacts.Services;

namespace MyContacts.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IContactService contactService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(ContactDTO contactDto)
    {
        List<Claim> claims = new List<Claim>
        {
            new("phone", contactDto.PhoneNumber),
            new("name", contactDto.Name),
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
        Contact contact = new Contact
        {
            PhoneNumber = contactDto.PhoneNumber,
            Name = contactDto.Name,
            Description = contactDto.Description,
            Contacts = new List<Contact>()
        };
        try
        {
            contactService.CreateContactUser(contact);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(ContactDTO contactDto)
    {
        List<Claim> claims = new List<Claim>
        {
            new("phone", contactDto.PhoneNumber),
            new("name", contactDto.Name),
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
}