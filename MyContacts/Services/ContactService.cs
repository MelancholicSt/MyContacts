using MyContacts.DTO;
using MyContacts.Repositories;

namespace MyContacts.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private IContactRepository contactRepository = contactRepository;
    private bool disposed = false;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Contact GetContacts()
    {
        throw new NotImplementedException();
    }

    public void AddContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void RemoveContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void EditContactName(string name)
    {
        throw new NotImplementedException();
    }

    public void EditContactDescription(string description)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Contact> GetContactsWithSameContact()
    {
        throw new NotImplementedException();
    }

    public void CreateContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void DeleteContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }
        if (disposing)
        {
            contactRepository.Dispose();
        }

        disposed = true;
    }
}