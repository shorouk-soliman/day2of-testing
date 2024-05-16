using CarFactoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary_tests
{
    // in xUnit Framework  
    // class to be used as a testing class
    // you need to make class puplic
    public class ToyotaTests
    {
        // Test Method
        // you need to use one of the two attributes
        // [Fact] => test Method with no parameters
        // [Theory] => test Method with parameters
        // Test Method have void return

        [Fact]
        public void IsStopped_Velocity0_true()
        {
            // Arrange
            Toyota toyota = new Toyota();
            toyota.velocity = 0;

            // Act
            bool actualResult = toyota.IsStopped();

            // Boolean Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void IsStopped_Velocity10_false()
        {
            // Arrange
            Toyota toyota = new Toyota();
            toyota.velocity = 10;

            // Act
            bool actualResult = toyota.IsStopped();

            // Boolean Assert
            Assert.False(actualResult);
        }


        [Fact]
        public void IncreaseVelocity_velocity10Add20_Velocity30()
        {
            // Arrange
            Toyota toyota = new Toyota() {velocity = 10 };
            double AddedVelocity = 20;

            // Act
            toyota.IncreaseVelocity(AddedVelocity);

            // Equality Assert
            Assert.Equal(30, toyota.velocity);
            //Assert.NotEqual(40, toyota.velocity);
        }

        [Theory]
        [InlineData(10,20,30)]
        [InlineData(15,5,20)]
        [InlineData(50,30,80)]
        public void IncreaseVelocity_useTestData_useTestOutput
            (double intialVelocity, double AddedVelocity, double ExpectedOutput)
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = intialVelocity };

            // Act
            toyota.IncreaseVelocity(AddedVelocity);

            // Equality Assert
            Assert.Equal(ExpectedOutput, toyota.velocity);
            //Assert.NotEqual(40, toyota.velocity);
        }

        [Fact]
        public void GetDirection_DirectionForward_Forward()
        {
            // Arrange
            Toyota toyota = new Toyota() { drivingMode = DrivingMode.Forward };

            // Act
            string result = toyota.GetDirection();

            // String Assert
            Assert.Equal("Forward", result);

            Assert.StartsWith("Fo", result);
            Assert.EndsWith("rd", result);

            Assert.Contains("wa", result);
            Assert.DoesNotContain("zx", result);

            Assert.Matches("F[a-z]{6}", result);
            Assert.DoesNotMatch("F[a-z]{8}", result);
        }


        [Fact]
        public void GetMyCar_AskForRefrence_Same()
        {
            // Arrange
            Toyota toyota = new Toyota();
            Toyota t = new Toyota();

            // Act
            Car result = toyota.GetMyCar();

            // Reference Assert
            Assert.Same(toyota, result);
            Assert.NotSame(t, result);
        }
    }
}
