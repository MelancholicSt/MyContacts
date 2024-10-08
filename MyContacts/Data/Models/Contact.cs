﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace MyContacts.Data.Models;

public class Contact
{
    [ScaffoldColumn(false)]
    [Key]
    [Required]
    public int Id { get; set; }
    [Required, StringLength(40), Display(Name = "Name")]
    public string Name { get; set; }

    [Required, StringLength(11), DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
    
    [StringLength(1500), Display(Name = "Contact description"), DataType(DataType.MultilineText)]
    public string? Description { get; set;}

    public virtual ICollection<ContactFriend> Friends { get; } = new List<ContactFriend>();
}