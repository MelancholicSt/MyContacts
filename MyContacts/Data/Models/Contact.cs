using System.ComponentModel.DataAnnotations;

namespace MyContacts.DTO;

public class Contact(string name, string phoneNumber, string description)
{
    [ScaffoldColumn(false)]
    public int ID;
    [Required, StringLength(40), Display(Name = "Name")]
    private string name = name;
    
    [Required, StringLength(15), DataType(DataType.PhoneNumber)]
    private string phoneNumber = phoneNumber;
    
    [StringLength(1500), Display(Name = "Contact description"), DataType(DataType.MultilineText)]
    private string description = description;

    
    private IEnumerable<Contact> contacts;

    public string GetName() => name;
    public void SetName(string value) => name = value;
    
    public string GetPhoneNumber() => phoneNumber;
    public void SetPhoneNumber(string value) => phoneNumber = value;

    public string GetAdditionalDescription() => description;
    public void SetAdditionalDescription(string value) => description = value;

    public IEnumerable<Contact> GetContacts() => contacts;
    public void SetContacts(IEnumerable<Contact> contacts) => this.contacts = contacts;

    public int GetID() => ID;
}