using System;

namespace MixTelematics.Helpers
{
    public class DistanceCalculator
    {
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371.0; // Earth radius in kilometers

            // Convert latitude and longitude from degrees to radians
            var latRad1 = DegreesToRadians(lat1);
            var lonRad1 = DegreesToRadians(lon1);
            var latRad2 = DegreesToRadians(lat2);
            var lonRad2 = DegreesToRadians(lon2);

            // Calculate the differences between latitudes and longitudes
            var deltaLat = latRad2 - latRad1;
            var deltaLon = lonRad2 - lonRad1;

            // Apply the Haversine formula
            var a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                    Math.Cos(latRad1) * Math.Cos(latRad2) *
                    Math.Pow(Math.Sin(deltaLon / 2), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadiusKm * c;

            return distance;
        }
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
