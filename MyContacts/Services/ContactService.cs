using System.Text.RegularExpressions;
using MyContacts.Data.Models;
using MyContacts.Data.DAL;

namespace MyContacts.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    
    public IEnumerable<Contact> GetFamiliarContacts((Contact, Contact) contacts)
    {
        return contacts.Item1.Contacts.Intersect(contacts.Item2.Contacts);
    }
    

    public void AddContactToContactUser(Contact user, Contact contact)
    {
        user.Contacts.ToList().Add(contact);
        contactRepository.UpdateContact(user);
        contactRepository.Save();
    }

    public void RemoveContactFromContactUser(Contact user, Contact contact)
    {
        
        if (contactRepository.GetContactByID(contact.Id) == null)
            throw new ApplicationException("The contact doesn't exist");
        if (contactRepository.GetContactByID(user.Id) == null)
            throw new ApplicationException("The contact user doesn't exists");
        
        user.Contacts.ToList().Remove(contact);
        contactRepository.UpdateContact(user);
        contactRepository.Save();
    }

    public Contact GetContactUser(int id)
    {
        Contact contact = contactRepository.GetContactByID(id);
          
        return contact;
    }

    public void CreateContactUser(Contact user)
    {
        Contact? contact = contactRepository.GetContactByPhoneNumber(user.PhoneNumber);
        if (!IsPhoneNumberFormatValid(contact.PhoneNumber))
            throw new ApplicationException("The phone number is incorrect");
        if (contact != null)
            throw new ApplicationException("The contact user already exists");
        contactRepository.InsertContact(user);
    }

    public void DeleteContactUser(Contact user)
    {
        Contact contact = contactRepository.GetContactByID(user.Id);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exist");
        contactRepository.DeleteContact(contact.Id);
    }

    public void UpdateContactUser(Contact user, ContactDTO info)
    {
        Contact contact = contactRepository.GetContactByID(user.Id);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exist");
        contactRepository.UpdateContact(user);
    }

    public bool IsPhoneNumberFormatValid(string number)
    {
        return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
    }
}