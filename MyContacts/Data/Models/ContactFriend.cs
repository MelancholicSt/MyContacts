using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyContacts.Data.Models;

public class ContactFriend
{
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
    
    public int FriendId { get; set; }
    public Contact Friend { get; set; }
}