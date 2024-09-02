using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyContacts.Data.Models;
using MyContacts.Services;

namespace MyContacts.Controllers;
[ApiController]
[Authorize]
[Route("/contact/")]
public class ContactController(IContactService contactService) : ControllerBase
{
    // Crud
    [HttpGet("get/{id}")]
    public ContactDTO GetContact(int id)
    {
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(id);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            return null;
        }

        ContactDTO result = new ContactDTO
        (
            contact.PhoneNumber, 
            contact.Name, 
            contact.Description,
            contact.Contacts.Select(c => c.Id)
        );
        return result;
    }
}