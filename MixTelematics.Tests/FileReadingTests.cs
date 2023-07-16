using System;
using System.IO;
using MixTelematics.Tests.Helpers;
using Xunit;

namespace MixTelematics.Tests
{
    public class FileReadingTests : ProjectDirectoryLocatorHelper
    {
        private readonly string _basePath;
        public FileReadingTests()
        {
             _basePath = Path() + @"\Data";
        }

        [Fact(Timeout = 15000)]
        public void When_ReadFile_ShouldReadWithinSpecifiedDuration()
        {
            // Arrange
            var filePath = _basePath + @"\VehiclePositions.dat";

            // Act
            var startTime = DateTime.Now;
            var vehicleData = Program.ReadVehicleData(filePath);
            var endTime = DateTime.Now;

            // Assert
            Assert.NotNull(vehicleData);
            Assert.InRange(endTime - startTime, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        }

        [Fact]
        public void When_ReadFile_ShouldReturnEmptyCollection_ForEmptyFile()
        {
            // Arrange
            var filePath = _basePath + @"\emptyfile.dat";

            // Act
            var vehicleData = Program.ReadVehicleData(filePath);

            // Assert
            Assert.Empty(vehicleData);
        }

        [Fact]
        public void When_ReadFile_ShouldThrowFileNotFoundException_ForNonExistentFile()
        {
            // Arrange
            var filePath = _basePath + @"\nonexistentfile.dat";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
            {
                try
                {
                    Program.ReadVehicleData(filePath);
                }
                catch (FileNotFoundException ex)
                {
                    // Assert that the exception message contains the expected file path
                    Assert.Equal(filePath, ex.FileName);
                    throw; // Rethrow the exception for proper test failure
                }
            });
        }
    }
}
