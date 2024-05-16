using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_tests
{
    public class CarFactoryTests
    {
        [Fact]
        public void NewCar_AskForAudi_null()
        {
            // Arrange

            // Act
           Car? result =  CarFactory.NewCar(CarTypes.Audi);

            // Reference Asssert
            Assert.Null(result);
            //Assert.NotNull(result);
        }

        [Fact]
        public void NewCar_AskForToyota_toyotaObject()
        {
            // Arrange
            Toyota toyota = new Toyota();

            // Act
            Car? result = CarFactory.NewCar(CarTypes.Toyota);

            // Reference Assert
            Assert.NotNull(result);
            //Assert.Same(toyota, result); // same object in memory
            Assert.NotSame(toyota, result);

            // Equality Assert 
            // override Equals  => value Equality
            Assert.Equal(toyota, result);
        }

        [Fact]
        public void NewCar_AskForBMW_BMWObject()
        {
            // Arrange

            // Act
            Car? result =  CarFactory.NewCar(CarTypes.BMW);

            // Type Assert
            Assert.IsType<BMW>(result);
            Assert.IsNotType<Toyota>(result);

            Assert.IsAssignableFrom<Car>(result);
        }


        [Fact]
        public void NewCar_AskForHonda_Exception()
        {
            // Arrange



            // Exception Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Act
                Car? result = CarFactory.NewCar(CarTypes.Honda);
            });
            Assert.ThrowsAny<Exception>(() =>
            {
                // Act
                Car? result = CarFactory.NewCar(CarTypes.Honda);
            });

        }
    }
}
