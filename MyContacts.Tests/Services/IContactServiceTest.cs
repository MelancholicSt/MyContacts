using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyContacts.Data.DAL;
using MyContacts.Services;

namespace MyContacts.Tests.Services;

[TestClass]
[TestSubject(typeof(IContactService))]
public class IContactServiceTest
{
    private Mock<ContactContext> _dbContext = new Mock<ContactContext>();
    
    [TestMethod]
    public void METHOD()
    {
        
    }
}