namespace MyContacts.Data.Models.DTO;

public class RegistrationDto(string phoneNumber, string name)
{
    public string PhoneNumber { get; } = phoneNumber;
    public string Name { get; } = name;
}