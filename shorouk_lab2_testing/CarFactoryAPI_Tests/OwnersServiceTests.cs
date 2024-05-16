using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CarFactoryAPI_Tests
{
    public class OwnersServiceTests : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<ICarsRepository> _carRepoMock;
        private readonly Mock<IOwnersRepository> _ownerRepoMock;
        private readonly Mock<ICashService> _cashMock;
        private readonly OwnersService _ownersService;

        public OwnersServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _carRepoMock = new Mock<ICarsRepository>();
            _ownerRepoMock = new Mock<IOwnersRepository>();
            _cashMock = new Mock<ICashService>();
            _ownersService = new OwnersService(_carRepoMock.Object, _ownerRepoMock.Object, _cashMock.Object);
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Test clean up");
        }

        [Fact(Skip = "Fail due to fail in dependencies working on isolating Unit")]
        [Trait("Author", "Ali")]
        [Trait("Priority", "2")]
        public void BuyCar_CarNotExist_NotExist()
        {
            // Arrange
            var buyCarInput = new BuyCarInput { CarId = 100, OwnerId = 10, Amount = 5000 };

            // Act
            string result = _ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("n't exist", result);
        }

        [Fact]
        [Trait("Author", "Ahmed")]
        [Trait("Priority", "5")]
        public void BuyCar_CarNotExist_NotExist2()
        {
            _testOutputHelper.WriteLine("Test 1");
            var ownersService = new OwnersService(new CarRepoStup(), new OwnerRepoStup(), new CashServiceStup());
            var buyCarInput = new BuyCarInput { CarId = 100, OwnerId = 10, Amount = 5000 };

            string result = ownersService.BuyCar(buyCarInput);

            Assert.Contains("n't exist", result);
        }

        [Fact]
        [Trait("Author", "Ali")]
        [Trait("Priority", "9")]
        public void BuyCar_CarWithOwner_Sold()
        {
            _testOutputHelper.WriteLine("Test 2");
            var car = new Car { Id = 10, Price = 1000, OwnerId = 5, Owner = new Owner() };
            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 10, Amount = 1000 };

            string result = _ownersService.BuyCar(carInput);

            Assert.Contains("sold", result);
        }

        [Fact]
        [Trait("Author", "Ahmed")]
        [Trait("Priority", "9")]
        public void BuyCar_OwnerNotExist_NotExist()
        {
            _testOutputHelper.WriteLine("Test 3");
            var car = new Car { Id = 10, Price = 1000 };
            Owner owner = null;

            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            _ownerRepoMock.Setup(o => o.GetOwnerById(100)).Returns(owner);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 100, Amount = 1000 };

            string result = _ownersService.BuyCar(carInput);

            Assert.Contains("n't exist", result);
        }

        [Fact]
        [Trait("Author", "shorouk")]
        [Trait("Priority", "1")]
        public void BuyCar_HaveCar_AlreadyHaveCar()
        {
            _testOutputHelper.WriteLine("Test 4");
            var car = new Car { Id = 10, Price = 1000 };
            var owner = new Owner { Id = 5, Car = new Car() };

            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            _ownerRepoMock.Setup(o => o.GetOwnerById(5)).Returns(owner);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 5, Amount = 3 };

            string result = _ownersService.BuyCar(carInput);

            Assert.Contains("Already have", result);
        }

        [Fact]
        [Trait("Author", "shorouk")]
        [Trait("Priority", "5")]
        public void BuyCar_CheckPrice_Insufficient()
        {
            _testOutputHelper.WriteLine("Test 5");
            var car = new Car { Id = 10, Price = 10000 };
            var owner = new Owner { Id = 8 };

            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            _ownerRepoMock.Setup(o => o.GetOwnerById(8)).Returns(owner);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 8, Amount = 4 };

            string result = _ownersService.BuyCar(carInput);

            Assert.Contains("Insufficient", result);
        }

        [Fact]
        [Trait("Author", "shorouk")]
        [Trait("Priority", "15")]
        public void BuyCar_AssignToOwner_SomethingWrong()
        {
            _testOutputHelper.WriteLine("Test 6");
            var car = new Car { Id = 10, Price = 5000 };
            var owner = new Owner { Id = 10 };

            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            _ownerRepoMock.Setup(o => o.GetOwnerById(9)).Returns(owner);
            _carRepoMock.Setup(c => c.AssignToOwner(10, 10)).Returns(false);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 10, Amount = 5000 };

            string result = _ownersService.BuyCar(carInput);

            Assert.Contains(" wrong", result);
        }

        [Fact]
        [Trait("Author", "shorouk")]
        [Trait("Priority", "10")]
        public void BuyCar_SuccessfulPurchase()
        {
            _testOutputHelper.WriteLine("Test 7");
            var car = new Car { Id = 10, Price = 5000 };
            var owner = new Owner { Id = 20 };

            _carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            _ownerRepoMock.Setup(o => o.GetOwnerById(20)).Returns(owner);
            _cashMock.Setup(c => c.VerifyPayment(20, 5000)).Returns(true);
            _carRepoMock.Setup(c => c.AssignToOwner(10, 20)).Returns(true);

            var carInput = new BuyCarInput { CarId = 10, OwnerId = 20, Amount = 5000 };

            // Act
            string result = _ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("successfully", result);
        }
    }
}
