using Microsoft.AspNetCore.Mvc;
using MyContacts.Services;

namespace MyContacts.Controllers;
[ApiController]
public class ContactController(IContactService contactService) : ControllerBase
{
    
}