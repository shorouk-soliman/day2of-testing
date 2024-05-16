using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using CarAPI.Entities;
using Moq.EntityFrameworkCore;

namespace CarFactoryAPI_Tests
{
    public class OwnerRepositoryTests
    {
        // Create Mock of Dependencies
        Mock<FactoryContext> contextMock;
        // use fake object as dependency
        OwnerRepository ownerRepository;

        public OwnerRepositoryTests()

        {
            // Create Mock of Dependencies
            contextMock = new();
            // use fake object as dependency
            ownerRepository = new(contextMock.Object);

        }
        [Fact()]
        [Trait("Author", "shorouk")]
        [Trait("Priorty", "2")]
        public void testOwner_AddOwner_Success()
        {
            //Arrange
            Owner owner = new() { Id = 10, Name = "shorouk" };
            //Act
            bool result = ownerRepository.AddOwner(owner);
            //Assert
            Assert.True(result);


        }

        [Fact()]
        [Trait("Author", "shorouk")]
        [Trait("Priorty", "2")]
        public void TestOwner_GetAllOwners_Sucess()
        {
            // Arrange
            // Build the mock data

            List<Owner> owners = new() {
            new Owner { Id = 10,Name="shorouk"},
            new Owner { Id = 20,Name="Ahmed"},
            new Owner { Id = 30,Name="Mahmoud"},
            };

            // setup called Dbsets
            contextMock.Setup(o => o.Owners).ReturnsDbSet(owners);
            // Act
            List<Owner> result = ownerRepository.GetAllOwners();
            // Assert
            Assert.NotEmpty(result);

        }
    }
}
