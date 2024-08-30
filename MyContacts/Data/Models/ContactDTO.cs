namespace MyContacts.DTO;

public class ContactDTO
{
    public int ID { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<int> ContactsIds { get; set; }
}