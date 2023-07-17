using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MixTelematics.Helpers;

namespace MixTelematics
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var projectDirectory = ProjectDirectoryLocator.GetProjectDirectory();
            var filePath = projectDirectory + @"\Data\VehiclePositions.dat";

            // Read the binary .dat file
            var vehicleData = ReadVehicleData(filePath).GetAwaiter().GetResult().ToList();
            var coordinatesData = Coordinates.ReturnCoordinatesData().ToList();
            var nearestVehiclePosition = FindNearestVehicle(coordinatesData, vehicleData);

            Console.WriteLine("The nearest vehicle to each coordinate: \n");
            foreach (var (key, value) in nearestVehiclePosition)
            {
                Console.WriteLine("Coordinate: (Lat) {0}, (Long) {1} || Vehicle: (ID) {2}, (Registration) {3}", key.Latitude, key.Longitude, value.VehicleId, value.VehicleRegistration);
            }
        }

        public static async Task<IEnumerable<Vehicle>> ReadVehicleData(string filePath)
        {
            // Define the structure of a single record in the .dat file
            const int vehicleIdSize = 4;
            const int vehicleRegistrationMaxSize = 100;
            const int latitudeSize = 4;
            const int longitudeSize = 4;
            const int recordedTimeUtcSize = 8;

            // Read the binary .dat file
            var fileBytes = await File.ReadAllBytesAsync(filePath);

            Vehicle[] vehicleData;
            await using (var memoryStream = new MemoryStream(fileBytes))
            {
                using var reader = new BinaryReader(memoryStream);
                var numRecords = (int)(reader.BaseStream.Length / ((vehicleIdSize + vehicleRegistrationMaxSize + latitudeSize + longitudeSize + recordedTimeUtcSize) / 4));
                vehicleData = new Vehicle[numRecords];

                var i = 0;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var data = new Vehicle
                    {
                        // Read the fields of each record
                        VehicleId = reader.ReadInt32(),
                        VehicleRegistration = await StringManipulation.ReadNullTerminatedStringAsync(reader, vehicleRegistrationMaxSize),
                        Latitude = reader.ReadSingle(),
                        Longitude = reader.ReadSingle(),
                        RecordedTimeUtc = reader.ReadUInt64()
                    };

                    vehicleData[i] = data;
                    i++;
                }
            }

            return vehicleData.Where(data => data != null);
        }

        public static Dictionary<Coordinates, Vehicle> FindNearestVehicle(List<Coordinates> coordinates, List<Vehicle> vehicle)
        {
            var coordinateVehicleMapping = new Dictionary<Coordinates, Vehicle>();

            // Create a grid and assign houses to cells
            var grid = new Grid();
            if (coordinates.Count > 0)
            {
                foreach (var coordinate in coordinates)
                {
                    var cellId = Grid.GetCellId(coordinate.Latitude, coordinate.Longitude);
                    grid.AddCoordinatesToCell(cellId, coordinate);
                }
            }
            else
            {
                throw new InvalidOperationException("No coordinates provided.");
            }


            // Find the nearest person for each location
            foreach (var location in coordinates)
            {
                var nearestVehicle = FindNearestVehicleForCoordinates(location, grid, vehicle);
                coordinateVehicleMapping.Add(location, nearestVehicle);
            }

            return coordinateVehicleMapping;
        }

        public static Vehicle FindNearestVehicleForCoordinates(Coordinates coordinate, Grid grid, List<Vehicle> vehicle)
        {
            var nearestVehicle = vehicle
                .Where(p => Grid.IsInSameOrAdjacentCell(coordinate, p, grid))
                .OrderBy(p => DistanceCalculator.CalculateDistance(coordinate.Latitude, coordinate.Longitude, p.Latitude, p.Longitude))
                .FirstOrDefault();

            return nearestVehicle;
        }
    }
}
