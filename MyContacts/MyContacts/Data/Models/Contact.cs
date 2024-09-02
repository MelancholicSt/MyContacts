using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyContacts.Data.Models;

public class Contact
{
    [ScaffoldColumn(false)]
    [Key]
    public int Id { get; set; }
    [Required, StringLength(40), Display(Name = "Name")]
    public string Name { get; set; }
    
    [Required, StringLength(15), DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    
    [StringLength(1500), Display(Name = "Contact description"), DataType(DataType.MultilineText)]
    public string Description { get; set; }

    
    public IEnumerable<Contact> Contacts;
    

 
}