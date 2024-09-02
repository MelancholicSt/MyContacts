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
public class ContactController(IContactService contactService, ILogger<ContactController> logger) : ControllerBase
{
    /// <summary>
    ///    Gets from db contact
    /// </summary>
    /// <param name="id">id of contact to get</param>
    /// <returns>ContactDTO</returns>
    // Crud
    [HttpGet("get/{id}")]
    public IActionResult GetContact(int id)
    {
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(id);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }

        ContactDTO result = new ContactDTO
        (
            contact.PhoneNumber, 
            contact.Name, 
            contact.Description,
            contact.Contacts.Select(c => c.Id)
        );
        return Ok(result);
    }

    [HttpPatch("edit/name")]
    public IActionResult EditContactName(int id, string name)
    {
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(id);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e);
        }

        contact.Name = name;
        try
        {
            contactService.UpdateContactUser(contact);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e);
        }

        return Ok();
    }

    [HttpPatch("edit/description")]
    public IActionResult EditContactDescription(int id, string description)
    {
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(id);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e);
        }

        contact.Description = description;
        
        try
        {
            contactService.UpdateContactUser(contact);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }

        return Ok();
    }
}