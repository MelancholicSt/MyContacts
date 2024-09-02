using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;
using MyContacts.Data.Models;
using MyContacts.Services;

namespace MyContacts.Controllers;

/// <summary>
/// T
/// </summary>
///
[ApiController]
[Authorize]
[Route("/contact/")]
[OpenApiRule()]
public class ContactController(IContactService contactService) : ControllerBase
{
    /// <summary>
    ///    Gets from db contact
    /// </summary>
    /// <param name="id">id of contact to get</param>
    /// <returns>The DTO object of contact DAO</returns>
    // Crud
    [HttpGet("get/{id}")]
    public ContactDTO GetContact(int id)
    {
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(id);
        }
        catch (NullReferenceException e)
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