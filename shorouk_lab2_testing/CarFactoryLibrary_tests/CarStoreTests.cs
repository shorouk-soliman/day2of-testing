using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_tests
{
    public class CarStoreTests
    {
        [Fact]
        public void AddCar_AddToyota_ListContainObject()
        {
            // Arrange
            CarStore carStore = new CarStore();
            Toyota toyota = new Toyota() {drivingMode = DrivingMode.Forward };
            BMW bMW = new BMW();

            // Act
            carStore.AddCar(toyota);

            // Collection Asserts
            //Assert.Empty(carStore.cars);
            Assert.NotEmpty(carStore.cars);


            // value Equality   => Equals
            Assert.Contains<Car>(toyota, carStore.cars);
            //Assert.Contains<Car>(bMW, carStore.cars);

            Assert.Contains<Car>(carStore.cars, c => c.drivingMode == DrivingMode.Forward);


            Assert.DoesNotContain<Car>(carStore.cars, c => c.velocity == 10);
        }
    }
}
