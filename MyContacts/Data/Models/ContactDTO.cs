using System.Diagnostics.CodeAnalysis;

namespace MyContacts.Data.Models;

public class ContactDTO(string phoneNumber, string name, string description, IEnumerable<int> contactsIds)
{
    public string PhoneNumber { get; } = phoneNumber;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public IEnumerable<int> ContactsIds { get; } = contactsIds;
}