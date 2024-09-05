using System.Diagnostics.CodeAnalysis;

namespace MyContacts.Data.Models;

public class ContactDto([NotNull]string phoneNumber, string name, string description, IEnumerable<int> contactsIds)
{
    [NotNull]
    public string PhoneNumber { get; } = phoneNumber;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public IEnumerable<int> ContactsIds { get; } = contactsIds;
}