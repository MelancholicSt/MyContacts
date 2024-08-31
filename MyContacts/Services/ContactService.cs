using MyContacts.Data.Models;
using MyContacts.Data.DAL;

namespace MyContacts.Services;

public class ContactService(IContactRepository contactRepository, IHttpContextAccessor httpContextAccessor) : IContactService
{

}