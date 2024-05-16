using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_tests
{
    public class CarTests
    {
        [Fact]
        public void Accelerate_velocity10_Range15to25()
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = 10 };
            BMW bMW = new BMW() { velocity = 10 };

            // Act
            toyota.Accelerate();
            bMW.Accelerate();

            // Numeric Assert
            Assert.InRange(toyota.velocity, 15, 25);
            Assert.InRange(bMW.velocity, 15, 25);

            Assert.NotInRange(toyota.velocity, 90, 140);

            Assert.True(toyota.velocity > 10);
        }
    }
}
