using System;
using System.Collections.Generic;
using System.Linq;
using MixTelematics.Tests.Helpers;
using Xunit;

namespace MixTelematics.Tests
{
    public class CoordinatesTests : ProjectDirectoryLocatorHelper
    {
        private readonly string _basePath;

        public CoordinatesTests()
        {
            _basePath = Path() + @"\Data";
        }

        [Fact]
        public void When_FindNearestVehicle_EmptyCoordinatesData_ThrowsInvalidOperationException()
        {
            // Arrange
            var emptyCoordinatesData = new List<Coordinates>();
            var filePath = _basePath + @"\VehiclePositions.dat";

            // Act
            var vehicleData = Program.ReadVehicleData(filePath).GetAwaiter().GetResult().ToList();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => Program.FindNearestVehicle(emptyCoordinatesData, vehicleData));
        }
    }
}
