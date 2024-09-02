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
        return contacts.Item1.SubContacts.Intersect(contacts.Item2.SubContacts);
    }
    

    public void AddSubContact(Contact user, Contact subContact)
    {
        if (user.Id == subContact.Id)
            throw new ApplicationException("The user cannot add itself to contact list");
        
        contactRepository.InsertSubContact(user, subContact);
        contactRepository.Save();
    }

    public void RemoveSubContact(Contact user, Contact subContact)
    {
        
        if (contactRepository.GetContactByID(subContact.Id) == null)
            throw new ApplicationException("The contact doesn't exist");
        if (contactRepository.GetContactByID(user.Id) == null)
            throw new ApplicationException("The contact user doesn't exists");
        
        contactRepository.RemoveSubContact(user, subContact);
        contactRepository.Save();
    }

    public void EditSubContact(Contact user, Contact subContact)
    {
        if (contactRepository.GetContactByID(subContact.Id) == null)
            throw new ApplicationException($"Contact \"{subContact.PhoneNumber}\" doesn't exist");
        if (contactRepository.GetContactByID(user.Id) == null)
            throw new ApplicationException($"Contact \"{subContact.PhoneNumber}\" user doesn't exists");
        
        contactRepository.UpdateSubContact(user, subContact);
        contactRepository.Save();
    }

    public Contact GetContactUser(int id)
    {
        Contact? contact = contactRepository.GetContactByID(id);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exists");
        return contact;
    }

    public Contact GetContactUser(string phoneNumber)
    {
        Contact? contact = contactRepository.GetContactByPhoneNumber(phoneNumber);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exists");
        
        return contact;
    }

    public void CreateContactUser(Contact user)
    {
        Contact? contact = contactRepository.GetContactByPhoneNumber(user.PhoneNumber);
        if (!IsPhoneNumberFormatValid(user.PhoneNumber))
            throw new ApplicationException("The phone number is incorrect");
        if (contact != null)
            throw new ApplicationException("The contact user already exists");
        contactRepository.InsertContact(user);
        contactRepository.Save();
    }

    public void DeleteContactUser(Contact user)
    {
        Contact contact = contactRepository.GetContactByID(user.Id);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exist");
        contactRepository.DeleteContact(contact.Id);
        contactRepository.Save();
    }

    
    public void UpdateContactUser(Contact user)
    {
        Contact contact = contactRepository.GetContactByID(user.Id);
        if (contact == null)
            throw new ApplicationException("The contact user doesn't exist");
        if(!IsPhoneNumberFormatValid(contact.PhoneNumber))
            throw new ApplicationException("The phone number is incorrect");
        contactRepository.UpdateContact(user);
        contactRepository.Save();
    }

    public bool IsPhoneNumberFormatValid(string number)
    {
        return Regex.Match(number, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$").Success;
    }
}