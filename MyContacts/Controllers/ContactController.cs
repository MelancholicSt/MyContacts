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
[OpenApiRule]
public class ContactController(IContactService contactService, ILogger<ContactController> logger) : ControllerBase
{
    /// <summary>
    ///    Gets from db contact
    /// </summary>
    /// <param name="id">id of contact to get</param>
    /// <returns>ContactDTO</returns>
    // Crud

    [HttpGet("get/all")]
    public IEnumerable<ContactDto> GetAllContacts()
    {
        List<Contact> contacts = contactService.GetAllContacts().ToList();
        return contacts
            .Select(c => 
                new ContactDto
                    (
                        c.Id,
                        c.PhoneNumber, 
                        c.Name, 
                        c.Description, 
                        c.Friends.Select(c => c.FriendId)
                    )
            );
    }

    [HttpGet("get/match")]
    public IEnumerable<ContactDto> GetFamiliarContacts(int contactId1, int contactId2)
    {
        (Contact, Contact) contactPair;
        try
        {
            contactPair.Item1 = contactService.GetContactUser(contactId1);
            contactPair.Item2 = contactService.GetContactUser(contactId2);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return contactService
            .GetFamiliarContacts(contactPair)
            .Select(c => new ContactDto
                (
                    c.Id,
                    c.PhoneNumber,
                    c.Name,
                    c.Description,
                    c.Friends.Select(fc => fc.FriendId)
                )
            );
    }
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

        ContactDto result = new ContactDto
        (
            contact.Id,
            contact.PhoneNumber, 
            contact.Name, 
            contact.Description,
            contact.Friends.Select(c => c.Friend.Id)
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
            contact.Name = name;
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
            contactService.UpdateContactUser(contact);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e);
        }

        contact.Description = description;
        return Ok();
    }
    
    /// <summary>
    /// Request should be from frontend client
    /// </summary>
    /// <param name="subContactId">Contact which will be added to user</param>
    /// <returns></returns>
    [HttpPost("self/contacts/add")]
    public IActionResult AddSubContact(int subContactId)
    {
        string userPhoneNumber = HttpContext.User.Claims.First(claim => claim.Type == "phone").Value;
        Contact user = contactService.GetContactUser(userPhoneNumber);
        Contact contact;
        try
        {
            contact = contactService.GetContactUser(subContactId);
            contactService.AddSubContact(user, contact);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e.Message);
        }
        return Ok();
    }
    [HttpGet("self/contacts/remove")]
    public IActionResult RemoveSubContact(int subContactId)
    {
        string userPhoneNumber = HttpContext.User.Claims.First(claim => claim.Type == "phone").Value;
        Contact user = contactService.GetContactUser(userPhoneNumber);

        Contact contact;
        try
        {
            contact = contactService.GetContactUser(subContactId);
            contactService.RemoveSubContact(user, contact);
        }
        catch (ApplicationException e)
        {
            logger.LogWarning(e.Message);
            return BadRequest(e.Message);
        }
        return Ok();
    }
    
}