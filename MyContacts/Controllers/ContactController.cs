using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Services;

namespace MyContacts.Controllers;
[ApiController]
[Authorize]
[Route("/contact/")]
public class ContactController(IContactService contactService) : ControllerBase
{
    
}