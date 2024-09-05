using System.Diagnostics.CodeAnalysis;

namespace MyContacts.Data.Models.DTO;

public class LoginDto(string phoneNumber)
{
    [NotNull]
    public string PhoneNumber { get; } = phoneNumber;
}